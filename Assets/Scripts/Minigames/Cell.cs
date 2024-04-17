using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    GridCreater creater;
    GridController controller;
    public (int, int) position;
    bool isColumn;
    public GameObject selected;
    public Button btn;
    List<GameObject> rowCells = new List<GameObject>();
    List<GameObject> columnCells = new List<GameObject>();

    void Start()
    {
        creater = GridCreater.instance;
        controller =  GridController.instance;
        Invoke("GetAllCells", 0.01f);
        btn.onClick.AddListener(Clicked);
    }

    void Clicked()
    {
        isColumn = !isColumn;
        if (isColumn)
        {
            GetColumn();
            if (!controller.isDoublePer) return;
            {
                isColumn = true;
            }
        }
        else
        {
            if (!controller.isDoublePer) return;
            {
                GetRow();
            }
        }
    }



    private void GetColumn()
    {
        UnSelected();
        
        for (int row = 0; row < controller.grid.GetLength(0); row++)
        {
            columnCells.Add(controller.grid[row, position.Item2]);
        }
        Debug.Log("Столбец: " + position.Item2);
        foreach (GameObject cell in columnCells)
        {
            Debug.Log(cell.gameObject.name);

            var cellGameObject = cell.gameObject.GetComponent<Cell>();
            cellGameObject.selected.SetActive(true);
            controller.currentCell = cellGameObject;
        }
    }

    private void GetRow()
    {
        UnSelected();

        for (int col = 0; col < controller.grid.GetLength(1); col++)
        {
            rowCells.Add(controller.grid[position.Item1, col]);
        }
        Debug.Log("Строка: " + position.Item1);
        foreach (GameObject cell in rowCells)
        {
            Debug.Log(cell.gameObject.name);

            var cellGameObject = cell.gameObject.GetComponent<Cell>();
            cellGameObject.selected.SetActive(true);
            controller.currentCell = cellGameObject;
        }

    }

    private void UnSelected()
    {
        rowCells.Clear();
        columnCells.Clear();

        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                controller.grid[row, col].GetComponent<Cell>().selected.SetActive(false);
            }
        }
    }

/*    public void UpdatePosition()
    {
        
    }*/

}
