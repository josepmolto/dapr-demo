namespace Derbyzone.Extensions;
public static class RandomHelper
{
    public static T Next<T>(IEnumerable<T> values)
    {
        var index = Random.Shared.Next(0, values.Count());

        return values.ElementAt(index);
    }
}