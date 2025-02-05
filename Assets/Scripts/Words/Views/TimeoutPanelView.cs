using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Words.Views
{
    public class TimeoutPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI losePriceText;
        [SerializeField] private Button backToMenuButton;

        private void Start()
        {
            backToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);
        }
        
        public void SetLostValue(int howMuchLost)
        {
            losePriceText.text = $"-{howMuchLost} stars";
        }
        
        private void OnBackToMenuButtonClick()
        {
            EventBus<BackToMenuPressedOnTimeoutPanelEvent>.Raise(new BackToMenuPressedOnTimeoutPanelEvent());
        }
    }
}