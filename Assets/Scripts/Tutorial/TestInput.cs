using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    private bool[] wasdPressed = new bool[4];

    public event Action OnAllKeysPressed;

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 movement = context.ReadValue<Vector2>();

            if (movement.y > 0 && !wasdPressed[0])
                wasdPressed[0] = true;

            if (movement.y < 0 && !wasdPressed[1])
                wasdPressed[1] = true;

            if (movement.x < 0 && !wasdPressed[2])
                wasdPressed[2] = true;

            if (movement.x > 0 && !wasdPressed[3])
                wasdPressed[3] = true;

            if (HaveAllKeysBeenPressed())
                OnAllKeysPressed?.Invoke();

        }
    }

    public bool HaveAllKeysBeenPressed()
    {
       return wasdPressed[0] && wasdPressed[1] && wasdPressed[2] && wasdPressed[3];
    }
}
