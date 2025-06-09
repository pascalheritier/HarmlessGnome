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

    private void Awake()
    {
        timerDuration = TimeSpan.FromMilliseconds(timerSeconds * 1000);
        timerCountDown = new TimeSpan(timerDuration.Ticks);
        stopWatch = Stopwatch.StartNew();
    }

    private void Update()
    {
        if (timerCountDown.Seconds == 0)
        {
            if (stopWatch.IsRunning)
                stopWatch.Stop();
            return;
        }
        timerCountDown = timerDuration.Subtract(TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds));
        timerText.text = $"{timerCountDown.ToString("mm\\:ss")}";
    }
}
