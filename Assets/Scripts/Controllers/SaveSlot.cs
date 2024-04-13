/*
using TMPro;
using UnityEngine;
using UnityEngine.UI;
*/

/*
 Коды ошибок, дабы чуть чуть экономить место (EN-FC-FN1-FС2)
    EN - Код ошибки, порядковый
    FC - Первая буква типа файла, определяющего ошибку (S - Скрипт, A - Ассет, SC - Сцена, I - Графическая ошибка)
    FN1 - Первая буква(набор) фолдера, содержащего или спровоцировавшего ошибку (Или последующем, указывается цепочкой фолдеров)
    FNС - Порядковый номер файла в фолдере FN1
*/

/*public class SaveSlot : MonoBehaviour
{
    public int slotIndex; // Индекс слота для этой кнопки
    [SerializeField] private TextMeshProUGUI saveText; // Ссылка на текстовый компонент кнопки

    private void Start()
    {
        UpdateSlotInfo();
    }

    private void Update()
    {

    }

    public void UpdateSlotInfo()
    {
        // Проверяем, есть ли сохранение в этом слоте
        string saveKey = "saveTime_slot" + slotIndex;
        if (PlayerPrefs.HasKey(saveKey))
        {
            // Получаем данные и обновляем текст кнопки
            int sceneIndex = PlayerPrefs.GetInt("sceneIndex_slot" + slotIndex);
            int dialogIndex = PlayerPrefs.GetInt("dialogIndex_slot" + slotIndex);
            string saveTime = PlayerPrefs.GetString(saveKey);
            //string sceneName = DataHolder.instance.GetSceneName(sceneIndex);
            saveText.text = $"Сохранение {slotIndex}:  {"CHECK"} ; Время: {saveTime}";
        }
        else
        {
            // Установите текст для пустого слота
            saveText.text = $"Сохранение {slotIndex}: - ; Время: -";
        }
    }

    public void LoadSave()
    {
        // Загружаем игру из выбранного слота
        // Нужно будет реализовать загрузку на основе сохраненных данных
    }
}
*/