/*
using TMPro;
using UnityEngine;
using UnityEngine.UI;
*/

/*
 ���� ������, ���� ���� ���� ��������� ����� (EN-FC-FN1-F�2)
    EN - ��� ������, ����������
    FC - ������ ����� ���� �����, ������������� ������ (S - ������, A - �����, SC - �����, I - ����������� ������)
    FN1 - ������ �����(�����) �������, ����������� ��� ����������������� ������ (��� �����������, ����������� �������� ��������)
    FN� - ���������� ����� ����� � ������� FN1
*/

/*public class SaveSlot : MonoBehaviour
{
    public int slotIndex; // ������ ����� ��� ���� ������
    [SerializeField] private TextMeshProUGUI saveText; // ������ �� ��������� ��������� ������

    private void Start()
    {
        UpdateSlotInfo();
    }

    private void Update()
    {

    }

    public void UpdateSlotInfo()
    {
        // ���������, ���� �� ���������� � ���� �����
        string saveKey = "saveTime_slot" + slotIndex;
        if (PlayerPrefs.HasKey(saveKey))
        {
            // �������� ������ � ��������� ����� ������
            int sceneIndex = PlayerPrefs.GetInt("sceneIndex_slot" + slotIndex);
            int dialogIndex = PlayerPrefs.GetInt("dialogIndex_slot" + slotIndex);
            string saveTime = PlayerPrefs.GetString(saveKey);
            //string sceneName = DataHolder.instance.GetSceneName(sceneIndex);
            saveText.text = $"���������� {slotIndex}:  {"CHECK"} ; �����: {saveTime}";
        }
        else
        {
            // ���������� ����� ��� ������� �����
            saveText.text = $"���������� {slotIndex}: - ; �����: -";
        }
    }

    public void LoadSave()
    {
        // ��������� ���� �� ���������� �����
        // ����� ����� ����������� �������� �� ������ ����������� ������
    }
}
*/