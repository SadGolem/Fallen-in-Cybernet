using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerGameManager : MonoBehaviour
{
    [Header("Dialog")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TMP_InputField inputText;
    [Header("SliderAreaMessage")]
    [Header("Objects")]
    [SerializeField] private GameObject mailWindow;
    [SerializeField] private GameObject prefabUser;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject winWindows;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip newMessage;
    [SerializeField] private AudioClip incorrectAnswer;
    [SerializeField] private AudioSource audioSource;
    private string correctAnswer;
    private List<(string, string, string)> users;
    private List<(string, string, string)> usersUse = new List<(string, string, string)>();
    private int indexUse = 0;
    private static int correct;
    private int count;
    private List<string> correctAnswers = new List<string>();

    void GetAllMessage()
    {
        users = UserBase.users;
        users = Shafler.Shuffle(users);
        count = users.Count;
    }

    private void Start()
    {
        GetAllMessage();
        SpawnerMessage();
    }

    void SpawnerMessage()
    {
        var mail = Instantiate(prefabUser, parent.transform);

        var user = mail.GetComponent<User>();
        int index = UnityEngine.Random.Range(0, users.Count-1);
        var button = mail.GetComponent<Button>();
        user.nameText.text = users[index].Item1;
        user.description.text = users[index].Item2.Substring(0,10) + "...";
        user.index = indexUse;
        correctAnswer = users[index].Item3;
        nameText.text = user.nameText.text;
        dialogText.text = users[index].Item2;
        button.onClick.AddListener(() => {ShowMessage(user); });
        
        usersUse.Add((users[index].Item1, users[index].Item2, users[index].Item3));
        indexUse++;
        //удаляем уже использованное сообщение
        users.RemoveAt(index);
        audioSource.PlayOneShot(newMessage);
    }

    public void ShowMessage(User user)
    {
        int index = user.index;
        correctAnswer = usersUse[index].Item3;
        nameText.text = user.nameText.text;
        dialogText.text = usersUse[index].Item2;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }
    }
    private void CheckAnswer()
    {
        string correctSolutionString = correctAnswer.Replace(" ", "").ToLower();
        string userAnswer = inputText.text.Replace(" ", "").ToLower();

        if (userAnswer == correctSolutionString)
        {
            correctAnswers.Add(correctAnswer);
            SpawnerMessage();
            correct++;
            inputText.text = "";
        }
        else
        {
            animator.Play("not right anwer text anim", 0,0.1f);
            audioSource.PlayOneShot(incorrectAnswer);
        }

        if (correct == count - 1)
        {
            winWindows.SetActive(true);
        }

    }

}
