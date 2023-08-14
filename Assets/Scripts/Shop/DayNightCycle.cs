using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    private SpriteRenderer backdrop;

    [SerializeField] private Sprite[] backDropSprites;
    [SerializeField] private TextMeshProUGUI timeDisplayText;

    private float currentTime = 12.0f;
    private const float endTime = 21.0f;
    private float timer = 0.0f;
    private float incrementInterval = 45.0f;

    private int currentBackdropIndex = 0;

    private void Awake()
    {
        backdrop = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= incrementInterval)
        {
            UpdateBackdrop();
            IncrementTime();
            timer = 0.0f;
        }

        UpdateTimeDisplay();
    }

    private void IncrementTime()
    {
        currentTime += 3.0f;
        if (currentTime > endTime)
            currentTime = endTime;
    }

    private void UpdateTimeDisplay()
    {
        int hours = Mathf.FloorToInt(currentTime);
        int minutes = Mathf.FloorToInt((currentTime - hours) * 60);
        string amPm = hours < 12 ? "AM" : "PM";

        hours %= 12;
        if (hours == 0)
            hours = 12;

        string timeDisplay = string.Format("{0:00}:{1:00} {2}", hours, minutes, amPm);
        timeDisplayText.text = timeDisplay;
    }

    private void UpdateBackdrop()
    {
        if (timer >= incrementInterval)
        {
            currentBackdropIndex++;
            if (currentBackdropIndex >= backDropSprites.Length)
                currentBackdropIndex = 0;

            backdrop.sprite = backDropSprites[currentBackdropIndex];
        }
    }
}
