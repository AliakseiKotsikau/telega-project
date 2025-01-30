using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Image timerBar;
    [SerializeField]
    private TextMeshProUGUI timerText; 
    [SerializeField]
    [Range(1,100)]
    private float timeLimit = 60f;

    private EventBinding<GameStartsEvent> gameStartsEventBinding;

    private float timeRemaining;
    private bool isRunning = false;


    private void OnEnable()
    {
        gameStartsEventBinding = new EventBinding<GameStartsEvent>(StartTimer);
        EventBus<GameStartsEvent>.Register(gameStartsEventBinding);
    }

    private void OnDisable()
    {
        EventBus<GameStartsEvent>.Deregister(gameStartsEventBinding);
    }

    void StartTimer()
    {
        timeRemaining = timeLimit;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateUI();

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isRunning = false;
                OnTimeExpired();
            }
        }
    }

    private void UpdateUI()
    {
        if (timerBar != null)
        {
            timerBar.fillAmount = timeRemaining / timeLimit;
        }
        if (timerText != null)
        {
            timerText.text = $"{Mathf.Ceil(timeRemaining)}";
        }
    }

    private void OnTimeExpired()
    {
        Debug.Log("Time is Out!");
        EventBus<TimeIsOutEvent>.Raise(new TimeIsOutEvent());
    }
}
