using UnityEngine;

public class AchievementControl : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [HideInInspector] public static bool isClickedOnAds;
    [HideInInspector] public static bool isThreeOnARow;

   /* private Animation achievementAnimation;*/
    private bool achievementShown = false;
    private bool achievementShownThreeOnARow = false;

    void Start()
    {
       /* achievementAnimation = animator.GetComponent<Animation>();
        if (achievementAnimation)
        {
            achievementAnimation.playAutomatically = false;
            achievementAnimation.Stop();
            achievementAnimation.clip = achievementAnimation.GetClip("ahievement rainbow light");
        }*/
    }

    public void DoNotClickOnTheAds()
    {
        if (!isClickedOnAds && !achievementShown)
        {
            string achievement = AchievementBase.doNotClickOnTheads.Item1;
            achievementShown = true;
            ShowAchievement();
            AchievementShowed.Showed(achievement);
        }
    }


    public void ThreeOnARow()
    {
        if (isThreeOnARow && !achievementShown)
        {
            string achievement = AchievementBase.doNotClickOnTheads.Item1;
            achievementShownThreeOnARow = true;
            ShowAchievement();
            AchievementShowed.Showed(achievement);
        }
    }

    public void ShowAchievement()
    {
        animator.SetBool("isShowing", true);
        /*achievementAnimation.Play();*/
        Invoke("HideAchievement", 3f);
    }

    private void HideAchievement()
    {
       /* achievementAnimation.Stop();*/
        animator.SetBool("isShowing", false);
    }
}
