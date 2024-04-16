using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitcher
{
    public static int sceneNumber = 1;


    public static void SwitchScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad); // Загрузить указанную сцену
    }

    public static void SwitchScene()
    {
        SceneManager.LoadScene(++sceneNumber); // Загрузить указанную сцену
    }
}

