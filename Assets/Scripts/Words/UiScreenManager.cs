using UnityEngine;
using Words.Views;

public class UiScreenManager : MonoBehaviour
{
    [SerializeField] private GameCanvasView gameCanvas;
    [SerializeField] private MenuCanvasView menuCanvas;
    
    public void EnableLookingForOpponentPanel()
    {
        menuCanvas.EnableLookingForOpponentPanel();
    }
    
    public void DisableLookingForOpponentPanel()
    {
        menuCanvas.DisableLookingForOpponentPanel();
    }
    
    public void HideLookingForOpponentPanelCancelButton()
    {
        menuCanvas.HideLookingForOpponentPanelCancelButton();
    }
    
    public void EnableGameCanvas()
    {
        gameCanvas.gameObject.SetActive(true);
        menuCanvas.gameObject.SetActive(false);
    }

    public void EnableMenuCanvas()
    {
        gameCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }

    public void EnableTimeOutPanel(int howMuchLost)
    {
        gameCanvas.EnableTimeOutPanelWithLostValue(howMuchLost);
    }

    public void EnableWinPanel()
    {
        gameCanvas.EnableWinPanel();
    }
}