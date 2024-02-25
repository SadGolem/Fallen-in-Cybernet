using UnityEngine;

public class GridCreater : MonoBehaviour
{
    public GameObject cellPrefab; // префаб для создания ячейки \/ НЕОБХОДИМО ПРИВЯЗАТЬ TextMesh
    public Sprite normalSprite; // обычная картинка для ячейки

    private string gridContent = "_ДАЁТЕ_ЗНАНАНИЕ_ЗНАНИНИЕ_З"; // Строка из 25 символов \/ ВНЕСТИ СЮДА ЗАШИФРОВАННУЮ СТРОКУ

    public GameObject[,] grid { get; private set; } // двумерный массив для хранения ячеек

    public bool PerfectSolution;
    public bool CorrectSolution;

    public static GridCreater instance;

    private void Awake()
    {
        instance = this;
    }

    public void CreateGrid()
    {
        grid = new GameObject[5, 5];
        int stringIndex = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                // Создание ячейки из префаба, который уже содержит нужный спрайт
                GameObject cell = Instantiate(cellPrefab, new Vector3(i, j, 0), Quaternion.identity);
                // cell.GetComponent<SpriteRenderer>().sprite = normalSprite; // -- просто не нужен. По крайней мере пока что.

                // Установка текста для TextMesh, если это необходимо
                TextMesh textMesh = cell.GetComponent<TextMesh>();
                if (textMesh != null && stringIndex < gridContent.Length)
                {
                    textMesh.text = gridContent[stringIndex].ToString();
                    stringIndex++;
                }

                grid[i, j] = cell;
            }
        }
    }
}



