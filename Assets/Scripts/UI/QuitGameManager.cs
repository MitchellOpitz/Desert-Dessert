using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject quitConfirmScreen;

    [SerializeField]
    GameObject webBuildCatchScreen;

    private void Start()
    {
        quitConfirmScreen.SetActive(false);
        webBuildCatchScreen.SetActive(false);
    }

    public void QuitGameButton()
    {
        quitConfirmScreen.SetActive(true);
    }

    public void ConfirmQuitYes()
    {
        if (
            Application.platform == RuntimePlatform.WebGLPlayer
            || Application.platform == RuntimePlatform.WindowsEditor
        )
        {
            webBuildCatchScreen.SetActive(true);
        }
        else
        {
            Application.Quit();
        }
    }

    public void WebBuildConfirmButton()
    {
        webBuildCatchScreen.SetActive(false);
        quitConfirmScreen.SetActive(false);
    }

    public void ConfirmQuitNo()
    {
        quitConfirmScreen.SetActive(false);
    }
}
