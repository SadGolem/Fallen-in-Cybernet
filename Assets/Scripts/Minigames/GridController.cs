using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridController : MonoBehaviour
{
    // Start is called before the first frame update
    private GridCreater gridCreater;
    private int attemptCounter = 0; // Счетчик попыток
    private int selectedColumnIndex = 0; // Инициализируем выбранный индекс столбца
    private bool checkButtonPressed = false; // Состояние кнопки "Проверить"
    public GameObject[,] grid;
    public static GridController instance;
    public Cell currentCell;
    public GameObject gridLayout;
    Transform[,] layoutGroup;
    private List<Cell> cells = new List<Cell>();
    [SerializeField] public bool isDoublePer;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private bool isTutorial;

    private void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        gridCreater = GridCreater.instance;
        grid = gridCreater.grid;
        GetLayout();
        /*string s = "";*/
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int index = i * cols + j;
                if (index < gridLayout.transform.childCount)
                {
                    layoutGroup[i, j] = gridLayout.transform.GetChild(index);
                    /*s += layoutGroup[i, j].GetComponent<Cell>().position + " ";*/
                }

            }
            /*s += "\n";*/
        }
        /*Debug.Log(s);*/

        Cell[] foundCells = FindObjectsOfType<Cell>();
        cells.AddRange(foundCells);
    }

    void GetLayout()
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        layoutGroup = new Transform[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int index = i * cols + j;
                if (index < grid.Length)
                {
                    layoutGroup[i, j] = grid[i, j].transform;
                    layoutGroup[i, j].GetComponent<Cell>().position = (i, j);

                }
            }
        }
        /*Debug.Log(s);*/
    }
    public void MoveColumnLeft()
    {
        int columnIndex = currentCell.position.Item2;

        if (columnIndex <= 0)
            return;

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            if (columnIndex - 1 >= 0)
            {
                // Меняем местами объекты в grid
                var temp = grid[i, columnIndex];
                grid[i, columnIndex] = grid[i, columnIndex - 1];
                grid[i, columnIndex - 1] = temp;

                // Обновляем позиции объектов
                var tempPos = grid[i, columnIndex].transform.position;
                grid[i, columnIndex].transform.position = grid[i, columnIndex - 1].transform.position;
                grid[i, columnIndex - 1].transform.position = tempPos;
            }
        }
        // Обновляем layoutGroup, чтобы отразить новый порядок объектов
        GetLayout();
    }

    public void MoveColumnRight()
    {
        int columnIndex = currentCell.position.Item2;

        // Проверяем, не является ли текущий столбец крайним справа
        if (columnIndex >= grid.GetLength(1) - 1)
            return;

        // Меняем местами объекты в grid для каждой строки
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            // Меняем местами объекты в grid
            var temp = grid[i, columnIndex];
            grid[i, columnIndex] = grid[i, columnIndex + 1];
            grid[i, columnIndex + 1] = temp;

            // Обновляем позиции объектов
            var tempPos = grid[i, columnIndex].transform.position;
            grid[i, columnIndex].transform.position = grid[i, columnIndex + 1].transform.position;
            grid[i, columnIndex + 1].transform.position = tempPos;
        }

        // Обновляем layoutGroup, чтобы отразить новый порядок объектов
        GetLayout();
    }


    public void MoveRowUp()
    {
        if (!isDoublePer) return;
            int rowIndex = currentCell.position.Item1;

        // Проверяем, не является ли текущая строка крайней сверху
        if (rowIndex <= 0)
            return;

        // Меняем местами объекты в grid для каждого столбца
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            // Меняем местами объекты в grid
            var temp = grid[rowIndex, j];
            grid[rowIndex, j] = grid[rowIndex - 1, j];
            grid[rowIndex - 1, j] = temp;

            // Обновляем позиции объектов
            var tempPos = grid[rowIndex, j].transform.position;
            grid[rowIndex, j].transform.position = grid[rowIndex - 1, j].transform.position;
            grid[rowIndex - 1, j].transform.position = tempPos;
        }

        // Обновляем layoutGroup, чтобы отразить новый порядок объектов
        GetLayout();
    }

    public void MoveRowDown()
    {
        if (!isDoublePer) return;
        int rowIndex = currentCell.position.Item1;

        // Проверяем, не является ли текущая строка крайней снизу
        if (rowIndex >= grid.GetLength(0) - 1)
            return;

        // Меняем местами объекты в grid для каждого столбца
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            // Меняем местами объекты в grid
            var temp = grid[rowIndex, j];
            grid[rowIndex, j] = grid[rowIndex + 1, j];
            grid[rowIndex + 1, j] = temp;

            // Обновляем позиции объектов
            var tempPos = grid[rowIndex, j].transform.position;
            grid[rowIndex, j].transform.position = grid[rowIndex + 1, j].transform.position;
            grid[rowIndex + 1, j].transform.position = tempPos;
        }

        // Обновляем layoutGroup, чтобы отразить новый порядок объектов
        GetLayout();
    }
    private void Update()
    {
        CheckSolution();
    }

    void CheckSolution() // В ПРОЦЕССЕ ДОРАБОТКИ
    {
        string correctSolutionString = gridCreater.gridAnswer; // Строка ответ (без пробелов) \/ СТРОГО 25 СИМВОЛОВ
        string result = BuildResultString();
        if (result == correctSolutionString)
        {
            if (attemptCounter == 0)
                gridCreater.PerfectSolution = true; // Флаг для выдачи достижения и окончания сцены
            else
                gridCreater.CorrectSolution = true; // Флаг для окончания сцены (я честно хз, как реализуется переход по сценам, нужна хелпа)
            Debug.Log("Решение правильное");
            winWindow.SetActive(true);

            if (!isTutorial && HintMaker.instance.countUsedHints < 1)
            {
                AchievementControl.instance.Clever();
            }
        }
        else
        {
            // Увеличение счетчика при неправильном решении
            attemptCounter++;
            Debug.Log("Решение неправильное");
        }
    }

    string BuildResultString() // В ПРОЦЕССЕ ДОРАБОТКИ
    {
        string result = "";
        for (int j = 0; j < grid.GetLength(1); j++) // Итерация по столбцам
        {
            for (int i = 0; i < grid.GetLength(0); i++) // Итерация по строкам
            {
                // Здесь предполагается, что у каждой ячейки есть компонент, содержащий символ этой ячейки.
                string cellComponent = grid[j, i].GetComponent<Cell>().GetComponentInChildren<TextMeshProUGUI>().text;
                if (cellComponent != null)
                {
                    // Предполагается, что у компонента Cell есть метод ToString() или свойство для получения символа
                    result += cellComponent.ToString();
                }
            }
        }
        Debug.Log("Результат:" + result);
        return result;
    }
}
