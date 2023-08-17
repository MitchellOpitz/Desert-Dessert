using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerControllerTest : MonoBehaviour
{
    [SerializeField] private CustomerSO[] possibleCustomers;
    [SerializeField] private Transform spawnPosition;
    private GameObject currentCustomerObject;
    private SpriteRenderer currentSpriteRenderer;
    private int currentSpriteIndex = 0;
    private int currentCustomerIndex = 0;

    [SerializeField] private float MinTime = 5f;
    [SerializeField] private float MaxTime = 8f;

    private float timer = 0f;
    private float randomTime;

    private void Start()
    {
        SpawnRandomCustomer();
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
        randomTime = Random.Range(MinTime, MaxTime);
    }

    private void SpawnNextCustomer()
    {
        Destroy(currentCustomerObject);
        SpawnRandomCustomer();
    }

    private Sprite[] GetSpritesForCurrentCustomer()
    {
        return possibleCustomers[currentCustomerIndex].sprites;
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
}
