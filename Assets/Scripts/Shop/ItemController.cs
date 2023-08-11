using UnityEngine;
using UnityEngine.InputSystem;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private MouseCursor mouseCursor;

    [Header("Input Action References"), Space(10)]
    [SerializeField] private InputActionReference chooseConeAction;

    [Header("Transforms"), Space(10)]
    [SerializeField] private Transform placeConeTransform;
    [SerializeField] private Transform trashTransform;

    private GameObject heldItem;
    private bool holdingItem;

    private void OnEnable()
    {
        chooseConeAction.action.Enable();
        chooseConeAction.action.performed += OnChooseCone;

        mouseCursor.OnMouseClicked += OnMouseClick;
        mouseCursor.OnMouseReleased += OnMouseRelease;
    }

    private void OnDisable()
    {
        chooseConeAction.action.Disable();
        chooseConeAction.action.performed -= OnChooseCone;


        mouseCursor.OnMouseClicked -= OnMouseClick;
        mouseCursor.OnMouseReleased -= OnMouseRelease;
    }

    private void Update()
    {
        if (holdingItem)
            DragItem();
    }

    private void OnChooseCone(InputAction.CallbackContext context)
    {
        CreateItemAtTransform(placeConeTransform);
    }

    private void OnMouseClick()
    {
        if (!holdingItem)
        {
            if (mouseCursor.IsCursorOverTransform(placeConeTransform))
                PickUpItem(placeConeTransform);
        }
    }

    private void OnMouseRelease()
    {
        if (holdingItem)
        {
            holdingItem = false;

            if (mouseCursor.IsCursorOverTransform(trashTransform))
                Destroy(heldItem);
            else
            {
                heldItem.transform.SetParent(placeConeTransform);
                heldItem.transform.position = placeConeTransform.position;
            }
        }
    }

    private void CreateItemAtTransform(Transform itemTransform)
    {
        if (itemPrefab != null && itemTransform != null)
        {
            GameObject item = Instantiate(itemPrefab, itemTransform.position, Quaternion.identity);
            item.transform.SetParent(itemTransform);
        }
    }

    private void PickUpItem(Transform pickUpTransform)
    {
        heldItem = pickUpTransform.GetChild(0).gameObject;
        heldItem.transform.SetParent(null);
        holdingItem = true;
    }

    private void DragItem()
    {
        if (holdingItem)
        {
            Vector3 mousePosition = mouseCursor.GetMousePosition();
            heldItem.transform.position = mousePosition;
        }
    }
}
