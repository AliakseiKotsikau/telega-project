public interface IEvent { }

public struct TimeIsOutEvent : IEvent { }

public struct GameStartsEvent : IEvent {
    public int TimeLimit;
}

public struct AllWordsFoundEvent : IEvent {
}

public struct PlayerEvent : IEvent {
    public int health;
    public int mana;
}