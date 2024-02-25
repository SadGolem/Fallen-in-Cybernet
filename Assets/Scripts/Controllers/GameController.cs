using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private SceneSwitcher _sceneSwitcher;
    private DataHolder _dataHolder;
    private int _sceneIndex = 0;

    private void Start()
    {
        _sceneSwitcher = SceneSwitcher.instance;
        _dataHolder = DataHolder.instance;
    }

    private void SwitchScene()
    {
        _sceneSwitcher.SwitchScene(_sceneIndex);
    }

    private void LoadScene()
    {
        /* _sceneIndex = _dataHolder.LoadDialogIndexAndScene();*/ // Загрузка сцены 
        SceneManager.LoadScene(_sceneIndex);
    }

    public void StartScene()
    {
        LoadScene();
    }



}
