using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LetterCell : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    [SerializeField]
    private TextMeshProUGUI cellText;

    [Header("Cell Data")]
    [SerializeField]
    private char letter;
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;

    public char Letter => letter;

    public void Setup(char letter, int x, int y)
    {
        this.letter = letter;
        this.x = x;
        this.y = y;

        if (cellText != null)
            cellText.text = letter.ToString();
    }

    /// <summary>
    /// Событие срабатывает, когда нажали (тапнули/кликнули) на этот UI-объект.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
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
            WordSelector.Instance.ContinueSelection(this);
        }
    }

    /// <summary>
    /// Срабатывает, когда отпускаем кнопку/палец 
    /// (если отпускание происходит над тем же объектом, который получил OnPointerDown).
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        WordSelector.Instance.EndSelection();
    }

    public void Highlight(Color color)
    {
        Image img = GetComponent<Image>();
        if (img != null)
        {
            img.color = color;
        }
    }
}