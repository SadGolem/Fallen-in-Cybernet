using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialMagic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject animCode;
    [SerializeField] private GameObject animAnswer;
    [SerializeField] private GameObject animInput;
    [SerializeField] private GameObject animNumbers;
    [SerializeField] private GameObject animField;


    // Update is called once per frame
    void Update()
    {
        if (_text.text == "����� ����� ��� ����.")
        {
            animField.SetActive(true);
        }
        else if(_text.text == "�� ������ ����������� �����.")
        {
            animField.SetActive(false);
            animNumbers.SetActive(true);
        }
        else if (_text.text == "������ ���� ������.")
        {
            animNumbers.SetActive(false);
            
            animInput.SetActive(true);
        }
        else if (_text.text == "������� ������ ���� ����������� ����� ������� ����� ����� �� ������ �������.")
        {
            animInput.SetActive(false);
            animCode.SetActive(true);
        }
        else if (_text.text.Contains("�������"))
        {
            animCode.SetActive(false);
            animAnswer.SetActive(true);
        }
        else
        {
            animAnswer.SetActive(false);
        }

    }
}
