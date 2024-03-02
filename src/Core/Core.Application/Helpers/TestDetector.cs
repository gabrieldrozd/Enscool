namespace Core.Application.Helpers;

public static class TestDetector
{
    public static bool IsTestMode() => AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(x => x.FullName is not null ? [x.FullName] : x.GetReferencedAssemblies().Select(y => y.FullName))
        .Any(x => x.Contains("xunit") || x.Contains("nunit") || x.Contains("mstest") || x.Contains("testhost"));
}