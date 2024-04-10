using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    [SerializeField] private List<Character> characters; 
    [SerializeField] private Image iconImage;
    [SerializeField] private GameController _gameController;
    [SerializeField] private GameObject _dialogWindow;
    public Button dialogSkipButton;
    public event Action dialogSkipButtonEvent;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public TextMeshProUGUI sentencesText;
    public TextMeshProUGUI nameText;

    
    private int indexDialog = 0;

    private bool isTyping;
    private bool goBreakOut;
    public bool _isReadingToGoNextScene = true;

    private void Start()
    {
        Invoke("WriteDialog", 2f);
        
        /*dialogSkipButton.onClick.AddListener(OnButtonClick);*/
    }

    public void SkipDialog()
    {
        if (indexDialog != characters.Count - 1)
        {
            /*if (isTyping)
            {
                *//*if (SceneManager.sceneCount == 1 || SceneManager.sceneCount == 2) return;*//*
                StopCoroutine(TypeSentence(characters[IndexDialog].dialogText[characters[IndexDialog].indexDialog], sentencesText));
                sentencesText.text = "";
                sentencesText.text = characters[IndexDialog].dialogText[characters[IndexDialog].indexDialog];
                isTyping = false;
            }
            else
            {
                StopAllCoroutines();
                sentencesText.text = characters[IndexDialog].dialogText[characters[IndexDialog].indexDialog];
                goBreakOut = true;
                    IndexDialog++;
                    WriteDialog();
                
            }*/

            StopAllCoroutines();
            if (sentencesText.text == characters[indexDialog].dialogText[characters[indexDialog].indexDialog])
            {
                ++indexDialog;

                WriteDialog();
            }
            else
            {
                sentencesText.text = characters[indexDialog].dialogText[characters[indexDialog].indexDialog];
            }
        }
        else
        {
            if (!isTyping)
                SwapScene();
        }
    }

    private void SetCharacter(string name, Sprite icon/*, AudioClip audioClip*/)
    {
        nameText.text = name;
        iconImage.sprite = icon;
        /*this.audioClip = audioClip;*/
    }

    private void WriteDialog()
    {
        if (indexDialog > characters.Count - 1)
            return;

        TextDialog(characters[indexDialog].dialogText[characters[indexDialog].indexDialog],
            characters[indexDialog].nameCharacter, characters[indexDialog].iconImage, characters[indexDialog].sound);
    }

    private void TextDialog(string text, string name, Sprite image, List<AudioClip> sound)
    {
        SetCharacter(name, image/*, sound*/);
        StartCoroutine(TypeSentence(text, sentencesText));
    }

    private IEnumerator TypeSentence(string sentence, TextMeshProUGUI texBox)
    {
        
        isTyping = true;
        sentencesText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if (goBreakOut) break;
            sentencesText.text += letter;
            if (letter != ' ')
                audioSource.PlayOneShot(RandomElementsSelector<AudioClip>.SelectRandomElement(characters[indexDialog].sound)); //нада сделать звук
            yield return new WaitForSeconds(0.15f);
        }
        isTyping = false;
        goBreakOut = false;
    }

    void SwapScene()
    {
        if (_isReadingToGoNextScene)
            _gameController.SwitchScene();
        else
            _dialogWindow.SetActive(false);

    }
}
