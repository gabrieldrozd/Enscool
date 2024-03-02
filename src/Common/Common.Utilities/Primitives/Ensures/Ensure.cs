namespace Common.Utilities.Primitives.Ensures;

public sealed class Ensure : EnsureHas, IEnsureIs, IEnsureNot
{
    public static readonly EnsureHas Has = new Ensure();
    public static readonly IEnsureIs Is = new Ensure();
    public static readonly IEnsureNot Not = new Ensure();

    private Ensure()
    {
    }
}