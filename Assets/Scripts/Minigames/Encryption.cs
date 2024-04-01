using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Encrypter : MonoBehaviour
{
    public string original; // Переменная для сохранения оригинальной строки
    public string shuffled; // Переменная для сохранения перетасованной строки
    System.Random random = new System.Random(); // Рандом

    private string ShuffleString(string input, int chunkSize)
    {
        List<string> chunks = new List<string>();

        // Разбиваем строку на сегменты по chunkSize символов
        for (int i = 0; i < input.Length; i += chunkSize)
        {
            chunks.Add(i + chunkSize <= input.Length ? input.Substring(i, chunkSize) : input.Substring(i));
        }

        // Перетасовываем сегменты
        for (int i = chunks.Count - 1; i > 0; i--)
        {
            int swapIndex = random.Next(i + 1);
            string temp = chunks[i];
            chunks[i] = chunks[swapIndex];
            chunks[swapIndex] = temp;
        }

        // Собираем перетасованные сегменты обратно в строку
        return string.Join("", chunks);
    }

    // Метод для выполнения "шифрования"
    public void doEncrypt()
    {
        List<string> encrList = new List<string>() { "воспользуйтесь_подсказкой" };
        int index = random.Next(encrList.Count);
        original = encrList[index]; // Сохраняем оригинальную строку

        // Перетасовываем строку, беря по 5 символов за раз
        shuffled = ShuffleString(original, 5);

        // Выводим результат в консоль (для отладки)
        Debug.Log("Original: " + original);
        Debug.Log("Shuffled: " + shuffled);
    }

    // Метод для перетасовки символов в строке

}

