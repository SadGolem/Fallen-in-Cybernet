using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridController : MonoBehaviour
{
    // Start is called before the first frame update
    private GridCreater gridCreater;
    private int attemptCounter = 0; // ������� �������
    private int selectedColumnIndex = 0; // �������������� ��������� ������ �������
    private bool checkButtonPressed = false; // ��������� ������ "���������"
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
                // ������ ������� ������� � grid
                var temp = grid[i, columnIndex];
                grid[i, columnIndex] = grid[i, columnIndex - 1];
                grid[i, columnIndex - 1] = temp;

                // ��������� ������� ��������
                var tempPos = grid[i, columnIndex].transform.position;
                grid[i, columnIndex].transform.position = grid[i, columnIndex - 1].transform.position;
                grid[i, columnIndex - 1].transform.position = tempPos;
            }
        }
        // ��������� layoutGroup, ����� �������� ����� ������� ��������
        GetLayout();
    }

    public void MoveColumnRight()
    {
        int columnIndex = currentCell.position.Item2;

        // ���������, �� �������� �� ������� ������� ������� ������
        if (columnIndex >= grid.GetLength(1) - 1)
            return;

        // ������ ������� ������� � grid ��� ������ ������
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            // ������ ������� ������� � grid
            var temp = grid[i, columnIndex];
            grid[i, columnIndex] = grid[i, columnIndex + 1];
            grid[i, columnIndex + 1] = temp;

            // ��������� ������� ��������
            var tempPos = grid[i, columnIndex].transform.position;
            grid[i, columnIndex].transform.position = grid[i, columnIndex + 1].transform.position;
            grid[i, columnIndex + 1].transform.position = tempPos;
        }

        // ��������� layoutGroup, ����� �������� ����� ������� ��������
        GetLayout();
    }


    public void MoveRowUp()
    {
        if (!isDoublePer) return;
            int rowIndex = currentCell.position.Item1;

        // ���������, �� �������� �� ������� ������ ������� ������
        if (rowIndex <= 0)
            return;

        // ������ ������� ������� � grid ��� ������� �������
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            // ������ ������� ������� � grid
            var temp = grid[rowIndex, j];
            grid[rowIndex, j] = grid[rowIndex - 1, j];
            grid[rowIndex - 1, j] = temp;

            // ��������� ������� ��������
            var tempPos = grid[rowIndex, j].transform.position;
            grid[rowIndex, j].transform.position = grid[rowIndex - 1, j].transform.position;
            grid[rowIndex - 1, j].transform.position = tempPos;
        }

        // ��������� layoutGroup, ����� �������� ����� ������� ��������
        GetLayout();
    }

    public void MoveRowDown()
    {
        if (!isDoublePer) return;
        int rowIndex = currentCell.position.Item1;

        // ���������, �� �������� �� ������� ������ ������� �����
        if (rowIndex >= grid.GetLength(0) - 1)
            return;

        // ������ ������� ������� � grid ��� ������� �������
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            // ������ ������� ������� � grid
            var temp = grid[rowIndex, j];
            grid[rowIndex, j] = grid[rowIndex + 1, j];
            grid[rowIndex + 1, j] = temp;

            // ��������� ������� ��������
            var tempPos = grid[rowIndex, j].transform.position;
            grid[rowIndex, j].transform.position = grid[rowIndex + 1, j].transform.position;
            grid[rowIndex + 1, j].transform.position = tempPos;
        }

        // ��������� layoutGroup, ����� �������� ����� ������� ��������
        GetLayout();
    }
    private void Update()
    {
        CheckSolution();
    }

    void CheckSolution() // � �������� ���������
    {
        string correctSolutionString = gridCreater.gridAnswer; // ������ ����� (��� ��������) \/ ������ 25 ��������
        string result = BuildResultString();
        if (result == correctSolutionString)
        {
            if (attemptCounter == 0)
                gridCreater.PerfectSolution = true; // ���� ��� ������ ���������� � ��������� �����
            else
                gridCreater.CorrectSolution = true; // ���� ��� ��������� ����� (� ������ ��, ��� ����������� ������� �� ������, ����� �����)
            Debug.Log("������� ����������");
            winWindow.SetActive(true);

            if (!isTutorial && HintMaker.instance.countUsedHints < 1)
            {
                AchievementControl.instance.Clever();
            }
        }
        else
        {
            // ���������� �������� ��� ������������ �������
            attemptCounter++;
            Debug.Log("������� ������������");
        }
    }

    string BuildResultString() // � �������� ���������
    {
        string result = "";
        for (int j = 0; j < grid.GetLength(1); j++) // �������� �� ��������
        {
            for (int i = 0; i < grid.GetLength(0); i++) // �������� �� �������
            {
                // ����� ��������������, ��� � ������ ������ ���� ���������, ���������� ������ ���� ������.
                string cellComponent = grid[j, i].GetComponent<Cell>().GetComponentInChildren<TextMeshProUGUI>().text;
                if (cellComponent != null)
                {
                    // ��������������, ��� � ���������� Cell ���� ����� ToString() ��� �������� ��� ��������� �������
                    result += cellComponent.ToString();
                }
            }
        }
        Debug.Log("���������:" + result);
        return result;
    }
}
