using UnityEngine;
using UnityEngine.EventSystems;

public class MagickGridController : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"Cell {gameObject.name} clicked.");
            // Добавить код для обработки и сохранения результата нажатия
        }

    }

