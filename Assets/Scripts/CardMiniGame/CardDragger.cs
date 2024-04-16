using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragger : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 originalPosition;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Card card;

    void Start()
    {
        card = GetComponent<Card>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // —ледуем за мышью по горизонтали
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // ”честь scaleFactor если канвас не в режиме Screen Space - Overlay
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ќпредел€ем, куда "бросили" карту (влево или вправо)
        bool isChosenLeft = rectTransform.anchoredPosition.x < originalPosition.x;

        // ¬ зависимости от того, куда карту бросили, вызываем соответствующий метод в CardGameController
        if (isChosenLeft)
        {
            CardGameController.instance.ChooseCategory(card.CardData, true); // если бросили влево
        }
        else
        {
            CardGameController.instance.ChooseCategory(card.CardData, false); // если бросили вправо
        }

        // ¬озвращаем карту на исходную позицию
        rectTransform.anchoredPosition = originalPosition;
    }
}