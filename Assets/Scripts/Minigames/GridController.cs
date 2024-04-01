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
            // ���� ��� ������� ����� �������, �� ������ ������
            return;
        }

        // ������������ ������� �����
        for (int i = 0; i < gridLayout.transform.childCount / grid.GetLength(1); i++)
        {
            // �������� ������� ��� ������
            int currentIndex = i * grid.GetLength(1) + columnIndex;
            int leftIndex = i * grid.GetLength(1) + columnIndex - 1;

            // ���������� ����� �������
            Vector3 leftPosition = layoutGroup[leftIndex].position;

            // ������ ������� �������� �������
            layoutGroup[leftIndex].position = layoutGroup[currentIndex].position;
            layoutGroup[currentIndex].position = leftPosition;

            // �����������: ��� ������ ������� �������� � �������� (���� ��� ���������)
            layoutGroup[leftIndex].SetSiblingIndex(currentIndex);
            layoutGroup[currentIndex].SetSiblingIndex(leftIndex);
        }

        // ��������� ������ layoutGroup, ���� ������� �������� � �������� ���������
        GetLayout();
    }

    public void MoveColumnRight()
    {
        int columnIndex = currentCell.position.Item2;// ����� ��������� �������� ������� ������ ������� �� ����� ������

    // ��������, ��� ��� �� ������� ������ �������
    if (columnIndex >= grid.GetLength(1) - 1)
        {
            // ���� ��� ������� ������ �������, �� ������ ������
            return;
        }

        // ������������ ������� ������
        for (int i = 0; i < gridLayout.transform.childCount / grid.GetLength(1); i++)
        {
            // �������� ������� ��� ������
            int currentIndex = i * grid.GetLength(1) + columnIndex;
            int rightIndex = i * grid.GetLength(1) + columnIndex + 1;

            // ���������� ������ �������
            Vector3 rightPosition = layoutGroup[rightIndex].position;

            // ������ ������� �������� �������
            layoutGroup[rightIndex].position = layoutGroup[currentIndex].position;
            layoutGroup[currentIndex].position = rightPosition;

            // �����������: ��� ������ ������� �������� � ��������
            layoutGroup[rightIndex].SetSiblingIndex(currentIndex);
            layoutGroup[currentIndex].SetSiblingIndex(rightIndex);
        }

        // ��������� ������ layoutGroup ����� ��������� ������� ��������
        GetLayout();
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
