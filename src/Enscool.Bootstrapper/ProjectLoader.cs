using System.Reflection;

namespace Enscool.Bootstrapper;

public static class ProjectLoader
{
    private const string ModulePart = "Enscool.Modules.";
    private const string TestModulePart = @".IntegrationTests\bin\Debug\net8.0\Enscool.Modules.";

    public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location);
        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        var disabledProjects = new List<string>();
        var modulePart = IsTestMode() ? TestModulePart : ModulePart;
        RemoveDisabledProjects(configuration, files, disabledProjects, modulePart);

        disabledProjects.ForEach(disabledModule => files.Remove(disabledModule));
        files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

        return assemblies;
    }

    public static IList<TProjectType> LoadProjects<TProjectType>(IEnumerable<Assembly> assemblies)
    {
        var projects = assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(TProjectType).IsAssignableFrom(x) && !x.IsInterface)
            .OrderBy(x => x.Name)
            .Select(Activator.CreateInstance)
            .Cast<TProjectType>();

        return projects.ToList();
    }

    private static void RemoveDisabledProjects(IConfiguration configuration, List<string> files, List<string> disabledProjects, string modulePart)
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

    private static bool IsTestMode() => AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(x => x.FullName is not null ? [x.FullName] : x.GetReferencedAssemblies().Select(y => y.FullName))
        .Any(x => x.Contains("xunit") || x.Contains("nunit") || x.Contains("mstest") || x.Contains("testhost"));
}