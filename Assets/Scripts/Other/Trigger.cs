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
        if (TextMeshProUGUI.text == "Внезапно вы замечаете толпу людей.")
        {
            nps.SetActive(true);
            /*this.gameObject.SetActive(false);*/
        }
        if (TextMeshProUGUI.text == "И где мне искать АИ???")
        {
            animator.Play("flasion", 0, 0.2f);
            /*this.gameObject.SetActive(false);*/
        }
        if (TextMeshProUGUI.text == "Вас хватают за руку и уводят на другой перекресток.")
        {
            nps.SetActive(false);
            /*this.gameObject.SetActive(false);*/
        }
    }
}
