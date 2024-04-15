using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    public void Quit()
    {
        //Debug.Log("Выход из приложения");
        Application.Quit();
    }
}