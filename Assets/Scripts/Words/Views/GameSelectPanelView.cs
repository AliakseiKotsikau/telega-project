using System;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectPanelView : MonoBehaviour
{
    [SerializeField] private Button findButton;
    [SerializeField] private GameOptionView[] gameOptionViews;
    
    private EventBinding<GameOptionSelectedEvent> gameSelectedEventBinding;
    
    private void OnEnable()
    {
        gameSelectedEventBinding = new EventBinding<GameOptionSelectedEvent>(OnGameSelected);
        EventBus<GameOptionSelectedEvent>.Register(gameSelectedEventBinding);
    }

    private void OnDisable()
    {
        EventBus<GameOptionSelectedEvent>.Deregister(gameSelectedEventBinding);
    }

    private void Start()
    {
        findButton.onClick.AddListener(OnFindButton);
        findButton.interactable = false;
    }

    private void OnFindButton()
    {
        EventBus<FindButtonPressedEvent>.Raise(new FindButtonPressedEvent());
    }
    
    private void OnGameSelected(GameOptionSelectedEvent gameOptionSelectedEvent)
    {
        findButton.interactable = true;
        foreach (var gameOption in gameOptionViews)
        {
            if (gameOption != gameOptionSelectedEvent.GameOption)
            {
                gameOption.ResetColor();
            }
        }
    }
}
