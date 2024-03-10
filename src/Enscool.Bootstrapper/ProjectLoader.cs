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

        var isTestMode = TestDetector.IsTestMode();
        var disabledProjects = new List<string>();

        RemoveDisabledProjects(configuration, files, disabledProjects, isTestMode);

        disabledProjects.ForEach(disabledModule => files.Remove(disabledModule));
        files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

        return assemblies;
    }

    private static void RemoveDisabledProjects(IConfiguration configuration, List<string> files, List<string> disabledProjects, bool isTestMode)
    {
        foreach (var file in files)
        {
            if (isTestMode)
            {
                CheckAndAddDisabledProject(configuration, file, disabledProjects, TestModulePart, "Module");
                CheckAndAddDisabledProject(configuration, file, disabledProjects, TestServicePart, "Service");
            }
            else
            {
                CheckAndAddDisabledProject(configuration, file, disabledProjects, ModulePart, "Module");
                CheckAndAddDisabledProject(configuration, file, disabledProjects, ServicePart, "Service");
            }
        }
    }

    private static void CheckAndAddDisabledProject(IConfiguration configuration, string file, List<string> disabledProjects, string part, string type)
    {
        if (!file.Contains(part))
            return;

        var name = file.Split(part)[1].Split(".")[0];
        var enabled = configuration.GetValue<bool>($"{name}{type}:Enabled");
        if (!enabled) disabledProjects.Add(file);
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
}