using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject window1;
    [SerializeField] private GameObject window2;
    [SerializeField] private GameObject animArrpows;
    [SerializeField] private GameObject animGrid;
    [SerializeField] private GameObject animHint;
    [SerializeField] private GameObject animMethod;


    // Update is called once per frame
    void Update()
    {
        if (_text.text == "� �������� ���� ������ � ������� ������������. ������� � ���������.")
        {
            window2.SetActive(false);
            animMethod.SetActive(true);
        }
        else if(_text.text == "����� � ������ ������������� ������ ��������������� ����������.")
        {
            animMethod.SetActive(false);
            animHint.SetActive(true);
        }
        else if (_text.text == "��� ������ ����� �� �����-������ ������. ��� ����, ����� ������������� ����� �������� � �������, ���������� �������� ��� ����.")
        {
            animHint.SetActive(false);
            window.SetActive(false);
            
            animGrid.SetActive(true);
        }
        else if (_text.text == "����� ������ ������ ��� �������, ������ �� ���������.")
        {
            window1.SetActive(false);
            animGrid.SetActive(false);
            animArrpows.SetActive(true);
        }
        else
        {
            animArrpows.SetActive(false);
        }

    }
}
