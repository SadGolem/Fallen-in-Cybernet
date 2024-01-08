using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreater : MonoBehaviour
{
    public GameObject cellPrefab; // ������ ��� �������� ������
    public Sprite normalSprite; // ������� �������� ��� ������
    public Sprite highlightSprite; // �������� ��� ��������� ������

    private GameObject[,] grid; // ��������� ������ ��� �������� �����
    private GameObject selectedCell; // ������� ��������� ������

    public static GridCreater instance;

    private void Awake()
    {
        instance = this;
    }

    void CreateGrid()
    {
        grid = new GameObject[5, 5];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector3(i, j, 0), Quaternion.identity);
                cell.GetComponent<SpriteRenderer>().sprite = normalSprite;
                grid[i, j] = cell;
            }
        }
    }

    public class GridController : MonoBehaviour
    {
        // Start is called before the first frame update
        private GridCreater gridCreater;
        void Start()
        {
            gridCreater = GridCreater.instance;
            gridCreater.CreateGrid();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    GameObject clickedCell = hit.collider.gameObject;

                    if (clickedCell == gridCreater.selectedCell)
                    {
                        // ���� �������� �� �� ������, ������� ���������
                        gridCreater.selectedCell.GetComponent<SpriteRenderer>().sprite = gridCreater.normalSprite;
                        gridCreater.selectedCell = null;
                    }
                    else
                    {
                        // ������� ��������� � ���������� ������ (���� ����)
                        if (gridCreater.selectedCell != null)
                            gridCreater.selectedCell.GetComponent<SpriteRenderer>().sprite = gridCreater.normalSprite;

                        // �������� ����� ������
                        clickedCell.GetComponent<SpriteRenderer>().sprite = gridCreater.highlightSprite;
                        gridCreater.selectedCell = clickedCell;
                    }
                }
            }
        }
    }
}
