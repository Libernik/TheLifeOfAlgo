using TMPro;
using UnityEngine;
using System;

public class LevelTimer : MonoBehaviour
{
    private float timeLeft = 30f;

    [SerializeField] private TextMeshProUGUI timerText;

    private bool active = false;

    public event Action OnTimeOver;

    void Update()
    {
        if (!active)
            return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            timeLeft = 0;

            active = false;

            Debug.Log("TIME OVER");

            OnTimeOver?.Invoke();

        }

        UpdateUI();
    }

    void UpdateUI()
    {
        timerText.text =
            Mathf.CeilToInt(timeLeft)
            .ToString();
    }

    public async void SpendTime(float amount)
    {
        timeLeft -= amount;

        if (timeLeft < 0)
            timeLeft = 0;

        UpdateUI();
    }

    public void StartTimer()
    {
        GameObject.Find("StartButton").SetActive(false);
        active = true;
    }

    public void SetTimer(float time)
    {
        timeLeft = time;
        UpdateUI();
    }
}