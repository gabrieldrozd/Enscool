namespace Common.Utilities.Models;

public interface IWithMapTo<out TDestination>
{
    public TDestination Map();
}