using UnityEngine;

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

    public void EnableTimeOutPanel()
    {
        gameCanvas.EnableTimeOutPanel();
    }

    public void EnableWinPanel()
    {
        gameCanvas.EnableWinPanel();
    }
}