using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Quiz/Answer")]
public class CardData : ScriptableObject
{
    public string answerText; // ����� ������
    public string nameAnswer; // ����� ������
    public bool isTrue; // ����������� �� ����� ��������� A (���� ���, �� ��������� B)
}

