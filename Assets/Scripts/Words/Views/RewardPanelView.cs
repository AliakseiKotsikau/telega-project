using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Button claimRewardButton;

    private void Start()
    {
        claimRewardButton.onClick.AddListener(OnClaimReward);
    }

    public void SetReward()
    {
        int reward = UserDataManager.GetPriceOfStartedGame();
        rewardText.text = $"Reward\n\n\n{reward} stars";
    }

    private void OnClaimReward()
    {
        EventBus<RewardClaimedEvent>.Raise(new RewardClaimedEvent());
    }
}