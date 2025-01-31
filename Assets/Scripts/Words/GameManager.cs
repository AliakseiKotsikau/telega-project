using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)]
    private int timeLimit;

    private UiManager uiManager;

    private EventBinding<TimeIsOutEvent> timeIsOutEventBinding;
    private EventBinding<AllWordsFoundEvent> allWordsFoundEventBinding;

    private void OnEnable()
    {
        timeIsOutEventBinding = new EventBinding<TimeIsOutEvent>(HandleTimeOut);
        EventBus<TimeIsOutEvent>.Register(timeIsOutEventBinding);
        
        allWordsFoundEventBinding = new EventBinding<AllWordsFoundEvent>(HandleFindingAllWords);
        EventBus<AllWordsFoundEvent>.Register(allWordsFoundEventBinding);
    }

    private void OnDisable()
    {
        EventBus<TimeIsOutEvent>.Deregister(timeIsOutEventBinding);
        EventBus<AllWordsFoundEvent>.Deregister(allWordsFoundEventBinding);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
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
                EventBus<GameStartsEvent>.Raise(new GameStartsEvent { TimeLimit = timeLimit });
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

    private void HandleFindingAllWords()
    {
        uiManager.EnableWinPanel();
    }
}
