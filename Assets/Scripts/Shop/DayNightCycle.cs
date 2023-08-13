using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    private SpriteRenderer backdrop;

    [SerializeField] private Sprite[] backDropSprites;
    [SerializeField] private float dayLength = 60f;

    private float timeOfDay = 0f;

    private void Awake()
    {
        backdrop = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CountTimeOfDay();
    }

    private void CountTimeOfDay()
    {
        timeOfDay += Time.deltaTime;

        if (timeOfDay > dayLength)
            timeOfDay = 0f;

        float timePercent = timeOfDay / dayLength;

        if (timePercent < 0.25f)
            backdrop.sprite = backDropSprites[0];
        else if (timePercent < 0.5f)
            backdrop.sprite = backDropSprites[1];
        else if (timePercent < 0.75f)
            backdrop.sprite = backDropSprites[2];
        else
            backdrop.sprite = backDropSprites[3];
    }
}
