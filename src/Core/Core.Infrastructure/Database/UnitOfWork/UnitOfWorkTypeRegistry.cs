using Core.Application.Abstractions.Database;
using Core.Infrastructure.Helpers;

namespace Core.Infrastructure.Database.UnitOfWork;

internal sealed class UnitOfWorkTypeRegistry
{
    private readonly Dictionary<string, Type> _types = new();

    public void Register<T>() where T : IBaseUnitOfWork => _types[GetKey<T>()] = typeof(T);

    public Type? Resolve<T>() => _types.TryGetValue(GetKey<T>(), out var type) ? type : null;

    private static string GetKey<T>() => $"{typeof(T).GetModuleName()}";
}