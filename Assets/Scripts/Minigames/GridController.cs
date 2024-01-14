using UnityEngine;

public class GridController : MonoBehaviour
{
    // Start is called before the first frame update
    private GridCreater gridCreater;
    private int attemptCounter = 0; // Счетчик попыток
    private int selectedColumnIndex = 0; // Инициализируем выбранный индекс столбца
    private bool checkButtonPressed = false; // Состояние кнопки "Проверить"
    private GameObject[,] grid;
    void Start()
    {
        gridCreater = GridCreater.instance;
        gridCreater.CreateGrid();
        grid = gridCreater.grid;
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
