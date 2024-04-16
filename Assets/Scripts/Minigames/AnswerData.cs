using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Quiz/Answer")]
public class AnswerData : ScriptableObject
{
    public string answerText; // Текст ответа
    public bool isCategoryA; // Принадлежит ли ответ категории A (если нет, то категории B)
}

