using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public static partial class RandomElementsSelector<T>
{
    private static Random random = new Random();

    public static T SelectRandomElement(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            return default(T);
        }

        int randomIndex = random.Next(0, list.Count);
        return list[randomIndex];
    }

    public static List<T> SelectRandomThreeElement(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            return default(List<T>);
        }

        int randomIndex = random.Next(0, list.Count);
        int randomIndex2 = random.Next(0, list.Count);
        int randomIndex3 = random.Next(0, list.Count);

        var result = new List<T>();
        result.Add(list[randomIndex]);
        result.Add(list[randomIndex2]);
        result.Add(list[randomIndex3]);
        return result;
    }

}

public static partial class RandomElementsSelector<T, A>
{
    private static Random random = new Random();

    public static (T,A) SelectRandomElement(List<(T, A)> list)
    {
        if (list == null || list.Count == 0)
        {
            return default((T,A));
        }

        int randomIndex = random.Next(0, list.Count);
        return list[randomIndex];
    }

    public static List<(T,A)> SelectRandomThreeElement(List<(T,A)> list)
    {
        if (list == null || list.Count == 0)
        {
            return default(List<(T,A)>);
        }

        int randomIndex = random.Next(0, list.Count);
        int randomIndex2 = random.Next(0, list.Count);
        int randomIndex3 = random.Next(0, list.Count);

        var result = new List<(T, A)>();
        result.Add(list[randomIndex]);
        result.Add(list[randomIndex2]);
        result.Add(list[randomIndex3]);
        return result;
    }

}
