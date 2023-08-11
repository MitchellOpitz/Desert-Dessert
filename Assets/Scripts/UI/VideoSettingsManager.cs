using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VideoSettingsManager : MonoBehaviour
{
    [SerializeField]
    private bool isFullScreen;

    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private TMP_Text downloadFor1080pText;

    [SerializeField]
    private TMP_Text webGLResolutionText;

    private Resolution[] resolutions;
    private List<Resolution> availableResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    private void Start()
    {
        resolutions = Screen.resolutions;
        availableResolutions = new List<Resolution>();
        resolutionDropdown.ClearOptions();

        if (
            Application.platform == RuntimePlatform.WindowsPlayer
            || Application.platform == RuntimePlatform.WindowsEditor
        )
        {
            downloadFor1080pText.gameObject.SetActive(false);
            webGLResolutionText.gameObject.SetActive(false);
            SetWindowsResolutions();
        }
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            downloadFor1080pText.gameObject.SetActive(true);
            webGLResolutionText.gameObject.SetActive(true);
        }
    }

    private void SetWindowsResolutions()
    {
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                availableResolutions.Add(resolutions[i]);
            }
        }

        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < availableResolutions.Count; i++)
        {
            string resolutionsToDisplay =
                $"{availableResolutions[i].width} x {availableResolutions[i].height}";
            resolutionOptions.Add(resolutionsToDisplay);
            if (
                availableResolutions[i].width == Screen.width
                && availableResolutions[i].height == Screen.height
            )
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution selectedRes = resolutions[resolutionIndex];
        Screen.SetResolution(selectedRes.width, selectedRes.height, Screen.fullScreen);
    }
}
