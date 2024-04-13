using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataHolder : MonoBehaviour
{
    public static DataHolder instance;
    private string saveFolderPath;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            saveFolderPath = Application.persistentDataPath + "/saves/";
            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }
        }
    }

    [Serializable]
    public class SaveData
    {
        public int sceneIndex;
        public int dialogIndex;
        public string saveTime;
    }

    public void SaveToFile(int slotIndex)
    {
        SaveData saveData = new SaveData
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex,
            dialogIndex = FindObjectOfType<DialogController>()?.GetCurrentDialogIndex() ?? 0,
            saveTime = System.DateTime.Now.ToString()
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Path.Combine(saveFolderPath, $"save{slotIndex}.json"), json);
    }
    public SaveData LoadFromFile(int slotIndex)
    {
        string filePath = Path.Combine(saveFolderPath, $"save{slotIndex}.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            return saveData;
        }
        else
        {
            Debug.LogError($"Save file not found in {filePath}");
            return null;
        }
    }

    public void SaveScene(int slotIndex, int indexScene)
    {
        PlayerPrefs.SetInt("sceneIndex_slot" + slotIndex, indexScene);
        PlayerPrefs.SetString("saveTime_slot" + slotIndex, System.DateTime.Now.ToString()); // ��������� ������� �����
    }

    public int LoadScene(int slotIndex)
    {
        return PlayerPrefs.GetInt("sceneIndex_slot" + slotIndex, -1); // ���������� -1 ���� ���� ����
    }

    public void SaveDialogIndexAndScene(int slotIndex, int indexScene, int indexDialog)
    {
        SaveScene(slotIndex, indexScene);
        PlayerPrefs.SetInt("dialogIndex_slot" + slotIndex, indexDialog);
        PlayerPrefs.SetString("saveTime_slot" + slotIndex, System.DateTime.Now.ToString());
        PlayerPrefs.Save(); // ��������� ��������� � PlayerPrefs
    }


    public int LoadDialogIndex(int slotIndex)
    {
        return PlayerPrefs.GetInt("dialogIndex_slot" + slotIndex, -1); // ���������� -1 ���� ���� ����
    }

    // �������������� ����� ��� ��������� ������� ���������� ��� �����
    public string GetSaveTime(int slotIndex)
    {
        return PlayerPrefs.GetString("saveTime_slot" + slotIndex, "-"); // ���������� "-" ���� ���� ����
    }


    // �������������� ����� ��� ��������� �������� ����� �� �������
    public string GetSceneNameByIndex(int index)
    {
        // ��� ���� ������ ��� ����������� ����� ����� �� �������
        switch (index)
        {
            case 1: return "������ ���";
            case 2: return "������ ���";
            case 3: return "������ ���";

            default: return "??? ���";
        }
    }
}
