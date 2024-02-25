using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    [SerializeField] private List<Character> characters; 
    [SerializeField] private Image iconImage;
    public Button dialogSkipButton;
    public event Action dialogSkipButtonEvent;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public TextMeshProUGUI sentencesText;
    public TextMeshProUGUI nameText;

    
    private int IndexDialog = 0;

    private bool isTyping;

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
                StopAllCoroutines();
                sentencesText.text = characters[IndexDialog].dialogText[characters[IndexDialog].indexDialog];
                isTyping = false;
            }
            else
            {
                IndexDialog++;
                WriteDialog();
            }
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
        if (IndexDialog > characters.Count - 1)
            return;

        TextDialog(characters[IndexDialog].dialogText[characters[IndexDialog].indexDialog],
            characters[IndexDialog].nameCharacter, characters[IndexDialog].iconImage, characters[IndexDialog].sound);
    }

    private void TextDialog(string text, string name, Sprite image, AudioClip sound)
    {
        SetCharacter(name, image/*, sound*/);
        StartCoroutine(TypeSentence(text, sentencesText, sound));
    }

    private IEnumerator TypeSentence(string sentence, TextMeshProUGUI texBox, AudioClip sound)
    {
        sentencesText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            isTyping = true;
            sentencesText.text += letter;
            audioSource.PlayOneShot(sound);
            /*SoundManager.instance.PlaySound(sound);*/ //нада сделать звук
            yield return new WaitForSeconds(0.03f);
        }
        isTyping = false;
    }

}
