using System;
using System.Collections.Generic;
using UnityEngine;

public class Encrypter : MonoBehaviour
{
    public static Encrypter Instance { get; private set; }

    private string original; // Переменная для сохранения оригинальной строки
    private string shuffled; // Переменная для сохранения перетасованной строки
    static int selectedIndex; // Переменная для сохранения индекса выбранной строки
    private System.Random random = new System.Random(); // Генератор случайных чисел

    // Список фраз для шифрования
    private List<string> encrList = new List<string>()
    {
        "воспользуйтесь_подсказкой",
        "мысль-свет_высшего_разума",
        // Добавьте дополнительные фразы сюда
    };

    // Метод для выполнения "шифрования"
    public void doEncrypt()
    {
        // Выбираем случайный индекс из списка
        selectedIndex = random.Next(encrList.Count);
        original = encrList[selectedIndex]; // Сохраняем оригинальную строку, выбранную случайно

        // Перетасовываем строку, беря по 5 символов за раз
        shuffled = ShuffleString(original, 5);

        /*// Выводим результаты в консоль (для отладки)
        Debug.Log($"Selected Index: {selectedIndex}");
        Debug.Log("Original: " + original);
        Debug.Log("Shuffled: " + shuffled);*/
    }

    // Метод для перетасовки символов в строке
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
    public int GetSelectedPhraseIndex()
    {
        return selectedIndex;
    }
}