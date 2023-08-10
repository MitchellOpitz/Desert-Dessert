using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject quitConfirmScreen;

    private void Start()
    {
        quitConfirmScreen.SetActive(false);
    }

    public void QuitGameButton()
    {
        quitConfirmScreen.SetActive(true);
    }

    public void ConfirmQuitYes()
    {
        Application.Quit();
    }

    public void ConfirmQuitNo()
    {
        quitConfirmScreen.SetActive(false);
    }
}
