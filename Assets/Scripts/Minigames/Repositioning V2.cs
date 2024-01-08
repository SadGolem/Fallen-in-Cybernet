using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreater : MonoBehaviour
{
    public GameObject cellPrefab; // префаб для создания ячейки \/ НЕОБХОДИМО ПРИВЯЗАТЬ TextMesh
    public Sprite normalSprite; // обычная картинка для ячейки

    private string gridContent = ""; // Строка из 25 символов \/ ВНЕСТИ СЮДА ЗАШИФРОВАННУЮ СТРОКУ

    public GameObject[,] grid { get; private set; } // двумерный массив для хранения ячеек

    private bool PerfectSolution;
    private bool CorrectSolution;

    public static GridCreater instance;

    private void Awake()
    {
        instance = this;
    }

    void CreateGrid()
    {
        grid = new GameObject[5, 5];
        int stringIndex = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector3(i, j, 0), Quaternion.identity);
                cell.GetComponent<SpriteRenderer>().sprite = normalSprite;

                // Установка текста для TextMesh
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

    public class GridController : MonoBehaviour
    {
        // Start is called before the first frame update
        private GridCreater gridCreater;
        private int attemptCounter = 0; // Счетчик попыток
        private int selectedColumnIndex = 0; // Инициализируем выбранный индекс столбца
        private bool checkButtonPressed = false; // Состояние кнопки "Проверить"

        void Start()
        {
            gridCreater = GridCreater.instance;
            gridCreater.CreateGrid();
        }

        // Update is called once per frame
        void Update()
        {
            // Обработка ввода для перестановки столбцов
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveColumnLeft(selectedColumnIndex);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveColumnRight(selectedColumnIndex);
            }

            // Обработка нажатия кнопки "Проверить"
            if (checkButtonPressed)
            {
                CheckSolution(); // Проверить состояние кнопки
                checkButtonPressed = false; // Сброс состояния кнопки
            }
        }

        void MoveColumnLeft(int columnIndex)
        {
            if (columnIndex <= 0)
            {
                // Если это крайний левый столбец, не делать ничего
                return;
            }

            // Перестановка столбца влево
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                GameObject temp = grid[i, columnIndex]; // Сохраняем текущий столбец
                grid[i, columnIndex] = grid[i, columnIndex - 1]; // Перемещаем левый столбец в текущую позицию
                grid[i, columnIndex - 1] = temp; // Помещаем сохраненный столбец в левую позицию

                // Перемещаем GameObjects на сцене
                grid[i, columnIndex].transform.position = new Vector2(columnIndex, i);
                grid[i, columnIndex - 1].transform.position = new Vector2(columnIndex - 1, i);
            }
        }
        void MoveColumnRight(int columnIndex)
        {
            int maxColumnIndex = grid.GetLength(1) - 1; // Получаем максимально возможный индекс столбца
            if (columnIndex >= maxColumnIndex)
            {
                // Если это крайний правый столбец, не делать ничего
                return;
            }

            // Перестановка столбца вправо
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                GameObject temp = grid[i, columnIndex]; // Сохраняем текущий столбец
                grid[i, columnIndex] = grid[i, columnIndex + 1]; // Перемещаем правый столбец в текущую позицию
                grid[i, columnIndex + 1] = temp; // Помещаем сохраненный столбец в правую позицию

                // Обновляем позиции GameObjects на сцене
                grid[i, columnIndex].transform.position = new Vector2(columnIndex, i);
                grid[i, columnIndex + 1].transform.position = new Vector2(columnIndex + 1, i);
            }
        }

        void CheckSolution() // В ПРОЦЕССЕ ДОРАБОТКИ
        {
            string correctSolutionString = ""; // Строка ответ (без пробелов) \/ СТРОГО 25 СИМВОЛОВ
            string result = BuildResultString();
            if (result == correctSolutionString)
            {
                if (attemptCounter == 0)
                    PerfectSolution = true; // Флаг для выдачи достижения и окончания сцены
                else
                    CorrectSolution = true; // Флаг для окончания сцены (я честно хз, как реализуется переход по сценам, нужна хелпа)
            }
            else
            {
                // Увеличение счетчика при неправильном решении
                attemptCounter++;
            }
        }

        string BuildResultString() // В ПРОЦЕССЕ ДОРАБОТКИ
        {
            string result = "";
            for (int j = 0; j < gridCreater.grid.GetLength(1); j++) // Итерация по столбцам
            {
                for (int i = 0; i < gridCreater.grid.GetLength(0); i++) // Итерация по строкам
                {
                    // Здесь предполагается, что у каждой ячейки есть компонент, содержащий символ этой ячейки.
                    Cell cellComponent = gridCreater.grid[i, j].GetComponent<Cell>();
                    if (cellComponent != null)
                    {
                        // Предполагается, что у компонента Cell есть метод ToString() или свойство для получения символа
                        result += cellComponent.ToString();
                    }
                }
            }
            return result;
        }
    }
}
