using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Параметры поля")]
    [SerializeField]
    public int boardSize = 10;                        // Размер NxN
    [SerializeField]
    public GameObject letterCellPrefab;              // Префаб ячейки (с LetterCell)
    [SerializeField]
    public GameObject canvas;                        // Canvas, на котором размещаются ячейки

    [Header("Содержание поля")]
    [SerializeField]
    private string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // Буквы для случайной заливки

    private List<string> wordsToPlace;                         

    private RandomWordSelector randomWordSelector;
    private WordSelector wordSelector;

    // Вспомогательная матрица символов (букв)
    private char[,] letterGrid;

    // Массив всех ячеек (скриптов), чтобы можно было обращаться извне
    private LetterCell[,] grid;

    private void Awake()
    {
        randomWordSelector = GetComponent<RandomWordSelector>();
        wordSelector = GetComponent<WordSelector>();
    }

    void Start()
    {
        randomWordSelector.OnWordsLoadFinished += GenerateBoardWithWords;
    }

    private void GenerateBoardWithWords()
    {
        wordsToPlace = randomWordSelector.GetRandomWordSet();
        GenerateBoard();
    }


    /// <summary>
    /// Генерация поля NxN с размещением заданных слов и заполнением пустот.
    /// </summary>
    void GenerateBoard()
    {
        // 1) Создадим двумерный массив символов и инициализируем пробелами (пустыми)
        letterGrid = new char[boardSize, boardSize];
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                letterGrid[x, y] = ' '; // обозначаем пустую ячейку
            }
        }

        // 2) Размещаем все слова из списка wordsToPlace
        PlaceWordsInGrid(wordsToPlace);

        // 3) Заполняем оставшиеся пустые ячейки случайными буквами
        FillEmptyCellsWithRandomLetters();

        // 4) Создаём реальные объекты (префабы LetterCell) на сцене (Canvas)
        CreateBoardObjects();
    }

    /// <summary>
    /// Шаг 2: Размещение заданных слов в матрице (только горизонталь и вертикаль, вперёд и назад).
    /// </summary>
    private void PlaceWordsInGrid(List<string> words)
    {
        // Возможные направления (горизонталь/вертикаль, вперёд/назад)
        // dx, dy: шаг по оси X и Y
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 0),   // слева -> направо
           // new Vector2Int(-1, 0),  // справа -> налево
            new Vector2Int(0, 1),   // сверху -> вниз
            //new Vector2Int(0, -1)   // снизу -> вверх
        };

        List<string> validWords = new List<string>();

        foreach (string word in words)
        {
            // Попробуем несколько раз подобрать позицию и направление
            bool placed = false;
            // Приведём слово к верхнему регистру, чтобы совпадало с letters
            string upperWord = word.ToUpper();

            for (int attempt = 0; attempt < 100 && !placed; attempt++)
            {
                Vector2Int dir = directions[Random.Range(0, directions.Length)];

                // Случайная стартовая позиция на поле
                int startX = Random.Range(0, boardSize);
                int startY = Random.Range(0, boardSize);

                // Проверка: влезает ли слово с такой стартовой позицией и направлением
                if (CanPlaceWord(upperWord, startX, startY, dir))
                {
                    // Если можно, то размещаем
                    PlaceWord(upperWord, startX, startY, dir);
                    placed = true;
                }
            }

            // Если после 100 попыток не удалось – можно вывести предупреждение
            if (placed)
            {
                // Сохраняем слово в набор "правильных"
                validWords.Add(upperWord);
            }
            else
            {
                Debug.LogWarning($"Не удалось разместить слово '{word}' на поле.");
            }
        }

        wordSelector.SetValidWords(validWords);
    }

    /// <summary>
    /// Проверяем, можно ли разместить слово word, начиная с (startX,startY) в направлении dir.
    /// </summary>
    private bool CanPlaceWord(string word, int startX, int startY, Vector2Int dir)
    {
        int x = startX;
        int y = startY;

        for (int i = 0; i < word.Length; i++)
        {
            // Выходим ли за границы поля?
            if (x < 0 || x >= boardSize || y < 0 || y >= boardSize)
                return false;

            // Если ячейка не пустая и не совпадает с нужной буквой, значит нельзя
            if (letterGrid[x, y] != ' ' && letterGrid[x, y] != word[i])
                return false;

            // Переходим к следующей букве
            x += dir.x;
            y += dir.y;
        }
        return true;
    }

    /// <summary>
    /// Размещаем (записываем) слово word в матрицу letterGrid
    /// (предполагая, что проверка CanPlaceWord уже пройдена).
    /// </summary>
    private void PlaceWord(string word, int startX, int startY, Vector2Int dir)
    {
        int x = startX;
        int y = startY;

        for (int i = 0; i < word.Length; i++)
        {
            letterGrid[x, y] = word[i];
            x += dir.x;
            y += dir.y;
        }
    }

    /// <summary>
    /// Шаг 3: Заполнить все пустые (пробелы) клетки случайными буквами.
    /// </summary>
    private void FillEmptyCellsWithRandomLetters()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                if (letterGrid[x, y] == ' ')
                {
                    letterGrid[x, y] = letters[Random.Range(0, letters.Length)];
                }
            }
        }
    }

    /// <summary>
    /// Шаг 4: На основе итогового массива letterGrid создаём объекты LetterCell на Canvas.
    /// </summary>
    private void CreateBoardObjects()
    {
        // Создаём сетку ячеек NxN (скриптов LetterCell)
        grid = new LetterCell[boardSize, boardSize];

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                // Создаём ячейку
                GameObject cellObj = Instantiate(letterCellPrefab, canvas.transform);

                // Получаем скрипт LetterCell
                LetterCell letterCell = cellObj.GetComponent<LetterCell>();

                // Устанавливаем букву из нашей матрицы (уже с учётом размещённых слов)
                char letter = letterGrid[x, y];
                letterCell.Setup(letter, x, y);

                // Сохраняем скрипт в двумерном массиве, чтобы при желании обращаться к ячейкам
                grid[x, y] = letterCell;
            }
        }
    }
}
