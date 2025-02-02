using UnityEngine;

public class MenuCanvasView : MonoBehaviour
{
    [SerializeField] private GameSelectPanelView gameSelectPanelView;
    [SerializeField] private LookingForOpponentPanelView lookingForOpponentPanelView;
    
    public void EnableLookingForOpponentPanel()
    {
        lookingForOpponentPanelView.gameObject.SetActive(true);
    }
    
    public void DisableLookingForOpponentPanel()
    {
        lookingForOpponentPanelView.gameObject.SetActive(false);
    }
}