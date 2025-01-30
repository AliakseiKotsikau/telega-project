using UnityEngine;
using UnityEngine.UI;

public class GameCanvasView : MonoBehaviour
{
    [SerializeField]
    private Timer timerProgressBar;
    [SerializeField]
    private GameObject timeOutPanel;

    public void EnableTimeOutPanel()
    {
        timeOutPanel.SetActive(true);
    }
}
