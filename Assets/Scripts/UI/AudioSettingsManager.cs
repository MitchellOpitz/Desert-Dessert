using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider sfxSlider;

    [SerializeField]
    private TextMeshProUGUI musicPercentageText;

    [SerializeField]
    private TextMeshProUGUI sfxPercentageText;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        //        audioMixer.SetFloat("music", volume);
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        //        audioMixer.SetFloat("sfx", volume);
    }

    public void updateMusicPercentageText(float volume)
    {
        musicPercentageText.text = Mathf.RoundToInt(volume * 100) + "%";
    }

    public void updateSfxPercentageText(float volume)
    {
        sfxPercentageText.text = Mathf.RoundToInt(volume * 100) + "%";
    }
}
