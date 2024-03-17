using System;
using System.Collections.Generic;

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

    public static List<(T, A)> SelectRandomFourElement(List<(T, A)> list)
    {
        if (list == null || list.Count == 0)
        {
            return new List<(T, A)>();
        }

        Random random = new Random();
        var result = new List<(T, A)>();

        // Создаем список уникальных случайных индексов
        var randomIndices = new List<int>();
        while (randomIndices.Count < 4)
        {
            int randomIndex = random.Next(0, list.Count);
            if (!randomIndices.Contains(randomIndex))
            {
                randomIndices.Add(randomIndex);
            }
        }

        // Добавляем соответствующие элементы к результату
        foreach (var index in randomIndices)
        {
            result.Add(list[index]);
        }

        return result;
    }

}
