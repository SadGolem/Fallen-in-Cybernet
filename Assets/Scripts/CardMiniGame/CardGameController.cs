using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class CardGameController : MonoBehaviour
{
    public List<CardData> answers; // ������ ���� �������, ������� �� ��������� � Inspector
    public int scoreCategoryA; // ������� ���������� ������� ��� ��������� A
    public int scoreCategoryB; // ������� ���������� ������� ��� ��������� B
    public int score; // ������� ���������� ������� ��� ��������� B
    public int playerHP; // �������� ������
    public List<GameObject> hp;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private Card prefab;
     private Card currentCard;
     private CardData currentAnswer;
    [SerializeField] private Transform cards;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip right;
    [SerializeField] private AudioClip notRight;
    [SerializeField] private Animator animator;
    [SerializeField] private AchievementControl achievementControl;

    public static CardGameController instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1f;
        SpawnNewCard();
    }

    // ����� ��� ������, ����� ����� ��������� ����� � ��������� A ��� B
    public void ChooseCategory(CardData chosenAnswer, bool isCategoryAChosen)
    {
        if (currentAnswer.isTrue == isCategoryAChosen)
        {
            // ����� ����������
            audioSource.PlayOneShot(right);
            animator.Play("right answer", 0, 0.2f);
            ++score;

            if (score >= 3)
            {
                AchievementControl.isThreeOnARow = true;
                achievementControl.ThreeOnARow();
            }
            scoreCategoryA++;
            currentCard.gameObject.SetActive(false);
            SpawnNewCard();

            
        }
        else
        {
            // ����� ������������
            audioSource.PlayOneShot(notRight);
            animator.Play("not right", 0, 0.2f);
            scoreCategoryB++;
            score = 0;

            currentCard.gameObject.SetActive(false);
            playerHP--; // ��������� �������� ������
            if (playerHP > 0)
                hp.ElementAt(playerHP).SetActive(false);
            if (playerHP == 0)
            {
                Time.timeScale = 0f;
                loseWindow.SetActive(true);
            }
            SpawnNewCard();

        }

        // ������� ����� �� ������ ��������� �������
        answers.Remove(currentAnswer);

        // ���������, �������� �� ��� �������
        if (answers.Count == 0)
        {
            EndGame(); // ��������� ����, ���� ������� �����������
        }
    }

    private void SpawnNewCard()
    {
        var card = Instantiate(prefab, cards.position, Quaternion.identity, cards);
        card.GetComponent<Image>().sprite = sprites.ElementAt(Random.Range(0, sprites.Count));
        var answer = answers.ElementAt(Random.Range(0, answers.Count));
        card.name.text = answer.nameAnswer;
        card.descripton.text = answer.answerText;
        card.CardData = answer;
        currentCard = card;
        currentAnswer = answer;
    }

    void EndGame()
    {
        // ����� ������ ��� ���������� ����
        winWindow.SetActive(true);
        Debug.Log("���� ���������. ����: " + scoreCategoryA);
    }
}