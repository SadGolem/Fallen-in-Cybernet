using UnityEngine;

public class GridController : MonoBehaviour
{
    // Start is called before the first frame update
    private GridCreater gridCreater;
    private int attemptCounter = 0; // ������� �������
    private int selectedColumnIndex = 0; // �������������� ��������� ������ �������
    private bool checkButtonPressed = false; // ��������� ������ "���������"
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
        // ��������� ����� ��� ������������ ��������
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveColumnLeft(selectedColumnIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveColumnRight(selectedColumnIndex);
        }

        // ��������� ������� ������ "���������"
        if (checkButtonPressed)
        {
            CheckSolution(); // ��������� ��������� ������
            checkButtonPressed = false; // ����� ��������� ������
        }
    }

    void MoveColumnLeft(int columnIndex)
    {
        if (columnIndex <= 0)
        {
            // ���� ��� ������� ����� �������, �� ������ ������
            return;
        }
        
        // ������������ ������� �����
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            GameObject temp = grid[i, columnIndex]; // ��������� ������� �������
            grid[i, columnIndex] = grid[i, columnIndex - 1]; // ���������� ����� ������� � ������� �������
            grid[i, columnIndex - 1] = temp; // �������� ����������� ������� � ����� �������

            // ���������� GameObjects �� �����
            grid[i, columnIndex].transform.position = new Vector2(columnIndex, i);
            grid[i, columnIndex - 1].transform.position = new Vector2(columnIndex - 1, i);
        }
    }
    void MoveColumnRight(int columnIndex)
    {
        int maxColumnIndex = grid.GetLength(1) - 1; // �������� ����������� ��������� ������ �������
        if (columnIndex >= maxColumnIndex)
        {
            // ���� ��� ������� ������ �������, �� ������ ������
            return;
        }

        // ������������ ������� ������
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            GameObject temp = grid[i, columnIndex]; // ��������� ������� �������
            grid[i, columnIndex] = grid[i, columnIndex + 1]; // ���������� ������ ������� � ������� �������
            grid[i, columnIndex + 1] = temp; // �������� ����������� ������� � ������ �������

            // ��������� ������� GameObjects �� �����
            grid[i, columnIndex].transform.position = new Vector2(columnIndex, i);
            grid[i, columnIndex + 1].transform.position = new Vector2(columnIndex + 1, i);
        }
    }

    void CheckSolution() // � �������� ���������
    {
        string correctSolutionString = ""; // ������ ����� (��� ��������) \/ ������ 25 ��������
        string result = BuildResultString();
        if (result == correctSolutionString)
        {
            if (attemptCounter == 0)
                PerfectSolution = true; // ���� ��� ������ ���������� � ��������� �����
            else
                CorrectSolution = true; // ���� ��� ��������� ����� (� ������ ��, ��� ����������� ������� �� ������, ����� �����)
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
