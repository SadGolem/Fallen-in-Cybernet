using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameButton : MonoBehaviour
{
    public int slotIndex;

    public void OnSaveButtonClick()
    {
        DataHolder.instance.SaveToFile(slotIndex);
        Debug.Log($"Игра сохранена в слот N{slotIndex}.");
    }
}

