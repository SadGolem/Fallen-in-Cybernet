using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] private GameObject nps;
    [SerializeField] private Animator animator;

    void Update()
    {
        if (TextMeshProUGUI.text == "�������� �� ��������� ����� �����.")
        {
            nps.SetActive(true);
            /*this.gameObject.SetActive(false);*/
        }
        if (TextMeshProUGUI.text == "� ��� ��� ������ ��???")
        {
            animator.Play("flasion", 0, 0.2f);
            /*this.gameObject.SetActive(false);*/
        }
        if (TextMeshProUGUI.text == "��� ������� �� ���� � ������ �� ������ �����������.")
        {
            nps.SetActive(false);
            /*this.gameObject.SetActive(false);*/
        }
    }
}
