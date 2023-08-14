using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ServiceKeybindOverlayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject keybindOverlayCanvas;

    [SerializeField]
    private InputActionReference showButtonOverlay;

    private void Start()
    {
        keybindOverlayCanvas.SetActive(false);
    }

    public void ShowKeybindWindow() { }
}
