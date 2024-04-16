using UnityEngine;

public class AchievementControl : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [HideInInspector] public static bool isClickedOnAds;

   /* private Animation achievementAnimation;*/
    private bool achievementShown = false;

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
            ShowAchievement();
            AchievementShowed.Showed(achievement);
        }
    }

    public void ShowAchievement()
    {
        animator.SetBool("isShowing", true);
        /*achievementAnimation.Play();*/
        achievementShown = true;
        Invoke("HideAchievement", 3f);
    }

    private void HideAchievement()
    {
       /* achievementAnimation.Stop();*/
        animator.SetBool("isShowing", false);
    }
}
