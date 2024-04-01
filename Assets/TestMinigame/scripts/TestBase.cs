using System.Collections.Generic;

public static class TestBase
{
    private static List<(string,string)> questionsFirstTest = new List<(string, string)> { ("Потенциальная возможность определённым образом нарушить информационную безопасность - это ...?" , "Угроза"),
("Попытка реализации угрозы называется ...?", "Атака"),
("Потенциальный злоумышленник - это...?", "Источник угрозы"),
("Промежуток времени от момента, когда появляется возможность использовать слабое место, и до момента, когда пробел ликвидируется, называется...?", "Окно опасности"),
("Чаще всего, угроза безопасности появляется в следствии наличия в системе ...?", "Уязвимости"),
("Люди, занимающиеся компьютерными преступлениями как профессионально, так и просто из любопытства - это...?", "Хакеры") };

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
