using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    // Start is called before the first frame update
    private GridCreater gridCreater;
    private int attemptCounter = 0; // Счетчик попыток
    private int selectedColumnIndex = 0; // Инициализируем выбранный индекс столбца
    private bool checkButtonPressed = false; // Состояние кнопки "Проверить"
    private GameObject[,] grid;
    public static GridController instance;
    public Cell currentCell;
    public GameObject gridLayout;
    Transform[,] layoutGroup;


    private void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        gridCreater = GridCreater.instance;
        grid = gridCreater.grid;
        GetLayout();
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
                if (index < gridLayout.transform.childCount)
                {
                    layoutGroup[i, j] = gridLayout.transform.GetChild(index);
                }
            }
        }
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
   /*     int columnIndex = currentCell.position.Item2;

        if (columnIndex >= grid.GetLength(1) - 1)
        {
            // Если это крайний правый столбец, не делать ничего
            return;
        }

        // Перестановка столбца вправо
        for (int i = 0; i < gridLayout.transform.childCount / grid.GetLength(1); i++)
        {
            int currentIndex = i * grid.GetLength(1) + columnIndex;
            int rightIndex = i * grid.GetLength(1) + columnIndex + 1;

            // Обмен местами объектов в массиве для соответствия новому порядку в иерархии
            Transform temp = layoutGroup[currentIndex];
            layoutGroup[currentIndex] = layoutGroup[rightIndex];
            layoutGroup[rightIndex] = temp;

            // Обновление позиций в иерархии
            layoutGroup[currentIndex].SetSiblingIndex(rightIndex);
            layoutGroup[rightIndex].SetSiblingIndex(currentIndex);
        }*/
    }

    public void MoveRowDown()
    {
        int rowIndex = currentCell.position.Item1;

        if (rowIndex <= 0)
        {
            // Если это нижняя строка, не делаем ничего
            return;
        }

        // Перестановка строки вниз
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            GameObject temp = grid[rowIndex, j]; // Сохраняем текущую строку
            grid[rowIndex, j] = grid[rowIndex - 1, j]; // Перемещаем нижнюю строку в текущую позицию
            grid[rowIndex - 1, j] = temp; // Помещаем сохраненную строку в нижнюю позицию

            // Обновляем позиции GameObjects на сцене
            grid[rowIndex, j].transform.position = new Vector2(j, rowIndex);
            grid[rowIndex - 1, j].transform.position = new Vector2(j, rowIndex - 1);
        }
    }

    public void MoveRowUp()
    {
        int rowIndex = currentCell.position.Item1;

        if (rowIndex >= grid.GetLength(0) - 1)
        {
            // Если это верхняя строка, не делаем ничего
            return;
        }

        // Перестановка строки вверх
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            GameObject temp = grid[rowIndex, j]; // Сохраняем текущую строку
            grid[rowIndex, j] = grid[rowIndex + 1, j]; // Перемещаем верхнюю строку в текущую позицию
            grid[rowIndex + 1, j] = temp; // Помещаем сохраненную строку в верхнюю позицию

            // Обновляем позиции GameObjects на сцене
            grid[rowIndex, j].transform.position = new Vector2(j, rowIndex);
            grid[rowIndex + 1, j].transform.position = new Vector2(j, rowIndex + 1);
        }
    }

    void CheckSolution() // В ПРОЦЕССЕ ДОРАБОТКИ
    {
        string correctSolutionString = ""; // Строка ответ (без пробелов) \/ СТРОГО 25 СИМВОЛОВ
        string result = BuildResultString();
        if (result == correctSolutionString)
        {
            if (attemptCounter == 0)
                gridCreater.PerfectSolution = true; // Флаг для выдачи достижения и окончания сцены
            else
                gridCreater.CorrectSolution = true; // Флаг для окончания сцены (я честно хз, как реализуется переход по сценам, нужна хелпа)
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
