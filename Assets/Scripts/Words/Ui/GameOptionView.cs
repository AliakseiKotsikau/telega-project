using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOptionView : MonoBehaviour, IPointerClickHandler 
{
    [SerializeField]
    private int starsAmount;
    [SerializeField]
    private TextMeshProUGUI optionText;
    
    private Image image;
    
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        optionText.text = $"{starsAmount} stars";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = Color.red;
    }
}
