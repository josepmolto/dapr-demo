namespace Derbyzone.Extensions;
public static class RandomExtensions
{
    public static T Next<T>(this Random random, IEnumerable<T> values)
    {
        var index = random.Next(0, values.Count());

        return values.ElementAt(index);
    }
}