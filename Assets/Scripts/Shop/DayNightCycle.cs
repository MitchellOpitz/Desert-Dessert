using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    private SpriteRenderer backdrop;

    [SerializeField] private Sprite[] backDropSprites;
    [SerializeField] private float dayLength = 180f;
    [SerializeField] private TextMeshProUGUI timeDisplayText;

    private float timeOfDay = 0f;

    private void Awake()
    {
        backdrop = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CountTimeOfDay();
        UpdateTimeDisplay();
        UpdateBackdrop();
    }

    private void CountTimeOfDay()
    {
        timeOfDay += Time.deltaTime;

        if (timeOfDay > dayLength)
            timeOfDay = 0f;
    }

    private void UpdateTimeDisplay()
    {
        float currentTimePercent = (timeOfDay / dayLength);
        int totalMinutes = Mathf.FloorToInt(currentTimePercent * 13 * 60);
        int hours = 7 + totalMinutes / 60;
        int minutes = (totalMinutes % 60) / 30 * 30;

        string amPm = (hours >= 12) ? "PM" : "AM";
        if (hours > 12)
            hours -= 12;

        string timeText = string.Format("{0:00}:{1:00} {2}", hours, minutes, amPm);
        timeDisplayText.text = timeText;
    }

    private void UpdateBackdrop()
    {
        int hours = 7 + Mathf.FloorToInt(timeOfDay / (dayLength / 15));
        int backdropIndex = 0;

        if (hours >= 7 && hours < 12)
            backdropIndex = 0;
        else if (hours >= 12 && hours < 17)
            backdropIndex = 1;
        else if (hours >= 17 && hours < 20)
            backdropIndex = 2;
        else
            backdropIndex = 3;

        backdrop.sprite = backDropSprites[backdropIndex];
    }
}
