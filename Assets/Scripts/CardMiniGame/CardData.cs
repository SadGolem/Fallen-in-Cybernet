using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Quiz/Answer")]
public class CardData : ScriptableObject
{
    public string answerText; // Текст ответа
    public string nameAnswer; // Текст ответа
    public bool isTrue; // Принадлежит ли ответ категории A (если нет, то категории B)
}

