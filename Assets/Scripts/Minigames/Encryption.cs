using System;
using System.Collections.Generic;
using UnityEngine;

public class Encrypter : MonoBehaviour
{
    public static Encrypter Instance { get; private set; }

    private string original; // ���������� ��� ���������� ������������ ������
    private string shuffled; // ���������� ��� ���������� �������������� ������
    static int selectedIndex; // ���������� ��� ���������� ������� ��������� ������
    private System.Random random = new System.Random(); // ��������� ��������� �����

    // ������ ���� ��� ����������
    private List<string> encrList = new List<string>()
    {
        "��������������_����������",
        "�����-����_�������_������",
        // �������� �������������� ����� ����
    };

    // ����� ��� ���������� "����������"
    public void doEncrypt()
    {
        // �������� ��������� ������ �� ������
        selectedIndex = random.Next(encrList.Count);
        original = encrList[selectedIndex]; // ��������� ������������ ������, ��������� ��������

        // �������������� ������, ���� �� 5 �������� �� ���
        shuffled = ShuffleString(original, 5);

        /*// ������� ���������� � ������� (��� �������)
        Debug.Log($"Selected Index: {selectedIndex}");
        Debug.Log("Original: " + original);
        Debug.Log("Shuffled: " + shuffled);*/
    }

    // ����� ��� ����������� �������� � ������
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
    public int GetSelectedPhraseIndex()
    {
        return selectedIndex;
    }
}