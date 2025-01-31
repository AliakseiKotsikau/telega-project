using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Image timerBar;
    [SerializeField]
    private TextMeshProUGUI timerText; 

    private EventBinding<GameStartsEvent> gameStartsEventBinding;

    private float timeLimit = 60f;
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

    private void StartTimer(GameStartsEvent gameStartsEvent)
    {
        timeLimit = gameStartsEvent.TimeLimit;
        timeRemaining = timeLimit;
        isRunning = true;
    }

    void Update()
    {
        if (!isRunning) return;
        
        timeRemaining -= Time.deltaTime;
        UpdateUI();

        if (timeRemaining > 0) return;
        
        timeRemaining = 0;
        isRunning = false;
        OnTimeExpired();
    }

    private void UpdateUI()
    {
        if (timerBar)
        {
            timerBar.fillAmount = timeRemaining / timeLimit;
        }
        if (timerText)
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
