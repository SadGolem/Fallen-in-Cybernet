using System.Collections;
using TMPro;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialog;
    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private GameObject adsSpawner;
    [SerializeField] private GameObject ads;
    [SerializeField] private DialogController _dialogController;

    void Update()
    {
        if (dialog.text == "Привет! Меня зовут Итан. Я твой помощник в этой игре. Я буду тебе помогать во всем. Твоей первой задачей будет нажимать на эти окошки." && Input.anyKeyDown)
        {
            dialogWindow.SetActive(false);
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
        yield return new WaitForSecondsRealtime(10f);

        dialogWindow.SetActive(true);
        adsSpawner.SetActive(false);
        _dialogController.SkipDialog();
    }

    private void DeleteAllAds()
    {
        Destroy(ads);
    }
}
