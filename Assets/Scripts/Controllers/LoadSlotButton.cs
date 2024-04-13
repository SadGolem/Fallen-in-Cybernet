using UnityEngine;
using UnityEngine.UI; // Это нужно для работы с классом Button
using UnityEngine.SceneManagement;
using TMPro; // Для работы с TextMeshPro
using static DataHolder;

public class LoadSlotButton : MonoBehaviour
{
    public int slotIndex; // Индекс этого слота сохранения
    public TextMeshProUGUI buttonText;

    private void Start()
    {
        UpdateButton();
    }
    private void Awake()
    {
        // Устанавливаем слушатель событий для кнопки
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    public void UpdateButton()
    {
        SaveData saveData = DataHolder.instance.LoadFromFile(slotIndex);
        Debug.Log($"Попытка обновления текста кнопки для слота {slotIndex}.");

        if (saveData != null)
        {
            buttonText.text = $"Сохранение {slotIndex}: Сцена {saveData.sceneIndex}; Диалог: {saveData.dialogIndex}; Время: {saveData.saveTime}";
            Debug.Log($"Текст кнопки обновлен: {buttonText.text}");
        }
        else
        {
            buttonText.text = $"Сохранение {slotIndex}: Пусто";
            Debug.Log("Данные сохранения не найдены.");
        }
    }

    public void OnButtonClicked()
    {
        // Загружаем сохраненные данные.
        SaveData saveData = DataHolder.instance.LoadFromFile(slotIndex);

        if (saveData != null)
        {
            // Загружаем сцену используя SceneSwitcher
            SceneSwitcher.SwitchScene(saveData.sceneIndex);

            // Установим обработчик, который будет вызван после загрузки сцены
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Debug.LogError($"Нет сохраненных данных для слота {slotIndex}.");
        }
    }

    // Обработчик для события после загрузки сцены
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Получаем сохраненные данные еще раз, в случае если сцена перезагрузилась
        SaveData saveData = DataHolder.instance.LoadFromFile(slotIndex);

        // Находим DialogController в новой сцене и устанавливаем индекс диалога
        DialogController dialogController = FindObjectOfType<DialogController>();
        if (dialogController != null && saveData != null)
        {
            dialogController.SetDialogIndex(saveData.dialogIndex);
        }
        else
        {
            Debug.LogError("Ошибка при установке индекса диалога: DialogController не найден или данные сохранения отсутствуют.");
        }

        // Отписываемся от события после использования
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}