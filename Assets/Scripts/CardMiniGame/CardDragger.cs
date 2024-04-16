using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragger : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 originalPosition;
    private RectTransform rectTransform;

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
        bool isChosenLeft = rectTransform.anchoredPosition.x < originalPosition.x;

        // � ����������� �� ����, ���� ����� �������, �������� ��������������� ����� � CardGameController
        if (isChosenLeft)
        {
            CardGameController.instance.ChooseCategory(myCardData, true); // ���� ������� �����
        }
        else
        {
            CardGameController.instance.ChooseCategory(myCardData, false); // ���� ������� ������
        }

        // ���������� ����� �� �������� �������
        rectTransform.anchoredPosition = originalPosition;
    }
}