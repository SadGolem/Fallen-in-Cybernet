using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    private List<(string, string)> questions = new List<(string, string)>();
    private List<string> wrongAnswers = new List<string>();
    private List<string> answers = new List<string>();

    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private List<Button> buttonsInInspector;
    [SerializeField] private List<(Button, TextMeshProUGUI)> buttons = new List<(Button, TextMeshProUGUI)>();
    [SerializeField] private Button previous;
    [SerializeField] private Button next;
    [SerializeField] private Button endTestBtn;
    [SerializeField] private GameObject endTest;
    [SerializeField] private GameObject endTestWindow;
    [SerializeField] private TextMeshProUGUI numberQuestion;

    private int indexQuestion = 0;
    private int correctAnwerCount = 0;
    private List<(int, string, string)> answersPlayer = new List<(int, string, string)>(); // индекс, неправильный ответ, правльный ответ

    private void Awake()
    {
        foreach (var btn in buttonsInInspector)
        {
            var text = btn.GetComponentInChildren<TextMeshProUGUI>();
            if (btn != null)
                buttons.Add((btn, text));
        }
    }

    private void Update()
    {
        numberQuestion.text = "Вопрос: " + (indexQuestion + 1).ToString();
    }

    private void Start()
    {
        previous.onClick.AddListener(PreviousQuestion);
        next.onClick.AddListener(NextQuestion);
        endTestBtn.onClick.AddListener(EndTest);

        foreach (var btn in buttonsInInspector)
            btn.onClick.AddListener(() => CheckAnswer(btn));
        StartTest(0);
    }

    public void StartTest(int indexTest)
    {
        GetTestQuestions(indexTest);
        GetAnswers();
        GenerateListWrongAnswers();
        RandomizeQuestion();
        SetQuestionInTheTextField(indexQuestion);
        SetAnswersInTheTextField();
    }

    private void GetTestQuestions(int level)
    {
        switch (level)
        {
            case 0:
                questions = TestBase.FirstTest;
                break;
            case 1:
                questions = TestBase.SecondTest;
                break;
            case 2:
                questions = TestBase.ThirdTest;
                break;
        }
    }

    private void SetQuestionInTheTextField(int index)
    {
        questionText.text = questions[index].Item1;
    }

    private void SetAnswersInTheTextField()
    {
        var wrongButtonsRandom = RandomElementsSelector<Button, TextMeshProUGUI>.SelectRandomFourElement(buttons);
        wrongButtonsRandom[0].Item2.text = questions[indexQuestion].Item2;
        var wrongAnswersRandom = RandomElementsSelector<string>.SelectRandomThreeElement(wrongAnswers);
        wrongButtonsRandom[1].Item2.text = wrongAnswersRandom[0];
        wrongButtonsRandom[2].Item2.text = wrongAnswersRandom[1];
        wrongButtonsRandom[3].Item2.text = wrongAnswersRandom[2];
    }

    private void GetAnswers()
    {
        foreach (var answer in questions)
        {
            answers.Add(answer.Item2);
        }
    }

    private void GenerateListWrongAnswers()
    {
        wrongAnswers = Shafler.Shuffle(answers);
    }

    private void RandomizeQuestion()
    {
        questions = Shafler.Shuffle(questions);

        SetAnwersPlayer();
    }

    private void SetAnwersPlayer()
    {
        int i = 0;
        foreach (var q in questions)
        {
            answersPlayer.Add((i, "", q.Item2));
        }
    }

    private void NextQuestion()
    {
        if (indexQuestion < questions.Count - 1)
        {
            SetQuestionInTheTextField(++indexQuestion);
            SetAnswersInTheTextField();
        }
        if (indexQuestion == questions.Count - 1)
        {
            next.gameObject.SetActive(false);
            endTest.SetActive(true);
        }
        previous.gameObject.SetActive(true);
    }

    private void PreviousQuestion()
    {
        if (indexQuestion > 0)
        {
            SetQuestionInTheTextField(--indexQuestion);
            SetAnswersInTheTextField();

            next.gameObject.SetActive(true);
            endTest.SetActive(false);
            previous.gameObject.SetActive(true);
        }
        if (indexQuestion == 0)
        {
            previous.gameObject.SetActive(false);
        }
    }

    private void CheckAnswer(Button btn)
    {
        var answer = btn.GetComponentInChildren<TextMeshProUGUI>();

        var currentAnswer = answersPlayer.ElementAt(indexQuestion);
        currentAnswer.Item2 = answer.text.ToString();
        answersPlayer[indexQuestion] = currentAnswer;

        if (answer.text == questions[indexQuestion].Item2)
        {
            Debug.Log("Выбран правильный ответ");
            

            NextQuestion();
        }
        else
        {
            Debug.Log("Выбран неправильный ответ");
            NextQuestion();
        }
    }

    private void EndTest()
    {
        endTestWindow.SetActive(true);

        ShowResultText();
    }

    private void ShowResultText()
    {
        resultText.text = "Вопросов было: " + questions.Count.ToString() +  "\n" +
            "Правильных ответов: " + CalcAnswerRight().ToString() + "\n";
        resultText.text += AnswerNonRight();
    }


    private int CalcAnswerRight()
    {
        var right = 0;
        foreach (var answer in answersPlayer)
        {
            if (answer.Item2 == answer.Item3)
                right++;
        }
        return right;
    }


    private string AnswerNonRight()
    {
        string nonRight = "";
        int i = 0;
        foreach (var answer in answersPlayer)
        {
            if (answer.Item2 != answer.Item3)
                nonRight += "Вопрос: " + ++i + ", ваш ответ: " + answer.Item2
                    + ", правильный ответ: " + answer.Item3 + "\n";
        }
        return nonRight;
    }
}
