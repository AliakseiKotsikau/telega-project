using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameCanvasView gameCanvas;

    public void StartGame()
    {
        EventBus<GameStartsEvent>.Raise(new GameStartsEvent());
    }

    public void EnableTimeOutPanel()
    {
        gameCanvas.EnableTimeOutPanel();
    }
}
