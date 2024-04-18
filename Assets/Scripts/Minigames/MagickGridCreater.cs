using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagickGridCreater : MonoBehaviour
{
    public GameObject cellPrefab; 
    public GameObject cellPrefabInput; 
    public TextMeshProUGUI codePhrases; 
    public Sprite normalSprite; // обычная картинка для ячейки
    public List<Sprite> normalSprites = new List<Sprite>();
    public GameObject obj;
    public GameObject objNum;

    [SerializeField] public string gridContent = "АЕРУТНСВЧ"; // Строка из 25 символов \/ ВНЕСТИ СЮДА ЗАШИФРОВАННУЮ СТРОКУ
    [SerializeField] public string gridAnswer = "ВОСПОЛЬЗУЙТЕСЬ_ПОДСКАЗКОЙ"; // Строка из 25 символов \/ ВНЕСТИ СЮДА ЗАШИФРОВАННУЮ СТРОКУ
    [SerializeField] public string gridNumbers = "ВОСПОЛЬЗУЙТЕСЬ_ПОДСКАЗКОЙ"; // Строка из 25 символов \/ ВНЕСТИ СЮДА ЗАШИФРОВАННУЮ СТРОКУ

    public GameObject[,] gridContentArray { get; private set; } // двумерный массив для хранения ячеек
    public GameObject[,] gridContentNumbers{ get; private set; } // двумерный массив для хранения ячеек

    public bool PerfectSolution;
    public bool CorrectSolution;

    public static MagickGridCreater instance;

    private void Awake()
    {
        instance = this;
        CreateGrid();
    }

    public void CreateGrid()
    {
        gridContentArray = new GameObject[3, 3];
        gridContentNumbers = new GameObject[3, 3];

        codePhrases.text = gridContent;
        CreateContent(cellPrefab, obj.transform, gridNumbers, gridContentArray); //создание грида с номерами
        CreateContent(cellPrefabInput, objNum.transform, "", gridContentNumbers); //создание грида с инпутами
    }


    private void CreateContent(GameObject prefab, Transform obj, string content, GameObject[,] array)
    {
        int stringIndex = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject cell = Instantiate(prefab, obj.forward, Quaternion.identity, obj.transform);
                cell.GetComponent<Image>().sprite = PickRandomElement();
                /*cell.GetComponent<Cell>().position = (i, j);*/
                // Установка текста для TextMesh
                TextMeshProUGUI textMesh = cell.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null && stringIndex < content.Length)
                {
                    textMesh.text = content[stringIndex].ToString();
                    stringIndex++;
                }

                array[i, j] = cell;
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

