using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ServiceKeybindOverlayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject keybindOverlayCanvas;

    [SerializeField]
    private PlayerControls _playerController;

    private bool showOverlay;

    private void Start()
    {
        _playerController = new PlayerControls();
        _playerController.Enable();
        showOverlay = false;
    }

    private void Update()
    {
        if (_playerController.UIButtonOverlay.ShowControlOverlay.ReadValue<float>() != 0)
        {
            showOverlay = true;
        }
        else
        {
            showOverlay = false;
        }

        if (showOverlay)
        {
            keybindOverlayCanvas.SetActive(true);
            Debug.Log("work plz");
        }
        else
        {
            keybindOverlayCanvas.SetActive(false);
        }
    }
}
