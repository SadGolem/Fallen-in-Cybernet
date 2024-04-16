using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Quiz/Answer")]
public class CardData : ScriptableObject
{
    public string answerText; // ����� ������
    public bool isCategoryA; // ����������� �� ����� ��������� A (���� ���, �� ��������� B)
}

