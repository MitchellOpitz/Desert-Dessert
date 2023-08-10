using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCursor : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 mousePosition;

    [SerializeField] private InputActionReference mousePositionAction;
    [SerializeField] private InputActionReference mouseClickAction;

    [SerializeField] private List<Transform> hoverTiltTransformsList;

    private bool hoveringOverIceCream;

    private void Awake()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        mousePositionAction.action.Enable();
        mousePositionAction.action.performed += OnMousePosition;

        mouseClickAction.action.Enable();
        mouseClickAction.action.performed += OnMouseClick; 
        mouseClickAction.action.canceled += OnMouseRelease;
    }

    private void OnDisable()
    {
        mousePositionAction.action.Disable();
        mousePositionAction.action.performed -= OnMousePosition;

        mouseClickAction.action.Disable();
        mouseClickAction.action.performed += OnMouseClick;
        mouseClickAction.action.canceled += OnMouseRelease;
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();

        HandleMousePosition();
    }

    private void HandleMousePosition()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(mousePosition);
        mousePos.z = 0f;

        transform.position = mousePos;

        foreach (Transform hoverTransform in hoverTiltTransformsList)
        {
            float distance = Vector3.Distance(hoverTransform.position, mousePos);
            if (distance < 1f)
            {
                hoveringOverIceCream = true;
                transform.rotation = Quaternion.Euler(0f, 0f, 30f);
                break;
            }

            if (hoveringOverIceCream)
            {
                hoveringOverIceCream = false;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    public void OnMouseClick(InputAction.CallbackContext context)
    {
        Debug.Log("Mouse Clicked");
    }

    public void OnMouseRelease(InputAction.CallbackContext context)
    {
        Debug.Log("Mouse Released");
    }
}
