using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordSelector : MonoBehaviour
{
    public static WordSelector Instance;

    // Список текущих выбранных ячеек
    private List<LetterCell> selectedCells = new List<LetterCell>();

    private void Awake()
    {
        Instance = this;
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
        bool isCorrect = BoardManagerInstance().validWords.Contains(selectedWord);
        Debug.Log(isCorrect);
        if (isCorrect)
        {
            // Подсвечиваем зелёным
            foreach (var c in selectedCells)
            {
                HighlightCell(c, Color.green);
                // Делаем неактивной кнопку, чтобы больше нельзя было нажать/выбрать эту клетку
                var button = c.GetComponentInChildren<Button>();
                if (button != null) button.interactable = false;
            }

            // (необязательно) Удаляем это слово из validWords, чтобы повторно его нельзя было «найти»
            BoardManagerInstance().validWords.Remove(selectedWord);

            Debug.Log($"Правильное слово: {selectedWord}");
        }
        else
        {
            // Сбрасываем цвет на белый
            foreach (var c in selectedCells)
            {
                HighlightCell(c, Color.white);
            }

            Debug.Log($"Неправильное слово: {selectedWord}");
        }

        // Очищаем список выбранных ячеек
        selectedCells.Clear();
    }

    // Помощник, чтобы не писать BoardManager.Instance в каждой строке
    private BoardManager BoardManagerInstance()
    {
        return FindObjectOfType<BoardManager>();
        // или, если у вас Singleton-доступ к BoardManager, то: return BoardManager.Instance;
    }

    private void AddCell(LetterCell cell)
    {
        selectedCells.Add(cell);
        HighlightCell(cell, Color.yellow);
    }

    private string GetSelectedWord()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (var c in selectedCells)
        {
            sb.Append(c.letter);
        }
        return sb.ToString();
    }

    // Меняем цвет фона ячейки (если есть Image)
    private void HighlightCell(LetterCell cell, Color color)
    {
        Image img = cell.GetComponentInChildren<Image>();
        if (img != null)
        {
            img.color = color;
        }
    }
}