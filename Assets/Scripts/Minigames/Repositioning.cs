using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab; // ������ ��� �������� ������
    public Sprite normalSprite; // ������� �������� ��� ������
    public Sprite highlightSprite; // �������� ��� ��������� ������

    private GameObject[,] grid; // ��������� ������ ��� �������� �����
    private GameObject selectedCell; // ������� ��������� ������

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

    public class NewBehaviourScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            CreateGrid();
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

                    if (clickedCell == selectedCell)
                    {
                        // ���� �������� �� �� ������, ������� ���������
                        selectedCell.GetComponent<SpriteRenderer>().sprite = normalSprite;
                        selectedCell = null;
                    }
                    else
                    {
                        // ������� ��������� � ���������� ������ (���� ����)
                        if (selectedCell != null)
                            selectedCell.GetComponent<SpriteRenderer>().sprite = normalSprite;

                        // �������� ����� ������
                        clickedCell.GetComponent<SpriteRenderer>().sprite = highlightSprite;
                        selectedCell = clickedCell;
                    }
                }
    }
}
