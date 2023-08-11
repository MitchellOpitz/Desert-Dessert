using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private MouseCursor mouseCursor;

    [Header("Input Action References"), Space(10)]
    [SerializeField] private InputActionReference chooseConeAction;
    [SerializeField] private InputActionReference chooseFlavorAction;
    [SerializeField] private InputActionReference chooseSauceAction;
    [SerializeField] private InputActionReference chooseToppingAction;

    [Header("Transforms"), Space(10)]
    [SerializeField] private Transform placeConeTransform;
    [SerializeField] private Transform trashTransform;

    private GameObject currentItem;
    private bool holdingItem;
    private bool chosenFlavor;

    private List<int> flavors = new List<int>();
    private List<int> sauces = new List<int>();
    private List<int> toppings = new List<int>();

    private void OnEnable()
    {
        chooseConeAction.action.Enable();
        chooseConeAction.action.performed += OnChooseCone;

        chooseFlavorAction.action.Enable();
        chooseFlavorAction.action.performed += OnChooseFlavor;

        chooseSauceAction.action.Enable();
        chooseSauceAction.action.performed += OnChooseSauce;

        chooseToppingAction.action.Enable();
        chooseToppingAction.action.performed += OnChooseTopping;

        mouseCursor.OnMouseClicked += OnMouseClick;
        mouseCursor.OnMouseReleased += OnMouseRelease;
    }

    private void OnDisable()
    {
        chooseConeAction.action.Disable();
        chooseConeAction.action.performed -= OnChooseCone;

        chooseFlavorAction.action.Disable();
        chooseFlavorAction.action.performed -= OnChooseFlavor;

        chooseSauceAction.action.Disable();
        chooseSauceAction.action.performed -= OnChooseSauce;

        chooseToppingAction.action.Disable();
        chooseToppingAction.action.performed -= OnChooseTopping;

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

    private void OnChooseFlavor(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (FlavorButtonPressed(0))
                ChooseFlavor(0);
            else if (FlavorButtonPressed(1))
                ChooseFlavor(1);
            else if (FlavorButtonPressed(2))
                ChooseFlavor(2);

            chosenFlavor = true;
        }
    }

    private void OnChooseSauce(InputAction.CallbackContext context)
    {
        if (context.performed && chosenFlavor)
        {
           if (SauceButtonPressed(0))
                ChooseSauce(0);
            else if (SauceButtonPressed(1))
                ChooseSauce(1);
            else if (SauceButtonPressed(2))
                ChooseSauce(2);
        }
    }

    private void OnChooseTopping (InputAction.CallbackContext context)
    {
        if (context.performed && chosenFlavor)
        {
            if (ToppingButtonPressed(0))
                ChooseTopping(0);
            else if (ToppingButtonPressed(1))
                ChooseTopping(1);
            else if (ToppingButtonPressed(2))
                ChooseTopping(2);
        }
    }

    private void ChooseFlavor(int flavorIndex)
    {
        flavors.Add(flavorIndex);
        Debug.Log("Flavor chosen: " + flavorIndex);
    }

    private void ChooseSauce(int sauceIndex)
    {
        sauces.Add(sauceIndex);
        Debug.Log("Sauce chosen: " + sauceIndex);
    }

    private void ChooseTopping(int toppingIndex)
    {
        toppings.Add(toppingIndex);
        Debug.Log("Topping chosen: " + toppingIndex);
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
                Destroy(currentItem);
            else
            {
                currentItem.transform.SetParent(placeConeTransform);
                currentItem.transform.position = placeConeTransform.position;
            }
        }
    }

    private bool FlavorButtonPressed(int flavorIndex) => (int)chooseFlavorAction.action.ReadValue<float>() == flavorIndex;

    private bool SauceButtonPressed(int sauceIndex) => (int)chooseSauceAction.action.ReadValue<float>() == sauceIndex;

    private bool ToppingButtonPressed(int toppingIndex) => (int)chooseToppingAction.action.ReadValue<float>() == toppingIndex;

    private void CreateItemAtTransform(Transform itemTransform)
    {
        if (itemPrefab != null && itemTransform != null)
        {
            if (itemTransform.childCount == 0)
            {
                GameObject item = Instantiate(itemPrefab, itemTransform.position, Quaternion.identity);
                item.transform.SetParent(itemTransform);
            }
        }
    }

    private void PickUpItem(Transform pickUpTransform)
    {
        currentItem = pickUpTransform.GetChild(0).gameObject;
        currentItem.transform.SetParent(null);
        holdingItem = true;
    }

    private void DragItem()
    {
        if (holdingItem)
        {
            Vector3 mousePosition = mouseCursor.GetMousePosition();
            currentItem.transform.position = mousePosition;
        }
    }
}
