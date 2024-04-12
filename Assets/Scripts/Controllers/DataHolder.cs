using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static DataHolder instance;

    private void Awake()
    {
        instance = this;
    }

    private void SaveScene(int index)
    {
        PlayerPrefs.SetInt("sceneIndex", index);
    }

    private void LoadScene(int index)
    {
        PlayerPrefs.GetInt("sceneIndex", index);
    }

    public void SaveDialogIndexAndScene(int indexScene, int indexDialog)
    {
        SaveScene(indexScene);
        PlayerPrefs.SetInt("dialogIndex", indexDialog);
    }

    public void LoadDialogIndexAndScene(int indexScene)
    {
        SaveScene(indexScene);
        PlayerPrefs.GetInt("dialogIndex");
    }




}
