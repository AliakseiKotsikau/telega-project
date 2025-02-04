using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOptionView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int starsAmount;
    [SerializeField] private TextMeshProUGUI optionText;
    
    public int StarsAmount => starsAmount;

    private Image image;

    private Color baseColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        baseColor = image.color;
    }

    private void Start()
    {
        optionText.text = $"{starsAmount} stars";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = Color.red;
        EventBus<GameOptionSelectedEvent>.Raise(new GameOptionSelectedEvent { GameOption = this });
    }

    public void ResetColor()
    {
        image.color = baseColor;
    }
}