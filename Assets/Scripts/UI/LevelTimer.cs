using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private float timerSeconds;

    private Stopwatch stopWatch;
    private TimeSpan timerDuration;
    private TimeSpan timerCountDown;
    private UIManager uiManager;
    private bool isEnabled = false;

    private void OnEnable()
    {
        isEnabled = true;
        if(stopWatch != null && !stopWatch.IsRunning)
            stopWatch.Start();
    }

    private void OnDisable()
    {
        isEnabled = false;
        if (stopWatch != null && stopWatch.IsRunning)
            stopWatch.Stop();
    }

    private void Awake()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        timerDuration = TimeSpan.FromMilliseconds(timerSeconds * 1000);
        timerCountDown = new TimeSpan(timerDuration.Ticks);
        stopWatch = Stopwatch.StartNew();
    }

    private void Update()
    {
        if (!isEnabled)
            return;

        if (timerCountDown.Seconds == 0)
        {
            if (stopWatch.IsRunning)
                stopWatch.Stop();
            if(!uiManager.IsGameOverScreenShowing())
                uiManager.ShowGameOver();
            return;
        }
        timerCountDown = timerDuration.Subtract(TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds));
        timerText.text = $"{timerCountDown.ToString("mm\\:ss")}";
    }
}
