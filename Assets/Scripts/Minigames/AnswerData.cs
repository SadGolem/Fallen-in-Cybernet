using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Quiz/Answer")]
public class AnswerData : ScriptableObject
{
    public string answerText; // ����� ������
    public bool isCategoryA; // ����������� �� ����� ��������� A (���� ���, �� ��������� B)
}

