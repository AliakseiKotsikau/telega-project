using UnityEngine;

using System.Collections.Generic;
using TMPro;

public class WordsToFindManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wordListText;

    private List<string> wordsToFind = new List<string>();
    private List<string> foundWords = new List<string>();

    public void SetWordsToFind(List<string> words)
    {
        wordsToFind = new List<string>(words);
        UpdateWordList();
    }

    public void MarkWordFound(string word)
    {
        if (foundWords.Contains(word) || !wordsToFind.Contains(word)) return;
        
        foundWords.Add(word);
        UpdateWordList();
    }

    private void UpdateWordList()
    {
        string formattedText = "";
        for (var i = 0; i < wordsToFind.Count; i++)
        {
            // cross out word if it is already found
            if (foundWords.Contains(wordsToFind[i]))
            {
                formattedText += $"<s>{wordsToFind[i]}</s>";
            }
            else
            {
                formattedText += wordsToFind[i];
            }

            // show only two words in line
            if (i % 2 == 1 || i == wordsToFind.Count - 1)
            {
                formattedText += "\n";
            }
            else
            {
                formattedText += " ";
            }
        }

        wordListText.text = formattedText;
    }
}
