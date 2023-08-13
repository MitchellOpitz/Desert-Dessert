using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialPlayerInput : MonoBehaviour
{
    private bool[] wasdPressed = new bool[4];

    private bool wasMouseButtonClicked;

    public event Action OnAllKeysPressed;
    public event Action OnMouseButtonClicked;

    /*public void OnMovement(InputAction.CallbackContext context)
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

            if (AllKeysPressed())
                OnAllKeysPressed?.Invoke();
        }
    }

    public bool AllKeysPressed() => wasdPressed[0] && wasdPressed[1] && wasdPressed[2] && wasdPressed[3];*/

    public void OnServe(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            wasMouseButtonClicked = true;

            if (MouseButtonClicked())
                OnMouseButtonClicked?.Invoke();
        }
    }

    public bool MouseButtonClicked() => wasMouseButtonClicked;
}

