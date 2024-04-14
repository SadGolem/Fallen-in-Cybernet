using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    // Start is called before the first frame update
    private MagickGridCreater gridCreater;
    private int attemptCounter = 0; // Счетчик попыток
    private int selectedColumnIndex = 0; // Инициализируем выбранный индекс столбца
    private bool checkButtonPressed = false; // Состояние кнопки "Проверить"
    public GameObject[,] grid;
    public GameObject[,] gridNumbers;
    public static MagicController instance;
    public Cell currentCell;
    public GameObject gridLayout;
    Transform[,] layoutGroup;
    private List<Cell> cells = new List<Cell>();
    [SerializeField] private bool isDoublePer;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private TextMeshProUGUI answer;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gridCreater = MagickGridCreater.instance;
        grid = gridCreater.gridContentArray;
        gridNumbers = gridCreater.gridContentNumbers;
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
        /*string s = "";*/
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int index = i * cols + j;
                if (index < grid.Length)
                {
                    layoutGroup[i, j] = grid[i, j].transform;
                    layoutGroup[i, j].GetComponent<Cell>().position = (i, j);
                    /*s += layoutGroup[i, j].GetComponent<Cell>().position + " ";*/

                }

            }
            /*s += "\n";*/
        }
        /*Debug.Log(s);*/

    }
    private void Update()
    {
        CheckSolution();
    }

    void CheckSolution() // В ПРОЦЕССЕ ДОРАБОТКИ
    {
        string correctSolutionString = gridCreater.gridAnswer;
        correctSolutionString = correctSolutionString.Replace(" ", "").ToLower();
        string userAnswer = answer.text.ToString();
        userAnswer = userAnswer.Replace(" ", "").ToLower();
        userAnswer = userAnswer.Substring(0, userAnswer.Length - 1); // ЫЫЫ это удаление невидимого сивола на инпуте

        if (userAnswer == correctSolutionString)
        {
            if (attemptCounter == 0)
                gridCreater.PerfectSolution = true; // Флаг для выдачи достижения и окончания сцены
            else
                gridCreater.CorrectSolution = true; // Флаг для окончания сцены (я честно хз, как реализуется переход по сценам, нужна хелпа)
            Debug.Log("Решение правильное");
            winWindow.SetActive(true);
        }
        else
        {
            // Увеличение счетчика при неправильном решении
            attemptCounter++;
            Debug.Log("Решение неправильное");
        }
    }
}
