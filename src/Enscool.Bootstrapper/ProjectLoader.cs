using System.Reflection;
using Core.Application.Helpers;

namespace Enscool.Bootstrapper;

public static class ProjectLoader
{
    private const string ModulePart = "Modules.";
    private const string TestModulePart = @".IntegrationTests\bin\Debug\net8.0\Modules.";

    private const string ServicePart = "Services.";
    private const string TestServicePart = @".IntegrationTests\bin\Debug\net8.0\Services.";

    public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location);
        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        var disabledProjects = new List<string>();

        var modulePart = TestDetector.IsTestMode() ? TestModulePart : ModulePart;
        RemoveDisabledModules(configuration, files, disabledProjects, modulePart);

        var servicePart = TestDetector.IsTestMode() ? TestServicePart : ServicePart;
        RemoveDisabledServices(configuration, files, disabledProjects, servicePart);

        disabledProjects.ForEach(disabledProject => files.Remove(disabledProject));
        files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

        return assemblies;
    }

    public static List<TProjectType> LoadProjects<TProjectType>(IEnumerable<Assembly> assemblies)
    {
        var projects = assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(TProjectType).IsAssignableFrom(x) && !x.IsInterface)
            .OrderBy(x => x.Name)
            .Select(Activator.CreateInstance)
            .Cast<TProjectType>();

        return projects.ToList();
    }

    private static void RemoveDisabledModules(IConfiguration configuration, List<string> files, List<string> disabledProjects, string modulePart)
    {
        foreach (var file in files)
        {
            if (!file.Contains(modulePart))
                return;

            var name = file.Split(modulePart)[1].Split(".")[0];
            var enabled = configuration.GetValue<bool>($"{name}Module:Enabled");
            if (!enabled) disabledProjects.Add(file);
        }
    }

    private static void RemoveDisabledServices(IConfiguration configuration, List<string> files, List<string> disabledProjects, string servicePart)
    {
        foreach (var file in files)
        {
            if (!file.Contains(servicePart))
                return;

            var name = file.Split(servicePart)[1].Split(".")[0];
            var enabled = configuration.GetValue<bool>($"{name}Service:Enabled");
            if (!enabled) disabledProjects.Add(file);
        }
    }
}