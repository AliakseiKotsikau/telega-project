using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OpponentSearchingText : MonoBehaviour
{
    private TextMeshProUGUI lookingForOpponentText;
    
    private bool isSearching;
    private string opponentName;
    
    private EventBinding<OpponentFoundEvent> opponentFoundEventBinding;
    
    private void OnEnable()
    {
        opponentFoundEventBinding = new EventBinding<OpponentFoundEvent>(OnOpponentFound);
        EventBus<OpponentFoundEvent>.Register(opponentFoundEventBinding);
    }
    
    private void OnDisable()
    {
        EventBus<OpponentFoundEvent>.Deregister(opponentFoundEventBinding);
    }
    
    private void OnOpponentFound(OpponentFoundEvent opponentFoundEvent)
    {
        opponentName = opponentFoundEvent.OpponentName;
        isSearching = false;
    }
    
    public void StartSearchingAnimation()
    {
        isSearching = true;
        lookingForOpponentText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartDotsAnimation());
    }
    
    private IEnumerator StartDotsAnimation()
    {
        int i = 0;
        while (isSearching)
        {
            lookingForOpponentText.text = "Looking for an opponent:\n " +  new String('*', i);
            i++;
            if (i > 10)
            {
                i = 1;
            }
            yield return new WaitForSeconds(0.2f);
        }
        
        lookingForOpponentText.text = "Looking for an opponent:\n\n\n\nFound: " + opponentName;

        yield return new WaitForSeconds(1f);
        EventBus<OpponentNameShownEvent>.Raise(new OpponentNameShownEvent());
    }
}
