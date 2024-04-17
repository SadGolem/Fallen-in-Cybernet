using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialog;
    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private GameObject adsSpawner;
    [SerializeField] private GameObject ads;
    [SerializeField] private DialogController _dialogController;
    [SerializeField] private AchievementControl _achievementControl;
    private static bool isStarted = false;

    void Update()
    {
        if (dialog.text == "Тебе нужно будет покликать на открывающиеся окна. Начинай!" && Input.anyKeyDown)
        {
            if (isStarted) return;
            dialogWindow.SetActive(false);
            dialogWindow.transform.position += new Vector3(0, -1, 0);
            adsSpawner.SetActive(true);
            StartCoroutine(WaitCoroutine());
        }
        if (dialog.text == "Привет, я настоящий Итан. Сейчас я уберу все эти окна." && Input.anyKeyDown)
        {
            DeleteAllAds();
        }
    }
    private IEnumerator WaitCoroutine()
    {
        if (!isStarted)
        {
            isStarted = true;
            yield return new WaitForSecondsRealtime(10f);

            
            dialogWindow.transform.position += new Vector3(0, 1, 0);
            dialogWindow.SetActive(true);
            adsSpawner.SetActive(false);
            _dialogController.SkipDialog();
        }
    }

    private void DeleteAllAds()
    {
        Destroy(ads);
        _achievementControl.DoNotClickOnTheAds();
    }
}
