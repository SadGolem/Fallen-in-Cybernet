using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HintMaker : MonoBehaviour
{
    public TextMeshProUGUI hintm;

    private List<List<string>> hintsPerPhrase = new List<List<string>>();
    private Dictionary<int, int> hintCountPerPhrase = new Dictionary<int, int>();

    public bool AllHintsGiven { get; private set; } = false;

    private void Start()
    {
        // �������������� ��������� ��� ������ �����
        // ��������, ��� ������ �����...
        hintsPerPhrase.Add(new List<string>
        {
            "� ������ ���� � ������� ������",
            "� ������ ���� �� ������ ������",
            "� ��������� ������ ������ ���� ���� �",
            "� ������ ���� �� ������ ������",
            "������� � ������ ���� � ������ ������"
        });
        hintsPerPhrase.Add(new List<string>
        {
            "������ ��������� ��� ����� 2",
            "������ ��������� ��� ����� 2",
            "������ ��������� ��� ����� 2",
            "��������� ��������� ��� ����� 2",
            "����� ��������� ��� ����� 2"
        });
        // �������� ���������� ��������� ��� ������ ����...
    }

    // ����� ��� ������� ��������� ��� ������� �����
    public string GetHint()
    {
        int phraseIndex = Encrypter.Instance.GetSelectedPhraseIndex();

        // ���������, ���� �� ��� ��� ���� ����� �����-���� �������� ���������
        if (!hintCountPerPhrase.ContainsKey(phraseIndex))
        {
            hintCountPerPhrase[phraseIndex] = 0;
        }

        if (hintCountPerPhrase[phraseIndex] < hintsPerPhrase[phraseIndex].Count)
        {
            string hint = hintsPerPhrase[phraseIndex][hintCountPerPhrase[phraseIndex]];
            hintCountPerPhrase[phraseIndex]++;
            return hint;
        }
        else
        {
            // ���������, ���� �� ������ ��� ��������� ��� ���� ����
            if (hintCountPerPhrase.Values.All(count => count >= 5))
            {
                AllHintsGiven = true;
            }
            return "��� ���� ����� ������ ��� ���������.";
        }
    }

    public void SetHint() 
    {
        hintm.text = GetHint();
    }
}