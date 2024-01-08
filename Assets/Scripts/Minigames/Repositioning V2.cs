using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreater : MonoBehaviour
{
    public GameObject cellPrefab; // ������ ��� �������� ������ \/ ���������� ��������� TextMesh
    public Sprite normalSprite; // ������� �������� ��� ������

    private string gridContent = ""; // ������ �� 25 �������� \/ ������ ���� ������������� ������

    public GameObject[,] grid { get; private set; } // ��������� ������ ��� �������� �����

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

                // ��������� ������ ��� TextMesh
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
        private int attemptCounter = 0; // ������� �������
        private int selectedColumnIndex = 0; // �������������� ��������� ������ �������
        private bool checkButtonPressed = false; // ��������� ������ "���������"

        void Start()
        {
            gridCreater = GridCreater.instance;
            gridCreater.CreateGrid();
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
}
