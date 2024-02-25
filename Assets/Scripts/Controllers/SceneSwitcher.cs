using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance;

    private void Awake()
    {
        instance = this;
    }

    public void SwitchScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad); // Загрузить указанную сцену
    }
}

