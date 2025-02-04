using System.Collections;
using UnityEngine;

public class OpponentSearcher : MonoBehaviour
{
    public void FindOpponent()
    {
        StartCoroutine(StartSearching());
    }

    private IEnumerator StartSearching()
    {
        yield return new WaitForSeconds(3f);
        EventBus<OpponentFoundEvent>.Raise(new OpponentFoundEvent{OpponentName = "Sopiernik"});
    }
}
