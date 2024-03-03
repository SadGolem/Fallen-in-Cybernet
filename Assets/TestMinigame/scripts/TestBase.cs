using System.Collections.Generic;

public static class TestBase
{
    private static List<(string,string)> questionsFirstTest = new List<(string, string)> { ("Как зовут папу?", "Как зовут маму?") };

    private static List<(string,string)> questionsSecondTest = new List<(string, string)> { ("Как зовут папу?", "Как зовут маму?") };

    private static List<(string,string)> questionsThirdTest = new List<(string, string)> { ("Как зовут папу?", "Как зовут маму?") };


    public static List<(string, string)> FirstTest
    {
        get { return questionsFirstTest; }
    }

    public static List<(string, string)> SecondTest
    {
        get { return questionsSecondTest; }
    }

    public static List<(string, string)> ThirdTest
    {
        get { return questionsThirdTest; }
    }

}
