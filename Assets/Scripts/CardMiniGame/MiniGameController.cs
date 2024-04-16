using UnityEngine;
using System.Collections.Generic;

public class MiniGameController : MonoBehaviour
{
    public List<CardData> answers; // Список всех ответов, который вы заполните в Inspector
    public int scoreCategoryA; // Счётчик правильных ответов для категории A
    public int scoreCategoryB; // Счётчик правильных ответов для категории B
    public int playerHP; // Здоровье игрока

    // Метод для вызова, когда игрок перетащил ответ в категорию A или B
    public void ChooseCategory(CardData chosenAnswer, bool isCategoryAChosen)
    {
        if (chosenAnswer.isCategoryA == isCategoryAChosen)
        {
            // Ответ правильный
            scoreCategoryA++;
        }
        else
        {
            // Ответ неправильный
            scoreCategoryB++;
            playerHP--; // Уменьшаем здоровье игрока
        }

        // Удалить ответ из списка доступных ответов
        answers.Remove(chosenAnswer);

        // Проверить, остались ли еще вопросы
        if (answers.Count == 0)
        {
            EndGame(); // Завершить игру, если вопросы закончились
        }
    }

    void EndGame()
    {
        // Здесь логика для завершения игры
        Debug.Log("Игра завершена. Счёт: " + scoreCategoryA);
    }
}