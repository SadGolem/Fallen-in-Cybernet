using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HintMaker : MonoBehaviour
{
    public TextMeshProUGUI hintm;

    private List<List<string>> hintsPerPhrase = new List<List<string>>();
    private Dictionary<int, int> hintCountPerPhrase = new Dictionary<int, int>();
    [SerializeField] private List<string> hints = new List<string>();

    public bool AllHintsGiven { get; private set; } = false;

    private void Start()
    {
        // Инициализируем подсказки для каждой фразы
        // Например, для первой фразы...
        /* hintsPerPhrase.Add(new List<string>
         {
             "Ь должен быть в третьей строке",
             "Ь должен быть во втором стобце",
             "В последней строке должна быть одна О",
             "Й должна быть во второй строке",
             "Двойная О должна быть в первой строке"
         });*/
      /*  hintCountPerPhrase.Add(new List<string>
        {
            "Первая подсказка для фразы 2",
            "Вторая подсказка для фразы 2",
            "Третья подсказка для фразы 2",
            "Четвертая подсказка для фразы 2",
            "Пятая подсказка для фразы 2"
        });*/
        hintsPerPhrase.Add(hints);
        // Добавьте аналогично подсказки для других фраз...
    }

    // Метод для запроса подсказки для текущей фразы
    public string GetHint()
    {
        int phraseIndex = Encrypter.Instance.GetSelectedPhraseIndex();

        // Проверяем, есть ли уже для этой фразы какие-либо выданные подсказки
        if (!hintCountPerPhrase.ContainsKey(phraseIndex))
        {
            hintCountPerPhrase[phraseIndex] = 0;
        }

        if (hintCountPerPhrase[phraseIndex] < hintsPerPhrase[phraseIndex].Count)
        {
            string hint = hintsPerPhrase[phraseIndex][hintCountPerPhrase[phraseIndex]];
            hintCountPerPhrase[phraseIndex]++;
            return hint;
        }
        else
        {
            // Проверяем, были ли выданы все подсказки для всех фраз
            if (hintCountPerPhrase.Values.All(count => count >= 5))
            {
                AllHintsGiven = true;
            }
            return "Для этой фразы больше нет подсказок.";
        }
    }

    public void SetHint() 
    {
        hintm.text = GetHint();
    }
}
