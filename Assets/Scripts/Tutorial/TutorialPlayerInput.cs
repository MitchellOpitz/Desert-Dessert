using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialPlayerInput : MonoBehaviour
{
    //private bool[] wasdPressed = new bool[4];

    private bool wasKeyPressed;
    private bool wasMouseButtonClicked;

    public event Action OnKeyPressed;
    public event Action OnMouseButtonClicked;

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 movement = context.ReadValue<Vector2>();

            wasKeyPressed = true;

            /*
            wasdPressed[0] = movement.y > 0;
            wasdPressed[1] = movement.y < 0;
            wasdPressed[2] = movement.x < 0;
            wasdPressed[3] = movement.x > 0;

            if (wasdPressed[0])
                Debug.Log("W pressed");
            if (wasdPressed[1])
                Debug.Log("S pressed");
            if (wasdPressed[2])
                Debug.Log("A pressed");
            if (wasdPressed[3])
                Debug.Log("D pressed");
            */

            if (KeyPressed())
                OnKeyPressed?.Invoke();
        }
    }

    public bool KeyPressed() => wasKeyPressed;

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

