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

    
    private int IndexDialog = 0;

    private bool isTyping;
    public bool _isReadingToGoNextScene = true;

    private void Start()
    {
        Invoke("WriteDialog", 2f);
        
        /*dialogSkipButton.onClick.AddListener(OnButtonClick);*/
    }

    public void SkipDialog()
    {
        if (IndexDialog != characters.Count - 1)
        {
            if (isTyping)
            {
                /*if (SceneManager.sceneCount == 1 || SceneManager.sceneCount == 2) return;*/
                StopAllCoroutines();
                sentencesText.text = "";
                sentencesText.text = characters[IndexDialog].dialogText[characters[IndexDialog].indexDialog];
                isTyping = false;
            }
            else
            {
                IndexDialog++;
                WriteDialog();
            }
        }
        else
            SwapScene();
    }

    private void SetCharacter(string name, Sprite icon/*, AudioClip audioClip*/)
    {
        nameText.text = name;
        iconImage.sprite = icon;
        /*this.audioClip = audioClip;*/
    }

    private void WriteDialog()
    {
        if (IndexDialog > characters.Count - 1)
            return;

        TextDialog(characters[IndexDialog].dialogText[characters[IndexDialog].indexDialog],
            characters[IndexDialog].nameCharacter, characters[IndexDialog].iconImage, characters[IndexDialog].sound);
    }

    private void TextDialog(string text, string name, Sprite image, List<AudioClip> sound)
    {
        SetCharacter(name, image/*, sound*/);
        StartCoroutine(TypeSentence(text, sentencesText));
    }

    private IEnumerator TypeSentence(string sentence, TextMeshProUGUI texBox)
    {
        sentencesText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            isTyping = true;
            sentencesText.text += letter;
            if (letter != ' ')
                audioSource.PlayOneShot(RandomElementsSelector<AudioClip>.SelectRandomElement(characters[IndexDialog].sound)); //нада сделать звук
            yield return new WaitForSeconds(0.15f);
        }
        isTyping = false;
    }

    void SwapScene()
    {
        if (_isReadingToGoNextScene)
            _gameController.SwitchScene();
        else
            _dialogWindow.SetActive(false);

    }
}
