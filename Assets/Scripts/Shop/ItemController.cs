using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform placeItemTransform;
    [SerializeField] private MouseCursor mouseCursor;

    [SerializeField] private InputActionReference chooseConeAction;

    private void OnEnable()
    {
        chooseConeAction.action.Enable();
        chooseConeAction.action.performed += OnChooseCone;
    }

    private void OnDisable()
    {
        chooseConeAction.action.Disable();
        chooseConeAction.action.performed -= OnChooseCone;
    }

    private void OnChooseCone(InputAction.CallbackContext context)
    {
        CreateAndPlaceItem();
    }

    private void CreateAndPlaceItem()
    {
        if (itemPrefab != null && placeItemTransform != null)
        {
            GameObject item = Instantiate(itemPrefab, placeItemTransform.position, Quaternion.identity);
            item.transform.SetParent(placeItemTransform);
        }
    }
}
