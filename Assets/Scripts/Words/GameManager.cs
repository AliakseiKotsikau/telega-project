using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] private int timeLimit;

    private UiScreenManager uiScreenManager;
    private OpponentSearcher opponentSearcher;

    private EventBinding<GameOptionSelectedEvent> gameOptionSelectedEventBinding;
    private EventBinding<FindButtonPressedEvent> findButtonPressedEventBinding;
    private EventBinding<OpponentSearchCancelledEvent> opponentSearchCancelledEventBinding;
    private EventBinding<OpponentFoundEvent> opponentFoundEventBinding;
    private EventBinding<OpponentNameShownEvent> opponentNameShownEventBinding;
    private EventBinding<TimeIsOutEvent> timeIsOutEventBinding;
    private EventBinding<AllWordsFoundEvent> allWordsFoundEventBinding;
    private EventBinding<RewardClaimedEvent> rewardClaimedEventBinding;
    private EventBinding<BackToMenuPressedOnTimeoutPanelEvent> backToMenuPressedOnTimeoutPanelEventBinding;

    private void OnEnable()
    {
        BindEventsToHandlers();
    }

    private void OnDisable()
    {
        EventBus<GameOptionSelectedEvent>.Deregister(gameOptionSelectedEventBinding);
        EventBus<FindButtonPressedEvent>.Deregister(findButtonPressedEventBinding);
        EventBus<OpponentSearchCancelledEvent>.Deregister(opponentSearchCancelledEventBinding);
        EventBus<OpponentFoundEvent>.Deregister(opponentFoundEventBinding);
        EventBus<OpponentNameShownEvent>.Deregister(opponentNameShownEventBinding);
        EventBus<TimeIsOutEvent>.Deregister(timeIsOutEventBinding);
        EventBus<AllWordsFoundEvent>.Deregister(allWordsFoundEventBinding);
        EventBus<RewardClaimedEvent>.Deregister(rewardClaimedEventBinding);
        EventBus<BackToMenuPressedOnTimeoutPanelEvent>.Deregister(backToMenuPressedOnTimeoutPanelEventBinding);
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
        HandleSetUp();
    }

    private void BindEventsToHandlers()
    {
        gameOptionSelectedEventBinding = new EventBinding<GameOptionSelectedEvent>(HandleGameOptionSelection);
        EventBus<GameOptionSelectedEvent>.Register(gameOptionSelectedEventBinding);

        findButtonPressedEventBinding = new EventBinding<FindButtonPressedEvent>(HandleOpponentSearching);
        EventBus<FindButtonPressedEvent>.Register(findButtonPressedEventBinding);

        opponentSearchCancelledEventBinding = new EventBinding<OpponentSearchCancelledEvent>(HandleSearchCancel);
        EventBus<OpponentSearchCancelledEvent>.Register(opponentSearchCancelledEventBinding);

        opponentFoundEventBinding = new EventBinding<OpponentFoundEvent>(HandleOpponentFound);
        EventBus<OpponentFoundEvent>.Register(opponentFoundEventBinding);

        opponentNameShownEventBinding = new EventBinding<OpponentNameShownEvent>(HandleStart);
        EventBus<OpponentNameShownEvent>.Register(opponentNameShownEventBinding);

        timeIsOutEventBinding = new EventBinding<TimeIsOutEvent>(HandleTimeOut);
        EventBus<TimeIsOutEvent>.Register(timeIsOutEventBinding);

        allWordsFoundEventBinding = new EventBinding<AllWordsFoundEvent>(HandleFindingAllWords);
        EventBus<AllWordsFoundEvent>.Register(allWordsFoundEventBinding);

        rewardClaimedEventBinding = new EventBinding<RewardClaimedEvent>(HandleRewardClaimed);
        EventBus<RewardClaimedEvent>.Register(rewardClaimedEventBinding);

        backToMenuPressedOnTimeoutPanelEventBinding =
            new EventBinding<BackToMenuPressedOnTimeoutPanelEvent>(HandleBackToMenuPressed);
        EventBus<BackToMenuPressedOnTimeoutPanelEvent>.Register(backToMenuPressedOnTimeoutPanelEventBinding);
    }

    private void HandleSetUp()
    {
        UserDataManager.InitUserBaseBalance();
        uiScreenManager.EnableMenuCanvas();
    }

    private void HandleGameOptionSelection(GameOptionSelectedEvent gameOptionSelectedEvent)
    {
        UserDataManager.SetPriceOfStartedGame(gameOptionSelectedEvent.GameOption.StarsAmount);
    }

    private void HandleOpponentSearching()
    {
        uiScreenManager.EnableLookingForOpponentPanel();
        opponentSearcher.FindOpponent();
    }

    private void HandleSearchCancel()
    {
        uiScreenManager.DisableLookingForOpponentPanel();
    }

    private void HandleOpponentFound()
    {
        uiScreenManager.HideLookingForOpponentPanelCancelButton();
    }

    private void HandleStart()
    {
        uiScreenManager.EnableGameCanvas();
        EventBus<GameStartsEvent>.Raise(new GameStartsEvent { TimeLimit = timeLimit });
    }

    private void HandleTimeOut()
    {
        int priceOfGame = UserDataManager.GetPriceOfStartedGame();
        uiScreenManager.EnableTimeOutPanel(priceOfGame);
    }

    private void HandleBackToMenuPressed()
    {
        uiScreenManager.EnableMenuCanvas();
        UserDataManager.WithdrawStarsRewardAndRemovePriceOfStartedGame();
    }

    private void HandleFindingAllWords()
    {
        uiScreenManager.EnableWinPanel();
    }

    private void HandleRewardClaimed()
    {
        uiScreenManager.EnableMenuCanvas();
        uiScreenManager.DisableLookingForOpponentPanel();
        UserDataManager.AddStarsRewardAndRemovePriceOfStartedGame();
    }
}