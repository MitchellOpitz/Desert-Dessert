using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerControllerTest : MonoBehaviour
{
    [SerializeField] private ItemController itemController;

    [SerializeField] private CustomerSO[] possibleCustomers;
    [SerializeField] private Transform spawnPosition;
    private GameObject currentCustomerObject;
    private SpriteRenderer currentSpriteRenderer;
    private int currentSpriteIndex = 0;
    private int currentCustomerIndex = 0;

    [SerializeField] private GameObject orderPanel;
    [SerializeField] private GameObject timerIcon;
    [SerializeField] private GameObject cone;
    private SpriteRenderer[] coneSpriteRenderers;

    [Header("Sprites"), Space(10)]
    [SerializeField] private Sprite[] flavorSprites;
    [SerializeField] private Sprite[] sauceSprites;
    [SerializeField] private Sprite[] toppingSprites;

    [SerializeField] private float minTime = 20f;
    [SerializeField] private float maxTime = 30f;

    [SerializeField] private ScoreManager scoreManager;
    private float scoreMultiplier = 1.0f;

    private float timer = 0f;
    private float randomTime;

    private void Awake()
    {
        InitializeSpriteRenderers(cone);
    }

    private void Start()
    {
        SpawnRandomCustomer();
        orderPanel.gameObject.SetActive(true);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= randomTime)
            SpawnNextCustomer();
        else
        {
            scoreMultiplier = Mathf.Lerp(1.0f, 2.0f, 1.0f - timer / randomTime);
            SetNextSprite();
        }
    }

    private void SpawnRandomCustomer()
    {
        currentCustomerIndex = UnityEngine.Random.Range(0, possibleCustomers.Length);
        CustomerSO randomCustomer = possibleCustomers[currentCustomerIndex];
        currentCustomerObject = Instantiate(randomCustomer.prefab, spawnPosition.position, Quaternion.identity);
        currentSpriteIndex = 0;
        currentSpriteRenderer = currentCustomerObject.GetComponent<SpriteRenderer>();
        currentSpriteRenderer.sprite = randomCustomer.sprites[currentSpriteIndex];
        timer = 0f;
        randomTime = UnityEngine.Random.Range(minTime, maxTime);

        GenerateRandomOrder();
    }

    public void SpawnNextCustomer()
    {
        if (CheckIfOrderCorrect())
        {
            int scoreModifier = Mathf.RoundToInt(scoreMultiplier * 2);
            scoreManager.ChangeScore(scoreModifier);
        }

        Destroy(currentCustomerObject);
        SpawnRandomCustomer();
    }

    private void SetNextSprite()
    {
        float quarterTime = randomTime / 4;
        float halfTime = randomTime / 2;
        float threeQuarterTime = 3 * randomTime / 4;

        if (timer <= quarterTime)
            currentSpriteIndex = 0;
        else if (timer <= halfTime)
            currentSpriteIndex = 1;
        else if (timer <= threeQuarterTime)
            currentSpriteIndex = 2;
        else
            currentSpriteIndex = 3;

        currentSpriteRenderer.sprite = possibleCustomers[currentCustomerIndex].sprites[currentSpriteIndex];
    }

    private void GenerateRandomOrder()
    {
        InitializeSpriteRenderers(cone);

        int randomScoopCount = UnityEngine.Random.Range(1, 4);

        for (int i = 0; i < randomScoopCount; i++)
        {
            Sprite randomFlavor = flavorSprites[UnityEngine.Random.Range(0, flavorSprites.Length)];
            if (randomScoopCount == 1)
                coneSpriteRenderers[0].sprite = randomFlavor;
            else if (randomScoopCount == 2)
            {
                coneSpriteRenderers[0].sprite = flavorSprites[UnityEngine.Random.Range(0, flavorSprites.Length)];
                coneSpriteRenderers[1].sprite = flavorSprites[UnityEngine.Random.Range(0, flavorSprites.Length)];
            }
            else
            {
                coneSpriteRenderers[0].sprite = flavorSprites[UnityEngine.Random.Range(0, flavorSprites.Length)];
                coneSpriteRenderers[1].sprite = flavorSprites[UnityEngine.Random.Range(0, flavorSprites.Length)];
                coneSpriteRenderers[2].sprite = flavorSprites[UnityEngine.Random.Range(0, flavorSprites.Length)];
            }
        }

        bool addSauce = UnityEngine.Random.value > 0.5f;

        if (addSauce)
        {
            Sprite randomSauce = sauceSprites[UnityEngine.Random.Range(0, sauceSprites.Length)];
            if (randomScoopCount == 1)
                coneSpriteRenderers[3].sprite = randomSauce;
            else if (randomScoopCount == 2)
                coneSpriteRenderers[4].sprite = randomSauce;
            else
                coneSpriteRenderers[5].sprite = randomSauce;
        }

        bool addToppings = UnityEngine.Random.value > 0.5f;

        if (addToppings)
        {
            Sprite randomTopping = toppingSprites[UnityEngine.Random.Range(0, toppingSprites.Length)];
            if (randomScoopCount == 1)
                coneSpriteRenderers[6].sprite = randomTopping;
            else if (randomScoopCount == 2)
                coneSpriteRenderers[7].sprite = randomTopping;
            else
                coneSpriteRenderers[8].sprite = randomTopping;
        }
    }

    public void InitializeSpriteRenderers(GameObject item)
    {
        coneSpriteRenderers = new SpriteRenderer[9];

        for (int i = 0; i < 9; i++)
        {
            coneSpriteRenderers[i] = item.transform.GetChild(i + 1).GetComponent<SpriteRenderer>();
            coneSpriteRenderers[i].sprite = null;
        }
    }

    public bool CheckIfOrderCorrect()
    {
        List<int> customerFlavors = new List<int>();
        List<int> customerSauces = new List<int>();
        List<int> customerToppings = new List<int>();

        for (int i = 0; i < coneSpriteRenderers.Length; i++)
        {
            if (coneSpriteRenderers[i].sprite != null)
            {
                if (i < 3)
                    customerFlavors.Add(Array.IndexOf(flavorSprites, coneSpriteRenderers[i].sprite));
                else if (i >= 3 && i < 6)
                    customerSauces.Add(Array.IndexOf(sauceSprites, coneSpriteRenderers[i].sprite));
                else
                    customerToppings.Add(Array.IndexOf(toppingSprites, coneSpriteRenderers[i].sprite));
            }
        }

        bool flavorsMatch = Enumerable.SequenceEqual(customerFlavors.OrderBy(x => x), itemController.flavors.OrderBy(x => x));
        bool saucesMatch = Enumerable.SequenceEqual(customerSauces.OrderBy(x => x), itemController.sauces.OrderBy(x => x));
        bool toppingsMatch = Enumerable.SequenceEqual(customerToppings.OrderBy(x => x), itemController.toppings.OrderBy(x => x));

        return flavorsMatch && saucesMatch && toppingsMatch;
    }
}
