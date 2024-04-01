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
    Transform[] layoutGroup;

    void GetLayout()
    {
        layoutGroup = new Transform[gridLayout.transform.childCount];
        for (int i = 0; i < gridLayout.transform.childCount; i++)
        {
            layoutGroup[i] = gridLayout.transform.GetChild(i);
        }
    }

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

    public void MoveColumnLeft()
    {
        int columnIndex = currentCell.position.Item2;

        if (columnIndex <= 0)
        {
            // Если это крайний левый столбец, не делать ничего
            return;
        }

        // Перестановка столбца влево
        for (int i = 0; i < gridLayout.transform.childCount / grid.GetLength(1); i++)
        {
            // Получаем индексы для обмена
            int currentIndex = i * grid.GetLength(1) + columnIndex;
            int leftIndex = i * grid.GetLength(1) + columnIndex - 1;

            // Запоминаем левую позицию
            Vector3 leftPosition = layoutGroup[leftIndex].position;

            // Меняем позиции объектов местами
            layoutGroup[leftIndex].position = layoutGroup[currentIndex].position;
            layoutGroup[currentIndex].position = leftPosition;

            // Опционально: для обмена местами объектов в иерархии (если это требуется)
            layoutGroup[leftIndex].SetSiblingIndex(currentIndex);
            layoutGroup[currentIndex].SetSiblingIndex(leftIndex);
        }

        // Обновляем массив layoutGroup, если порядок объектов в иерархии изменился
        GetLayout();
    }

    public void MoveColumnRight()
    {
        int columnIndex = currentCell.position.Item2;// нужно корректно получить текущий индекс столбца из вашей логики

    // Проверка, что это не крайний правый столбец
    if (columnIndex >= grid.GetLength(1) - 1)
        {
            // Если это крайний правый столбец, не делать ничего
            return;
        }

        // Перестановка столбца вправо
        for (int i = 0; i < gridLayout.transform.childCount / grid.GetLength(1); i++)
        {
            // Получаем индексы для обмена
            int currentIndex = i * grid.GetLength(1) + columnIndex;
            int rightIndex = i * grid.GetLength(1) + columnIndex + 1;

            // Запоминаем правую позицию
            Vector3 rightPosition = layoutGroup[rightIndex].position;

            // Меняем позиции объектов местами
            layoutGroup[rightIndex].position = layoutGroup[currentIndex].position;
            layoutGroup[currentIndex].position = rightPosition;

            // Опционально: для обмена местами объектов в иерархии
            layoutGroup[rightIndex].SetSiblingIndex(currentIndex);
            layoutGroup[currentIndex].SetSiblingIndex(rightIndex);
        }

        // Обновляем массив layoutGroup после изменения порядка объектов
        GetLayout();
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
