using UnityEngine;

public class GridCreater : MonoBehaviour
{
/*    public GameObject cellPrefab; // ������ ��� �������� ������ \/ ���������� ��������� TextMesh
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

    public void CreateGrid()
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
    }*/
}



