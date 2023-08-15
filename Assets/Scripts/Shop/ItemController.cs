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

    [Header("Sprites"), Space(10)]
    [SerializeField] private Sprite[] flavorSprites;
    [SerializeField] private Sprite[] sauceSprites;
    [SerializeField] private Sprite[] toppingSprites;

    private SpriteRenderer[] coneSpriteRenderers;

    private GameObject currentItem;
    private bool holdingItem;
    private bool chosenSauce;
    private bool chosenTopping;
    private bool canMoveOntoNextItem;

    private int maxFlavors = 3;
    private int currentFlavorCount = 0;

    public List<int> flavors = new List<int>();
    public List<int> sauces = new List<int>();
    public List<int> toppings = new List<int>();

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
        FindObjectOfType<AudioManager>().PlaySound("DropCone");
    }

    #region Flavors

    private void OnChooseFlavor(InputAction.CallbackContext context)
    {
        if (context.performed && (!chosenSauce || !chosenTopping))
        {
            int chosenFlavorIndex = GetChosenFlavorIndex();
            if (chosenFlavorIndex != -1)
            {
                if (chosenSauce || chosenTopping || currentFlavorCount == maxFlavors)
                    return;

                ChooseFlavor(chosenFlavorIndex);
                FindObjectOfType<AudioManager>().PlaySound("ScoopIceCream");

                if (currentFlavorCount >= 1)
                    canMoveOntoNextItem = true;
            }
        }
    }

    private void ChooseFlavor(int flavorIndex)
    {
        if (currentFlavorCount < maxFlavors)
        {
            flavors.Add(flavorIndex);

            if (flavorIndex >= 0 && flavorIndex < maxFlavors)
                coneSpriteRenderers[currentFlavorCount].sprite = flavorSprites[flavorIndex];

            currentFlavorCount++;
        }
    }

    private int GetChosenFlavorIndex()
    {
        if (currentFlavorCount < maxFlavors)
        {
            for (int i = 0; i < flavorSprites.Length; i++)
            {
                if (FlavorButtonPressed(i))
                    return i;
            }
        }
        return -1;
    }

    #endregion

    #region Sauces
    private void OnChooseSauce(InputAction.CallbackContext context)
    {
        if (context.performed && !chosenSauce && canMoveOntoNextItem)
        {
          int chosenSauceIndex = GetChosenSauceIndex();
            FindObjectOfType<AudioManager>().PlaySound("DropSyrup");
            if (chosenSauceIndex != -1)
            {
                ChooseSauce(chosenSauceIndex);
                chosenSauce = true;
            }
        }
    }

    private void ChooseSauce(int sauceIndex)
    {
        if (currentFlavorCount >= 1 && currentFlavorCount <= maxFlavors)
        {
            sauces.Add(sauceIndex);

            if (currentFlavorCount == 1)
                coneSpriteRenderers[3].sprite = sauceSprites[sauceIndex];
            else if (currentFlavorCount == 2)
                coneSpriteRenderers[4].sprite = sauceSprites[sauceIndex];
            else if (currentFlavorCount == 3)
                coneSpriteRenderers[5].sprite = sauceSprites[sauceIndex];
        }
    }

    private int GetChosenSauceIndex()
    {
        if (currentFlavorCount > 0 && currentFlavorCount <= maxFlavors)
        {
            for (int i = 0; i < sauceSprites.Length; i++)
            {
                if (SauceButtonPressed(i) && !sauces.Contains(i))
                    return i;
            }
        }
        return -1;
    }

    #endregion

    #region Toppings
    private void OnChooseTopping(InputAction.CallbackContext context)
    {
        if (context.performed && !chosenTopping && canMoveOntoNextItem)
        {
            int chosenToppingIndex = GetChosenToppingIndex();
            FindObjectOfType<AudioManager>().PlaySound("DropSprinkles");
            if (chosenToppingIndex != -1)
            {
                ChooseTopping(chosenToppingIndex);
                chosenTopping = true;
            }
        }
    }

    private void ChooseTopping(int toppingIndex)
    {
       if (currentFlavorCount >= 1 && currentFlavorCount <= maxFlavors)
        {
            toppings.Add(toppingIndex);

            if (currentFlavorCount == 1)
                coneSpriteRenderers[6].sprite = toppingSprites[toppingIndex];
            else if (currentFlavorCount == 2)
                coneSpriteRenderers[7].sprite = toppingSprites[toppingIndex];
            else if (currentFlavorCount == 3)
                coneSpriteRenderers[8].sprite = toppingSprites[toppingIndex];
        }
    }

    private int GetChosenToppingIndex()
    {
        if (currentFlavorCount > 0 && currentFlavorCount <= maxFlavors)
        {
            for (int i = 0; i < toppingSprites.Length; i++)
            {
                if (ToppingButtonPressed(i) && !toppings.Contains(i))
                    return i;
            }
        }
        return -1;
    }

    #endregion

    #region Mouse Input
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

    private void PickUpItem(Transform pickUpTransform)
    {
        if (pickUpTransform.childCount > 0)
        {
            currentItem = pickUpTransform.GetChild(0).gameObject;
            currentItem.transform.SetParent(null);
            holdingItem = true;
        }
    }

    private void DragItem()
    {
        if (holdingItem)
        {
            Vector3 mousePosition = mouseCursor.GetMousePosition();
            currentItem.transform.position = mousePosition;
        }
    }

    #endregion

    #region Button Input
    private bool FlavorButtonPressed(int flavorIndex) => (int)chooseFlavorAction.action.ReadValue<float>() == flavorIndex;

    private bool SauceButtonPressed(int sauceIndex) => (int)chooseSauceAction.action.ReadValue<float>() == sauceIndex;

    private bool ToppingButtonPressed(int toppingIndex) => (int)chooseToppingAction.action.ReadValue<float>() == toppingIndex;

    #endregion

    #region Item Handling
    private void CreateItemAtTransform(Transform itemTransform)
    {
        if (itemPrefab == null || itemTransform == null)
            return;
       
        if (itemTransform.childCount == 0)
        {
            GameObject item = Instantiate(itemPrefab, itemTransform.position, Quaternion.identity);
            item.transform.SetParent(itemTransform);

            InitializeSpriteRenderers(item);
            ResetAllValues();
        }
    }

    private void InitializeSpriteRenderers(GameObject item)
    {
        coneSpriteRenderers = new SpriteRenderer[9];

        for (int i = 0; i < 9; i++)
        {
            coneSpriteRenderers[i] = item.transform.GetChild(i + 1).GetComponent<SpriteRenderer>();
            coneSpriteRenderers[i].sprite = null;
        }
    }

    private void ResetAllValues()
    {
        chosenSauce = false;
        chosenTopping = false;

        flavors.Clear();
        sauces.Clear();
        toppings.Clear();

        currentFlavorCount = 0;
    }

    #endregion
}
