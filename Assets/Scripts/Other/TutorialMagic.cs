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
        if (_text.text == "Перед тобой два поля.")
        {
            animField.SetActive(true);
        }
        else if(_text.text == "На первом расположены цифры.")
        {
            animField.SetActive(false);
            animNumbers.SetActive(true);
        }
        else if (_text.text == "Второе поле пустое.")
        {
            animNumbers.SetActive(false);
            
            animInput.SetActive(true);
        }
        else if (_text.text == "Заполни второе поле шифртекстом слева направо снизу вверх по одному символу.")
        {
            animInput.SetActive(false);
            animCode.SetActive(true);
        }
        else if (_text.text.Contains("КОДОВУЮ"))
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
