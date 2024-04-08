using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    // Start is called before the first frame update
    private GridCreater gridCreater;
    private int attemptCounter = 0; // ������� �������
    private int selectedColumnIndex = 0; // �������������� ��������� ������ �������
    private bool checkButtonPressed = false; // ��������� ������ "���������"
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
   /*     int columnIndex = currentCell.position.Item2;

        if (columnIndex >= grid.GetLength(1) - 1)
        {
            // ���� ��� ������� ������ �������, �� ������ ������
            return;
        }

        // ������������ ������� ������
        for (int i = 0; i < gridLayout.transform.childCount / grid.GetLength(1); i++)
        {
            int currentIndex = i * grid.GetLength(1) + columnIndex;
            int rightIndex = i * grid.GetLength(1) + columnIndex + 1;

            // ����� ������� �������� � ������� ��� ������������ ������ ������� � ��������
            Transform temp = layoutGroup[currentIndex];
            layoutGroup[currentIndex] = layoutGroup[rightIndex];
            layoutGroup[rightIndex] = temp;

            // ���������� ������� � ��������
            layoutGroup[currentIndex].SetSiblingIndex(rightIndex);
            layoutGroup[rightIndex].SetSiblingIndex(currentIndex);
        }*/
    }

    public void MoveRowDown()
    {
        int rowIndex = currentCell.position.Item1;

        if (rowIndex <= 0)
        {
            // ���� ��� ������ ������, �� ������ ������
            return;
        }

        // ������������ ������ ����
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            GameObject temp = grid[rowIndex, j]; // ��������� ������� ������
            grid[rowIndex, j] = grid[rowIndex - 1, j]; // ���������� ������ ������ � ������� �������
            grid[rowIndex - 1, j] = temp; // �������� ����������� ������ � ������ �������

            // ��������� ������� GameObjects �� �����
            grid[rowIndex, j].transform.position = new Vector2(j, rowIndex);
            grid[rowIndex - 1, j].transform.position = new Vector2(j, rowIndex - 1);
        }
    }

    public void MoveRowUp()
    {
        int rowIndex = currentCell.position.Item1;

        if (rowIndex >= grid.GetLength(0) - 1)
        {
            // ���� ��� ������� ������, �� ������ ������
            return;
        }

        // ������������ ������ �����
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            GameObject temp = grid[rowIndex, j]; // ��������� ������� ������
            grid[rowIndex, j] = grid[rowIndex + 1, j]; // ���������� ������� ������ � ������� �������
            grid[rowIndex + 1, j] = temp; // �������� ����������� ������ � ������� �������

            // ��������� ������� GameObjects �� �����
            grid[rowIndex, j].transform.position = new Vector2(j, rowIndex);
            grid[rowIndex + 1, j].transform.position = new Vector2(j, rowIndex + 1);
        }
    }

    void CheckSolution() // � �������� ���������
    {
        string correctSolutionString = ""; // ������ ����� (��� ��������) \/ ������ 25 ��������
        string result = BuildResultString();
        if (result == correctSolutionString)
        {
            if (attemptCounter == 0)
                gridCreater.PerfectSolution = true; // ���� ��� ������ ���������� � ��������� �����
            else
                gridCreater.CorrectSolution = true; // ���� ��� ��������� ����� (� ������ ��, ��� ����������� ������� �� ������, ����� �����)
        }
        else
        {
            // ���������� �������� ��� ������������ �������
            attemptCounter++;
        }
    }

    string BuildResultString() // � �������� ���������
    {
        string result = "";
        for (int j = 0; j < gridCreater.grid.GetLength(1); j++) // �������� �� ��������
        {
            for (int i = 0; i < gridCreater.grid.GetLength(0); i++) // �������� �� �������
            {
                // ����� ��������������, ��� � ������ ������ ���� ���������, ���������� ������ ���� ������.
                Cell cellComponent = gridCreater.grid[i, j].GetComponent<Cell>();
                if (cellComponent != null)
                {
                    // ��������������, ��� � ���������� Cell ���� ����� ToString() ��� �������� ��� ��������� �������
                    result += cellComponent.ToString();
                }
            }
        }
        return result;
    }
}
