using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    private float currentTime = 0f;
    private bool isRunning = false;
    private TMP_Text stopwatchText;

    private void Start()
    {
        stopwatchText = GetComponent<TMP_Text>();
        ResetStopwatch();
    }

    private void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            UpdateStopwatchDisplay();
        }
    }

    public void StartStopwatch()
    {
        isRunning = true;
    }

    public void PauseStopwatch()
    {
        isRunning = false;
    }

    public void ResetStopwatch()
    {
        currentTime = 0f;
        UpdateStopwatchDisplay();
        isRunning = false;
    }

    private void UpdateStopwatchDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        int milliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);

        stopwatchText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}