using UnityEngine;
using System.Collections.Generic;

public class MiniGameController : MonoBehaviour
{
    public List<CardData> answers; // ������ ���� �������, ������� �� ��������� � Inspector
    public int scoreCategoryA; // ������� ���������� ������� ��� ��������� A
    public int scoreCategoryB; // ������� ���������� ������� ��� ��������� B
    public int playerHP; // �������� ������

    // ����� ��� ������, ����� ����� ��������� ����� � ��������� A ��� B
    public void ChooseCategory(CardData chosenAnswer, bool isCategoryAChosen)
    {
        if (chosenAnswer.isCategoryA == isCategoryAChosen)
        {
            // ����� ����������
            scoreCategoryA++;
        }
        else
        {
            // ����� ������������
            scoreCategoryB++;
            playerHP--; // ��������� �������� ������
        }

        // ������� ����� �� ������ ��������� �������
        answers.Remove(chosenAnswer);

        // ���������, �������� �� ��� �������
        if (answers.Count == 0)
        {
            EndGame(); // ��������� ����, ���� ������� �����������
        }
    }

    void EndGame()
    {
        // ����� ������ ��� ���������� ����
        Debug.Log("���� ���������. ����: " + scoreCategoryA);
    }
}