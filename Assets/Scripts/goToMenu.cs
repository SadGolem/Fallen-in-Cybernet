using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        if(text.text == "Большое спасибо за прохождение!" && Input.anyKey)
        {
            SceneManager.LoadScene(0);
        }

    }
}
