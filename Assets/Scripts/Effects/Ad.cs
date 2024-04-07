using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ad : MonoBehaviour
{
    public Image _image;
    private AchievementControl _achievementControl;
    public Image Image
    {
        get { return _image; }
        set { _image = value; }
    }

    private void Start()
    {
        _achievementControl = GetComponent<AchievementControl>();
    }

    public void OnButtonClick()
    {
        _achievementControl.isClickedOnAds = true;
    }
}
