using UnityEngine;

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wordListText; // UI ������� ��� ����������� ������

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
            UpdateWordList(); // ��������� ������ � ������ ������ ���������� �����
        }
    }


    private void UpdateWordList()
    {
        string formattedText = "";
        for (int i = 0; i < wordsToFind.Count; i++)
        {
            // ���������, ������� �� �����, ����� ��� ������������
            if (foundWords.Contains(wordsToFind[i]))
            {
                formattedText += $"<s>{wordsToFind[i]}</s>";
            }
            else
            {
                formattedText += wordsToFind[i];
            }

            // ���� ��� ������ ����� � ���� ��� ��������� �����, ��������� ������� ������
            if (i % 2 == 1 || i == wordsToFind.Count - 1)
            {
                formattedText += "\n";
            }
            else
            {
                // ���� ��� �� ����� ������, ��������� ������
                formattedText += " ";
            }
        }

        wordListText.text = formattedText;
    }
}
