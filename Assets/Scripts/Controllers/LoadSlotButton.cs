using UnityEngine;
using UnityEngine.UI; // ��� ����� ��� ������ � ������� Button
using UnityEngine.SceneManagement;
using TMPro; // ��� ������ � TextMeshPro
using static DataHolder;

public class LoadSlotButton : MonoBehaviour
{
    public int slotIndex; // ������ ����� ����� ����������
    public TextMeshProUGUI buttonText;

    private void Start()
    {
        UpdateButton();
    }
    private void Awake()
    {
        // ������������� ��������� ������� ��� ������
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    public void UpdateButton()
    {
        SaveData saveData = DataHolder.instance.LoadFromFile(slotIndex);
        Debug.Log($"������� ���������� ������ ������ ��� ����� {slotIndex}.");

        if (saveData != null)
        {
            buttonText.text = $"���������� {slotIndex}: ����� {saveData.sceneIndex}; ������: {saveData.dialogIndex}; �����: {saveData.saveTime}";
            Debug.Log($"����� ������ ��������: {buttonText.text}");
        }
        else
        {
            buttonText.text = $"���������� {slotIndex}: �����";
            Debug.Log("������ ���������� �� �������.");
        }
    }

    public void OnButtonClicked()
    {
        // ��������� ����������� ������.
        SaveData saveData = DataHolder.instance.LoadFromFile(slotIndex);

        if (saveData != null)
        {
            // ��������� ����� ��������� SceneSwitcher
            SceneSwitcher.SwitchScene(saveData.sceneIndex);

            // ��������� ����������, ������� ����� ������ ����� �������� �����
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Debug.LogError($"��� ����������� ������ ��� ����� {slotIndex}.");
        }
    }

    // ���������� ��� ������� ����� �������� �����
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �������� ����������� ������ ��� ���, � ������ ���� ����� ���������������
        SaveData saveData = DataHolder.instance.LoadFromFile(slotIndex);

        // ������� DialogController � ����� ����� � ������������� ������ �������
        DialogController dialogController = FindObjectOfType<DialogController>();
        if (dialogController != null && saveData != null)
        {
            dialogController.SetDialogIndex(saveData.dialogIndex);
        }
        else
        {
            Debug.LogError("������ ��� ��������� ������� �������: DialogController �� ������ ��� ������ ���������� �����������.");
        }

        // ������������ �� ������� ����� �������������
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}