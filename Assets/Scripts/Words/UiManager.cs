using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameCanvasView gameCanvas;

    public void EnableTimeOutPanel()
    {
        gameCanvas.EnableTimeOutPanel();
    }
    
    public void EnableWinPanel()
    {
        gameCanvas.EnableWinPanel();
    }
}
