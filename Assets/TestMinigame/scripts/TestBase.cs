using System.Collections.Generic;

public static class TestBase
{
    private static List<(string,string)> questionsFirstTest = new List<(string, string)> { ("Вопрос1?", "Ответ1"), ("Вопрос2?", "Ответ2"),
        ("Вопрос3", "Ответ3"), ("Вопрос4", "Ответ4"), ("Вопрос5", "Ответ5") };

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
