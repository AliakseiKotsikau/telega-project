using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordSelector : MonoBehaviour
{
    public static WordSelector Instance;

    private WordsToFindManager wordsToFindManager;

    private List<string> validWords = new List<string>();

    private List<LetterCell> selectedCells = new List<LetterCell>();

    private void Awake()
    {
        Instance = this;
        wordsToFindManager = GetComponent<WordsToFindManager>();
    }

    public void SetValidWords(List<string> wordsOnBoard)
    {
        validWords = wordsOnBoard;
        wordsToFindManager.SetWordsToFind(wordsOnBoard);
    }

    public void StartSelection(LetterCell cell)
    {
        // Сбрасываем прошлый выбор
        selectedCells.Clear();
        AddCell(cell);
    }

    public void ContinueSelection(LetterCell cell)
    {
        if (!selectedCells.Contains(cell))
        {
            AddCell(cell);
        }
    }

    public void EndSelection()
    {
        // Сформировать слово
        string selectedWord = GetSelectedWord().ToUpper();

        // Проверить, есть ли оно в списке validWords
        bool isCorrect = validWords.Contains(selectedWord);

        if (isCorrect)
        {
            // Подсвечиваем зелёным
            foreach (var c in selectedCells)
            {
                c.Highlight(Color.green);
                var button = c.GetComponent<Button>();
                if (button != null) button.interactable = false;
            }

            wordsToFindManager.MarkWordFound(selectedWord);

            // (необязательно) Удаляем это слово из validWords, чтобы повторно его нельзя было «найти»
            validWords.Remove(selectedWord);

            Debug.Log($"Правильное слово: {selectedWord}");
        }
        else
        {
            // Сбрасываем цвет на белый
            foreach (var c in selectedCells)
            {
                c.Highlight(Color.white);
            }

            Debug.Log($"Неправильное слово: {selectedWord}");
        }

        // Очищаем список выбранных ячеек
        selectedCells.Clear();

        if (validWords.Count == 0)
        {
            EventBus<AllWordsFoundEvent>.Raise(new AllWordsFoundEvent());
        }
    }

    private void AddCell(LetterCell cell)
    {
        selectedCells.Add(cell);
        cell.Highlight(Color.yellow);
    }

    private string GetSelectedWord()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (var c in selectedCells)
        {
            sb.Append(c.Letter);
        }
        return sb.ToString();
    }
}