using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Для работы с TextMeshPro
using static DataHolder;

public class SaveGameButton : MonoBehaviour
{
    public int slotIndex;
    public TextMeshProUGUI saveButtonText;

    public void OnSaveButtonClick()
    {
        // Сохраняем данные
        DataHolder.instance.SaveToFile(slotIndex);

        // Теперь обновляем текст на всех кнопках загрузки.
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
            // Предположим, что вы хотите отобразить ту же самую информацию о сохранении
            saveButtonText.text = $"Сохранение {slotIndex}: Сцена {saveData.sceneIndex}; Диалог: {saveData.dialogIndex}; Время: {saveData.saveTime}";
        }
        else
        {
            saveButtonText.text = "Новое сохранение";
        }
    }

}