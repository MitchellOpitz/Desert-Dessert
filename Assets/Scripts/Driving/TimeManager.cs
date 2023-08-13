using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public int backdropSpriteIndex { get; set; }

    private float remainingTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public void SetRemainingTime(float time)
    {
        remainingTime = time;
    }
}
