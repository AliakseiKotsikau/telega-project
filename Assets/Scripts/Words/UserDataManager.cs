using UnityEngine;

public class UserDataManager
{
    private const string StarsAmountKey = "StarsAmount";
    private const string PriceOfStartedGameKey = "PriceOfStartedGame";

    public static int GetStarsAmount()
    {
        return PlayerPrefs.GetInt(StarsAmountKey, 0);
    }

    private static void SetStarsAmount(int amount)
    {
        PlayerPrefs.SetInt(StarsAmountKey, amount);
        RaiseBalanceChangedEvent(amount);
    }

    private static void AddStars(int amount)
    {
        int currentAmount = GetStarsAmount();
        SetStarsAmount(currentAmount + amount);
    }

    private static void WithdrawStars(int amount)
    {
        int currentAmount = GetStarsAmount();
        SetStarsAmount(currentAmount - amount);
    }

    public static void InitUserBaseBalance()
    {
        if (!PlayerPrefs.HasKey(StarsAmountKey))
        {
            SetStarsAmount(5);
        }
        else
        {
            RaiseBalanceChangedEvent(GetStarsAmount());
        }
    }

    private static void RaiseBalanceChangedEvent(int amount)
    {
        EventBus<PlayerBalanceUpdatedEvent>.Raise(new PlayerBalanceUpdatedEvent { PlayerBalance = amount });
    }
    
    public static int GetPriceOfStartedGame()
    {
        return PlayerPrefs.GetInt(PriceOfStartedGameKey, 0);
    }
    
    public static void SetPriceOfStartedGame(int price)
    {
        PlayerPrefs.SetInt(PriceOfStartedGameKey, price);
    }
    
    public static void AddStarsRewardAndRemovePriceOfStartedGame()
    {
        AddStars(GetPriceOfStartedGame());
        PlayerPrefs.DeleteKey(PriceOfStartedGameKey);
    }
    
    public static void WithdrawStarsRewardAndRemovePriceOfStartedGame()
    {
        WithdrawStars(GetPriceOfStartedGame());
        PlayerPrefs.DeleteKey(PriceOfStartedGameKey);
    }
}