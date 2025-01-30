using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UiManager uiManager;

    private EventBinding<TimeIsOutEvent> timeIsOutEventBinding;

    private void OnEnable()
    {
        timeIsOutEventBinding = new EventBinding<TimeIsOutEvent>(HandleTimeOut);
        EventBus<TimeIsOutEvent>.Register(timeIsOutEventBinding);
    }

    private void OnDisable()
    {
        EventBus<TimeIsOutEvent>.Deregister(timeIsOutEventBinding);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        uiManager = GetComponent<UiManager>();
    }

    private void Start()
    {
        ChangeState(GameState.START);
    }

    private void ChangeState(GameState newState)
    {
        switch (newState)
        {
            case GameState.SET_UP:
                break;
            case GameState.START:
                uiManager.StartGame();
                break;
            case GameState.PLAYING:
                break;
            case GameState.WIN:
                break;
            case GameState.LOSE:
                break;
            case GameState.TIME_OUT:
                HandleTimeOut();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandleTimeOut()
    {
        uiManager.EnableTimeOutPanel();
    }
}
