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
    [SerializeField] private AchievementControl _achievementControl;

    void Update()
    {
        if (dialog.text == "���� ����� ����� ��������� �� ������������� ����. �������!" && Input.anyKeyDown)
        {
            dialogWindow.SetActive(false);
            adsSpawner.SetActive(true);
            StartCoroutine(WaitCoroutine());
        }
        if (dialog.text == "������, � ��������� ����. ������ � ����� ��� ��� ����." && Input.anyKeyDown)
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
        _achievementControl.DoNotClickOnTheAds();
    }
}
