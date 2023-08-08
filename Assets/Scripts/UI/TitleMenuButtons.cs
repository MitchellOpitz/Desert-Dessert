using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuButtons : MonoBehaviour
{
    [SerializeField]
    Transform self;

    [SerializeField]
    Vector2 titleCanvas;

    [SerializeField]
    Vector2 settingsCanvas;

    [SerializeField]
    Vector2 creditsCanvas;

    [SerializeField]
    float slideSpeed;

    [SerializeField]
    float velocity;

    [SerializeField]
    Canvas title;

    [SerializeField]
    Canvas settings;

    [SerializeField]
    Canvas credits;
    private Vector2 targetCanvas;

    void Start()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void PlayGameButton()
    {
        Debug.Log("Play Game");
    }

    public void SettingsButton()
    {
        targetCanvas = settingsCanvas;
    }

    public void BackFromSettings()
    {
        targetCanvas = titleCanvas;
    }

    public void CreditsButton()
    {
        targetCanvas = creditsCanvas;
    }

    public void BackFromCredits()
    {
        targetCanvas = titleCanvas;
    }

    public void QuitGameButton()
    {
        Debug.Log("Quit Game");
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, targetCanvas, slideSpeed);
    }
}
