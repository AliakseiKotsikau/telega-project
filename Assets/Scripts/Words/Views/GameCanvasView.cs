using UnityEngine;
using UnityEngine.UI;

public class GameCanvasView : MonoBehaviour
{
    [SerializeField]
    private Timer timerProgressBar;
    [SerializeField]
    private GameObject timeOutPanel;
    [SerializeField]
    private GameObject winPanel;

    public void EnableTimeOutPanel()
    {
        timeOutPanel.SetActive(true);
    }

    public void EnableWinPanel()
    {
        winPanel.SetActive(true);
    }
}
