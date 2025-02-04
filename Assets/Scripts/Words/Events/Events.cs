public interface IEvent
{
}

public struct TimeIsOutEvent : IEvent
{
}

public struct GameStartsEvent : IEvent
{
    public int TimeLimit;
}

public struct AllWordsFoundEvent : IEvent
{
}

public struct GameOptionSelectedEvent : IEvent
{
    public GameOptionView GameOption;
}

public struct PlayerBalanceUpdatedEvent : IEvent
{
    public int PlayerBalance;
}

public struct FindButtonPressedEvent : IEvent
{
}

public struct OpponentSearchCancelledEvent : IEvent
{
}

public struct OpponentFoundEvent : IEvent
{
    public string OpponentName;
}

public struct OpponentNameShownEvent : IEvent
{
}

public struct RewardClaimedEvent : IEvent
{
}