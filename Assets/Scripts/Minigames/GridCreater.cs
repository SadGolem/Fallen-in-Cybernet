using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridCreater : MonoBehaviour
{
    public GameObject cellPrefab; // префаб для создания ячейки \/ НЕОБХОДИМО ПРИВЯЗАТЬ TextMesh
    public Sprite normalSprite; // обычная картинка для ячейки
    public List<Sprite> normalSprites = new List<Sprite>();
    public GameObject obj;


    private string gridContent = "ЛЬЗУЙВОСПОТЕСЬ_ПОДСКАЗКОЙ"; // Строка из 25 символов \/ ВНЕСТИ СЮДА ЗАШИФРОВАННУЮ СТРОКУ

    public GameObject[,] grid { get; private set; } // двумерный массив для хранения ячеек

    public bool PerfectSolution;
    public bool CorrectSolution;

    public static GridCreater instance;

    private void Awake()
    {
        instance = this;
        CreateGrid();
    }

    public void CreateGrid()
    {
        grid = new GameObject[5, 5];
        int stringIndex = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject cell = Instantiate(cellPrefab, obj.transform.forward, Quaternion.identity, obj.transform);
                cell.GetComponent<Image>().sprite = PickRandomElement();
                cell.GetComponent<Cell>().position = (i, j);
                // Установка текста для TextMesh
                TextMeshProUGUI textMesh = cell.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null && stringIndex < gridContent.Length)
                {
                    textMesh.text = gridContent[stringIndex].ToString();
                    stringIndex++;
                }

                grid[i, j] = cell;
            }
        }

        Sprite PickRandomElement()
        {
            if (normalSprites.Count == 0)
            {
                Debug.LogError("Список пуст!");
                return null;
            }

            int randomIndex = Random.Range(0, normalSprites.Count);
            return normalSprites[randomIndex];
        }
    }
}



