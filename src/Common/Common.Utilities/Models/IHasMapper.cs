namespace Common.Utilities.Models;

public interface IMapTo<out TDestination>
{
    public TDestination Map();
}