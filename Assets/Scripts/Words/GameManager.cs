using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)]
    private int timeLimit;

    private UiScreenManager uiScreenManager;
    private OpponentSearcher opponentSearcher;

    private EventBinding<TimeIsOutEvent> timeIsOutEventBinding;
    private EventBinding<AllWordsFoundEvent> allWordsFoundEventBinding;
    private EventBinding<FindButtonPressedEvent> findButtonPressedEventBinding;
    private EventBinding<OpponentSearchCancelledEvent> opponentSearchCancelledEventBinding;

    private void OnEnable()
    {
        timeIsOutEventBinding = new EventBinding<TimeIsOutEvent>(() => ChangeState(GameState.TIME_OUT));
        EventBus<TimeIsOutEvent>.Register(timeIsOutEventBinding);
        
        allWordsFoundEventBinding = new EventBinding<AllWordsFoundEvent>(() => ChangeState(GameState.WIN));
        EventBus<AllWordsFoundEvent>.Register(allWordsFoundEventBinding);
        
        findButtonPressedEventBinding = new EventBinding<FindButtonPressedEvent>(() => ChangeState(GameState.SEARCHING_FOR_OPPONONET));
        EventBus<FindButtonPressedEvent>.Register(findButtonPressedEventBinding);
        
        opponentSearchCancelledEventBinding = new EventBinding<OpponentSearchCancelledEvent>(() => ChangeState(GameState.SEARCH_CANCELLED));
        EventBus<OpponentSearchCancelledEvent>.Register(opponentSearchCancelledEventBinding);
    }

    private void OnDisable()
    {
        EventBus<TimeIsOutEvent>.Deregister(timeIsOutEventBinding);
        EventBus<AllWordsFoundEvent>.Deregister(allWordsFoundEventBinding);
        EventBus<FindButtonPressedEvent>.Deregister(findButtonPressedEventBinding);
        EventBus<OpponentSearchCancelledEvent>.Deregister(opponentSearchCancelledEventBinding);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        uiScreenManager = GetComponent<UiScreenManager>();
        
        // add logic components
        opponentSearcher = gameObject.GetOrAddComponent<OpponentSearcher>();
    }

    private void Start()
    {
        ChangeState(GameState.SET_UP);
    }
    

    private void ChangeState(GameState newState)
    {
        switch (newState)
        {
            case GameState.SET_UP:
                HandleSetUp();
                break;
            case GameState.SEARCHING_FOR_OPPONONET:
                HandleOpponentSearching();
                break;
            case GameState.SEARCH_CANCELLED:
                HandleSearchCancel();
                break;
            case GameState.START:
                EventBus<GameStartsEvent>.Raise(new GameStartsEvent { TimeLimit = timeLimit });
                break;
            case GameState.PLAYING:
                break;
            case GameState.WIN:
                HandleFindingAllWords();
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

    private void HandleSetUp()
    {
        UserDataManager.InitUserBaseBalance();
        uiScreenManager.EnableMenuCanvas();
        ChangeState(GameState.START);
    }
    
    private void HandleOpponentSearching()
    {
        uiScreenManager.EnableLookingForOpponentPanel();
    }
    
    private void HandleSearchCancel()
    {
        uiScreenManager.DisableLookingForOpponentPanel();
    }

    private void HandleTimeOut()
    {
        uiScreenManager.EnableTimeOutPanel();
    }

    private void HandleFindingAllWords()
    {
        uiScreenManager.EnableWinPanel();
    }
    
}
