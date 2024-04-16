using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class CardGameController : MonoBehaviour
{
    public List<CardData> answers; // Список всех ответов, который вы заполните в Inspector
    public int scoreCategoryA; // Счётчик правильных ответов для категории A
    public int scoreCategoryB; // Счётчик правильных ответов для категории B
    public int score; // Счётчик правильных ответов для категории B
    public int playerHP; // Здоровье игрока
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

    // Метод для вызова, когда игрок перетащил ответ в категорию A или B
    public void ChooseCategory(CardData chosenAnswer, bool isCategoryAChosen)
    {
        if (currentAnswer.isTrue == isCategoryAChosen)
        {
            // Ответ правильный
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
            // Ответ неправильный
            audioSource.PlayOneShot(notRight);
            animator.Play("not right", 0, 0.2f);
            scoreCategoryB++;
            score = 0;

            currentCard.gameObject.SetActive(false);
            playerHP--; // Уменьшаем здоровье игрока
            if (playerHP > 0)
                hp.ElementAt(playerHP).SetActive(false);
            if (playerHP == 0)
            {
                Time.timeScale = 0f;
                loseWindow.SetActive(true);
            }
            SpawnNewCard();

        }

        // Удалить ответ из списка доступных ответов
        answers.Remove(currentAnswer);

        // Проверить, остались ли еще вопросы
        if (answers.Count == 0)
        {
            EndGame(); // Завершить игру, если вопросы закончились
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
        // Здесь логика для завершения игры
        winWindow.SetActive(true);
        Debug.Log("Игра завершена. Счёт: " + scoreCategoryA);
    }
}