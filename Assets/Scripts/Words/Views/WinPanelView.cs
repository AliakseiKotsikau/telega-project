using System;
using System.Collections;
using UnityEngine;

public class WinPanelView : MonoBehaviour
{
    [SerializeField]
    private RewardPanelView rewardPanelView;

    private void OnEnable()
    {
        StartCoroutine(EnableRewardPanelAfterDelay());
    }

    private IEnumerator EnableRewardPanelAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        rewardPanelView.gameObject.SetActive(true);
        rewardPanelView.SetReward();
    }
}
