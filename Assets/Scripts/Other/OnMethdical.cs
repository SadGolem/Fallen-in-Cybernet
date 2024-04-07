using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnMethdical : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject button;


    void Update()
    {
        if (text.text == "*вам установили чип*")
            button.SetActive(true);
    }
}
