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
    [SerializeField] private GameController _gameController;
    [SerializeField] private GameObject _dialogWindow;
    public Button dialogSkipButton;
    public event Action dialogSkipButtonEvent;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public TextMeshProUGUI sentencesText;
    public TextMeshProUGUI nameText;

    
    private int indexDialog = 0;

    private bool goBreakOut;
    public bool _isReadingToGoNextScene = true;

    private void Start()
    {
        WriteDialog();
    }

    public void SkipDialog()
    {
        if (indexDialog <= characters.Count - 1)
        {
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

        if (indexDialog == characters.Count)
        {
            SwapScene();
        }
    }

    private void SetCharacter(string name, Sprite icon/*, AudioClip audioClip*/)
    {
        nameText.text = name;
        iconImage.sprite = icon;
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
        SetCharacter(name, image);
        StartCoroutine(TypeSentence(text, sentencesText));
    }

    private IEnumerator TypeSentence(string sentence, TextMeshProUGUI texBox)
    {
        sentencesText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if (goBreakOut) break;
            sentencesText.text += letter;
            if (letter != ' ')
                audioSource.PlayOneShot(RandomElementsSelector<AudioClip>.SelectRandomElement(characters[indexDialog].sound)); //нада сделать звук
            yield return new WaitForSeconds(0.08f);
        }
        goBreakOut = false;
    }

    void SwapScene()
    {
        if (_isReadingToGoNextScene)
            _gameController.SwitchScene();
        else
            _dialogWindow.SetActive(false);

    }

    public void SetDialogIndex(int index)
    {
        indexDialog = index;
    }

    public int GetCurrentDialogIndex()
    {
        // Возвращаем текущий индекс диалога
        return indexDialog;
    }
}
