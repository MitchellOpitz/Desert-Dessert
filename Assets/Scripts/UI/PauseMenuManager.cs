using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject pauseScreenOverlay;

    [SerializeField]
    GameObject pauseScreen;

    [SerializeField]
    GameObject settingsMenu;

    [SerializeField]
    GameObject quitConfirmPopup;

    [SerializeField]
    GameObject scooperCheck;

    private void Start()
    {
        //Ensuring pause menu isn't left active for scene start
        pauseScreenOverlay.SetActive(false);
        settingsMenu.SetActive(false);
        quitConfirmPopup.SetActive(false);
    }

    public void OpenPauseGameMenu()
    {
        if (scooperCheck != null)
        {
            scooperCheck.SetActive(false);
        }
        pauseScreenOverlay.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumePausedGame()
    {
        if (scooperCheck != null)
        {
            scooperCheck.SetActive(true);
            Cursor.visible = false;
        }
        pauseScreenOverlay.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseSettingsMenu()
    {
        pauseScreen.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void PauseBackFromSettings()
    {
        pauseScreen.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void PauseQuitToMenu()
    {
        quitConfirmPopup.SetActive(true);
    }

    public void PauseQuitToMenuConfirmYes()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PauseQuitToMenuConfirmNo()
    {
        quitConfirmPopup.SetActive(false);
    }
}
