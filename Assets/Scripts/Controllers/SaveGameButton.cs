using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // ��� ������ � TextMeshPro
using static DataHolder;

public class SaveGameButton : MonoBehaviour
{
    public int slotIndex;
    public TextMeshProUGUI saveButtonText;

    public void OnSaveButtonClick()
    {
        // ��������� ������
        DataHolder.instance.SaveToFile(slotIndex);

        // ������ ��������� ����� �� ���� ������� ��������.
        LoadSlotButton[] loadButtons = FindObjectsOfType<LoadSlotButton>();
        foreach (var button in loadButtons)
        {
            button.UpdateButton();
        }
        UpdateSaveButtonText();
        Debug.Log("Game saved in slot " + slotIndex);
    }

    private void OnEnable()
    {
        UpdateSaveButtonText();
    }

    public void UpdateSaveButtonText()
    {
        SaveData saveData = DataHolder.instance.LoadFromFile(slotIndex);
        if (saveData != null)
        {
            // �����������, ��� �� ������ ���������� �� �� ����� ���������� � ����������
            saveButtonText.text = $"���������� {slotIndex}: ����� {saveData.sceneIndex}; ������: {saveData.dialogIndex}; �����: {saveData.saveTime}";
        }
        else
        {
            saveButtonText.text = "����� ����������";
        }
    }

}