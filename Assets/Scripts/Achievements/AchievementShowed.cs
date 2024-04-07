using System.Collections.Generic;
using UnityEngine;

public static class AchievementShowed
{
    private static readonly List<int> achievement = new List<int>();


    private static void Save()
    {
        //реализовать сохранения после получения
    }

    public static void Showed(string achievement)
    {
        //показанная ачивка попадает сюда и сохраняется
        Save();
    }


}
