using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridCreater : MonoBehaviour
{
    public GameObject cellPrefab; // ������ ��� �������� ������ \/ ���������� ��������� TextMesh
    public Sprite normalSprite; // ������� �������� ��� ������
    public List<Sprite> normalSprites = new List<Sprite>();
    public GameObject obj;


    private string gridContent = "��������������_����������"; // ������ �� 25 �������� \/ ������ ���� ������������� ������

    public GameObject[,] grid { get; private set; } // ��������� ������ ��� �������� �����

    public bool PerfectSolution;
    public bool CorrectSolution;

    public static GridCreater instance;

    private void Awake()
    {
        instance = this;
        CreateGrid();
    }

    public void CreateGrid()
    {
        grid = new GameObject[5, 5];
        int stringIndex = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject cell = Instantiate(cellPrefab, obj.transform.forward, Quaternion.identity, obj.transform);
                cell.GetComponent<Image>().sprite = PickRandomElement();
                cell.GetComponent<Cell>().position = (i, j);
                // ��������� ������ ��� TextMesh
                TextMeshProUGUI textMesh = cell.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null && stringIndex < gridContent.Length)
                {
                    textMesh.text = gridContent[stringIndex].ToString();
                    stringIndex++;
                }

                grid[i, j] = cell;
            }
        }

        Sprite PickRandomElement()
        {
            if (normalSprites.Count == 0)
            {
                Debug.LogError("������ ����!");
                return null;
            }

            int randomIndex = Random.Range(0, normalSprites.Count);
            return normalSprites[randomIndex];
        }
    }
}



