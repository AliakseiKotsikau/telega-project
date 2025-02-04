using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasView : MonoBehaviour
{
    [SerializeField]
    private Timer timerProgressBar;
    [SerializeField]
    private GameObject timeOutPanel;
    [SerializeField]
    private WinPanelView winPanel;

    private void OnEnable()
    {
        winPanel.gameObject.SetActive(false);
        timeOutPanel.SetActive(false);
    }

    public void EnableTimeOutPanel()
    {
        timeOutPanel.SetActive(true);
    }

    public void EnableWinPanel()
    {
        winPanel.gameObject.SetActive(true);
    }
}
