using TMPro;
using UnityEngine;

public class PlayerInfoPanelView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerName;
    [SerializeField]
    private TextMeshProUGUI playerBalance;
    
    private EventBinding<PlayerBalanceUpdatedEvent> playerBalanceUpdatedEventBinding;
    
    private void OnEnable()
    {
        playerBalanceUpdatedEventBinding = new EventBinding<PlayerBalanceUpdatedEvent>(OnPlayerBalanceUpdated);
        EventBus<PlayerBalanceUpdatedEvent>.Register(playerBalanceUpdatedEventBinding);
    }
    
    private void OnDisable()
    {
        EventBus<PlayerBalanceUpdatedEvent>.Deregister(playerBalanceUpdatedEventBinding);
    }
    
    private void OnPlayerBalanceUpdated(PlayerBalanceUpdatedEvent playerBalanceUpdatedEvent)
    {
        playerBalance.text = $"Balance:\n {playerBalanceUpdatedEvent.PlayerBalance} stars";
    }
}
