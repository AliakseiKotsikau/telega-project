using UnityEngine;

namespace Words.Views
{
    public class GameCanvasView : MonoBehaviour
    {
        [SerializeField] private Timer timerProgressBar;
        [SerializeField] private TimeoutPanelView timeOutPanel;
        [SerializeField] private WinPanelView winPanel;

        private void OnEnable()
        {
            winPanel.gameObject.SetActive(false);
            timeOutPanel.gameObject.SetActive(false);
        }

        public void EnableTimeOutPanelWithLostValue(int howMuchLost)
        {
            timeOutPanel.gameObject.SetActive(true);
            timeOutPanel.SetLostValue(howMuchLost);
        }

        public void EnableWinPanel()
        {
            winPanel.gameObject.SetActive(true);
        }
    }
}