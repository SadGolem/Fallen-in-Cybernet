using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Encrypter : MonoBehaviour
{
    public string original; // ���������� ��� ���������� ������������ ������
    public string shuffled; // ���������� ��� ���������� �������������� ������
    System.Random random = new System.Random(); // ������

    private string ShuffleString(string input, int chunkSize)
    {
        List<string> chunks = new List<string>();

        // ��������� ������ �� �������� �� chunkSize ��������
        for (int i = 0; i < input.Length; i += chunkSize)
        {
            chunks.Add(i + chunkSize <= input.Length ? input.Substring(i, chunkSize) : input.Substring(i));
        }

        // �������������� ��������
        for (int i = chunks.Count - 1; i > 0; i--)
        {
            int swapIndex = random.Next(i + 1);
            string temp = chunks[i];
            chunks[i] = chunks[swapIndex];
            chunks[swapIndex] = temp;
        }

        // �������� �������������� �������� ������� � ������
        return string.Join("", chunks);
    }

    // ����� ��� ���������� "����������"
    public void doEncrypt()
    {
        List<string> encrList = new List<string>() { "��������������_����������" };
        int index = random.Next(encrList.Count);
        original = encrList[index]; // ��������� ������������ ������

        // �������������� ������, ���� �� 5 �������� �� ���
        shuffled = ShuffleString(original, 5);

        // ������� ��������� � ������� (��� �������)
        Debug.Log("Original: " + original);
        Debug.Log("Shuffled: " + shuffled);
    }

    // ����� ��� ����������� �������� � ������

}

