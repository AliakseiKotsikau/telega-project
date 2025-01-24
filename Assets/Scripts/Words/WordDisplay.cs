using UnityEngine;

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wordListText; // UI элемент дл€ отображени€ текста

    private List<string> wordsToFind = new List<string>();
    private List<string> foundWords = new List<string>();


    public void SetWordsToFind(List<string> words)
    {
        wordsToFind = new List<string>(words);
        UpdateWordList();
    }

    public void MarkWordFound(string word)
    {
        if (!foundWords.Contains(word) && wordsToFind.Contains(word))
        {
            foundWords.Add(word);
            UpdateWordList(); // ќбновл€ем список с учетом нового найденного слова
        }
    }


    private void UpdateWordList()
    {
        string formattedText = "";
        for (int i = 0; i < wordsToFind.Count; i++)
        {
            // ѕровер€ем, найдено ли слово, чтобы его перечеркнуть
            if (foundWords.Contains(wordsToFind[i]))
            {
                formattedText += $"<s>{wordsToFind[i]}</s>";
            }
            else
            {
                formattedText += wordsToFind[i];
            }

            // ≈сли это второе слово в паре или последнее слово, добавл€ем перенос строки
            if (i % 2 == 1 || i == wordsToFind.Count - 1)
            {
                formattedText += "\n";
            }
            else
            {
                // ≈сли это не конец строки, добавл€ем пробел
                formattedText += " ";
            }
        }

        wordListText.text = formattedText;
    }
}
