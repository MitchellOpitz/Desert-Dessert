using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialPlayerInput : MonoBehaviour
{
    [SerializeField] GameObject tutorialManagerObject;

    private bool[] wasdPressed = new bool[4];
    [SerializeField] GameObject[] wasdKeys;

    private bool wasMouseButtonClicked;

    public event Action OnAllKeysPressed;
    public event Action OnMouseButtonClicked;

    void Start() {
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed && tutorialManagerObject.GetComponent<TutorialManager>().hasDrivingControlPopupBeenDisplayed)
        {
            Vector2 movement = context.ReadValue<Vector2>();
            Debug.Log(movement);

            if (movement.y > 0 && !wasdPressed[0] && wasdKeys[0].activeInHierarchy)
                wasdPressed[0] = true;
                wasdKeys[0].SetActive(false);

            if (movement.y < 0 && !wasdPressed[1] && wasdKeys[1].activeInHierarchy)
                wasdPressed[1] = true;
                wasdKeys[1].SetActive(false);

            if (movement.x < 0 && !wasdPressed[2] && wasdKeys[2].activeInHierarchy)
                wasdPressed[2] = true;
                wasdKeys[2].SetActive(false);

            if (movement.x > 0 && !wasdPressed[3] && wasdKeys[3].activeInHierarchy)
                wasdPressed[3] = true;
                wasdKeys[3].SetActive(false);

            if (AllKeysPressed())
                OnAllKeysPressed?.Invoke();
        }
    }

    public bool AllKeysPressed() => wasdPressed[0] && wasdPressed[1] && wasdPressed[2] && wasdPressed[3] && wasdPressed[0] == true;

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

