using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class OpponentSearchingText : MonoBehaviour
{
    private TextMeshProUGUI lookingForOpponentText;
    
    private bool isSearching ;
    
    public void StartSearchingAnimation()
    {
        isSearching = true;
        lookingForOpponentText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartDotsAnimation());
    }
    
    public void StopSearchingAnimation()
    {
        isSearching = false;
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
            yield return new WaitForSeconds(0.3f);
        }
        
        lookingForOpponentText.text = "Looking for an opponent:\n\n\n\nFound: ";
    }
}
