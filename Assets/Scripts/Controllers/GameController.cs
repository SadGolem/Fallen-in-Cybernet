using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private DataHolder _dataHolder;
    private int _sceneIndex = 1;
    

    private void Start()
    {
        _dataHolder = DataHolder.instance;
        Time.timeScale = 1.0f;
        SceneSwitcher.sceneNumber = 0;
    }

    public void SwitchScene()
    {
        SceneSwitcher.SwitchScene();
    }

    private void LoadScene()
    {
        /* _sceneIndex = _dataHolder.LoadDialogIndexAndScene();*/ // �������� ����� 
        SceneManager.LoadScene(_sceneIndex);
    }

    public void StartScene()
    {
        LoadScene();
    }

    public void RestartScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        // �������� ������ �������� �����
        int activeSceneIndex = activeScene.buildIndex;

        // ��������� ����� �� �� �������
        SceneManager.LoadScene(activeSceneIndex);
    }


}
