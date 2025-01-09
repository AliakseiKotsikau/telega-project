using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; // Если вы используете TextMeshPro

public class LetterCell : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    [Header("Данные ячейки")]
    public char letter;       // Какая буква хранится
    public int x;             // Координата в матрице
    public int y;             // Координата в матрице

    private TextMeshProUGUI cellText;    // Если используете обычный Text


    private void Awake()
    {
        // Предположим, что в префабе есть компонент Text
        cellText = GetComponentInChildren<TextMeshProUGUI>();

    }

    /// <summary>
    /// Инициализация ячейки
    /// </summary>
    public void Setup(char letter, int x, int y)
    {
        this.letter = letter;
        this.x = x;
        this.y = y;

        if (cellText != null)
            cellText.text = letter.ToString();

        // if (cellTextTMP != null)
        //    cellTextTMP.text = letter.ToString();
    }

    /// <summary>
    /// Событие срабатывает, когда нажали (тапнули/кликнули) на этот UI-объект.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"OnPointerDown на букве: {letter}");
        // Начинаем выбор
        WordSelector.Instance.StartSelection(this);

        // Важно: чтобы при "таскании" зажата была именно эта ячейка
        eventData.useDragThreshold = false;
        // Можете задать eventData.pointerDrag = gameObject, если нужно
    }

    /// <summary>
    /// Срабатывает, когда, удерживая клик/палец, мы "заходим" на этот объект.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Чтобы вызывать ContinueSelection только при «зажатой» левой кнопке/тапе
        // можно проверить:
        if (eventData.pointerDrag != null || Input.GetMouseButton(0))
        {
            Debug.Log($"OnPointerEnter на букве: {letter}");
            WordSelector.Instance.ContinueSelection(this);
        }
    }

    /// <summary>
    /// Срабатывает, когда отпускаем кнопку/палец 
    /// (если отпускание происходит над тем же объектом, который получил OnPointerDown).
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"OnPointerUp на букве: {letter}");
        WordSelector.Instance.EndSelection();
    }
}