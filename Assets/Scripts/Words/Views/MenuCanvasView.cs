using UnityEngine;

public class MenuCanvasView : MonoBehaviour
{
    [SerializeField] private GameSelectPanelView gameSelectPanelView;
    [SerializeField] private LookingForOpponentPanelView lookingForOpponentPanelView;

    private void OnEnable()
    {
        lookingForOpponentPanelView.gameObject.SetActive(false);
    }
    
    public void EnableLookingForOpponentPanel()
    {
        lookingForOpponentPanelView.gameObject.SetActive(true);
    }
    
    public void DisableLookingForOpponentPanel()
    {
        lookingForOpponentPanelView.gameObject.SetActive(false);
    }
    
    public void HideLookingForOpponentPanelCancelButton()
    {
        lookingForOpponentPanelView.HideCancelButton();
    }
}