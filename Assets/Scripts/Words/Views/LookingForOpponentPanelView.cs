using UnityEngine;
using UnityEngine.UI;

public class LookingForOpponentPanelView : MonoBehaviour
{
    [SerializeField] private Button cancelSearchButton;
    [SerializeField] private OpponentSearchingText opponentSearchingText;

    private void OnEnable()
    {
        opponentSearchingText.StartSearchingAnimation();
    }

    private void Start()
    {
        cancelSearchButton.onClick.AddListener(OnCancelButtonClick);
    }
    
    public void HideCancelButton()
    {
        cancelSearchButton.gameObject.SetActive(false);
    }

    private void OnCancelButtonClick()
    {
        EventBus<OpponentSearchCancelledEvent>.Raise(new OpponentSearchCancelledEvent());
    }
}