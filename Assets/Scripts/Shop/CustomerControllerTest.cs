using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private float minTime = 5f;
    [SerializeField] private float maxTime = 8f;

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
        {
            timer = 0f;
            SpawnNextCustomer();
        }
        else
        {
            SetNextSprite();
        }
    }

    private void SpawnRandomCustomer()
    {
        currentCustomerIndex = Random.Range(0, possibleCustomers.Length);
        CustomerSO randomCustomer = possibleCustomers[currentCustomerIndex];
        currentCustomerObject = Instantiate(randomCustomer.prefab, spawnPosition.position, Quaternion.identity);
        currentSpriteIndex = 0;
        currentSpriteRenderer = currentCustomerObject.GetComponent<SpriteRenderer>();
        currentSpriteRenderer.sprite = randomCustomer.sprites[currentSpriteIndex];
        randomTime = Random.Range(minTime, maxTime);

        GenerateRandomOrder();
    }

    private void SpawnNextCustomer()
    {
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

        int randomScoopCount = Random.Range(1, 4);

        for (int i = 0; i < randomScoopCount; i++)
        {
            Sprite randomFlavor = flavorSprites[Random.Range(0, flavorSprites.Length)];
            if (randomScoopCount == 1)
                coneSpriteRenderers[0].sprite = randomFlavor;
            else if (randomScoopCount == 2)
            {
                coneSpriteRenderers[0].sprite = flavorSprites[Random.Range(0, flavorSprites.Length)];
                coneSpriteRenderers[1].sprite = flavorSprites[Random.Range(0, flavorSprites.Length)];
            }
            else
            {
                coneSpriteRenderers[0].sprite = flavorSprites[Random.Range(0, flavorSprites.Length)];
                coneSpriteRenderers[1].sprite = flavorSprites[Random.Range(0, flavorSprites.Length)];
                coneSpriteRenderers[2].sprite = flavorSprites[Random.Range(0, flavorSprites.Length)];
            }
        }

        bool addSauce = Random.value > 0.5f;

        if (addSauce)
        {
            Sprite randomSauce = sauceSprites[Random.Range(0, sauceSprites.Length)];
            if (randomScoopCount == 1)
                coneSpriteRenderers[3].sprite = randomSauce;
            else if (randomScoopCount == 2)
                coneSpriteRenderers[4].sprite = randomSauce;
            else
                coneSpriteRenderers[5].sprite = randomSauce;
        }

        bool addToppings = Random.value > 0.5f;

        if (addToppings)
        {
            Sprite randomTopping = toppingSprites[Random.Range(0, toppingSprites.Length)];
            if (randomScoopCount == 1)
                coneSpriteRenderers[6].sprite = randomTopping;
            else if (randomScoopCount == 2)
                coneSpriteRenderers[7].sprite = randomTopping;
            else
                coneSpriteRenderers[8].sprite = randomTopping;
        }

        Debug.Log(randomScoopCount);
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
}
