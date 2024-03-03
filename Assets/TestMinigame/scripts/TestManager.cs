using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    private List<(string, string)> questions;
    private List<string> wrongAnswers;
    private List<string> answers;

    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private List<(Button, TextMeshProUGUI)> buttons;
    [SerializeField] private Button previous;
    [SerializeField] private Button next;

    private int indexQuestion = 0;

    private void Start()
    {
        previous.onClick.AddListener(PreviousQuestion);
        next.onClick.AddListener(NextQuestion);
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
            case 1:
                questions = TestBase.FirstTest;
                break;
            case 2:
                questions = TestBase.SecondTest;
                break;
            case 3:
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
        /*var wrongAnswersRandom = RandomElementsSelector<Button, TextMeshProUGUI>.SelectRandomThreeElement(buttons);
        wrongAnswersRandom[0].Item2.text = questions[indexQuestion].Item2;
        var wrongAnswersRandom = RandomElementsSelector<string>.SelectRandomThreeElement(wrongAnswers);
        answer1Text.text = wrongAnswersRandom[0];
        answer2Text.text = wrongAnswersRandom[1];
        answer3Text.text = wrongAnswersRandom[2];*/
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
    }

    private void NextQuestion()
    {
        if (indexQuestion < questions.Count)
        {
            SetQuestionInTheTextField(++indexQuestion);
            SetAnswersInTheTextField();
        }
    }

    private void PreviousQuestion()
    {
        if (indexQuestion > 1)
        {
            SetQuestionInTheTextField(--indexQuestion);
            SetAnswersInTheTextField();
        }
    }

}
