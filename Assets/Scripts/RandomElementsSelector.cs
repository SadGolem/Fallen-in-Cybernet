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

    public static List<T> SelectRandomThreeElement(List<T> list, string rightAnswer)
    {
        if (list == null || list.Count == 0)
        {
            return default(List<T>);
        }
        List<T> newList = new List<T>();
        newList.AddRange(list);

        for (int i = newList.Count - 1; i >= 0; i--)
        {
            if (newList[i].ToString() == rightAnswer)
                newList.RemoveAt(i);
        }
        
        int randomIndex = random.Next(0, newList.Count - 1);
        var item1 = newList[randomIndex];
        newList.RemoveAt(randomIndex);
        int randomIndex2 = random.Next(0, newList.Count - 2);
        var item2 = newList[randomIndex2];
        newList.RemoveAt(randomIndex2);
        int randomIndex3 = random.Next(0, newList.Count - 3);

        var result = new List<T>();
        result.Add(item1);
        result.Add(item2);
        result.Add(newList[randomIndex3]);
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
