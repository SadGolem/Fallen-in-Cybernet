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
        // ������� �� ����� �� �����������
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // ������ scaleFactor ���� ������ �� � ������ Screen Space - Overlay
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ����������, ���� "�������" ����� (����� ��� ������)
        float dragDistance = rectTransform.anchoredPosition.x - originalPosition.x;

        if (Mathf.Abs(dragDistance) < 150f)
        { rectTransform.anchoredPosition = originalPosition;
            return;
        }
        bool isChosenLeft = rectTransform.anchoredPosition.x < originalPosition.x;

        // � ����������� �� ����, ���� ����� �������, �������� ��������������� ����� � CardGameController
        if (isChosenLeft)
        {
            CardGameController.instance.ChooseCategory(card.CardData, true); // ���� ������� �����
        }
        else
        {
            CardGameController.instance.ChooseCategory(card.CardData, false); // ���� ������� ������
        }

        // ���������� ����� �� �������� �������
        rectTransform.anchoredPosition = originalPosition;
    }
}