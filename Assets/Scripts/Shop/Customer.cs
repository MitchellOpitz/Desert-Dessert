using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject[] SlimeBreeds;

    [Space]
    [SerializeField] GameObject[] IceCreamFlavours;
    [SerializeField] GameObject[] IceCreamSauces;
    [SerializeField] GameObject[] IceCreamToppings;

    [Space(10)]
    [Header("Dialogue")]
    [SerializeField] Vector2 SlimePosition;
    [SerializeField] Vector2 ConePosition;
    [SerializeField] GameObject ConeObject;
    [SerializeField] float ObjectOffset;
    [SerializeField] Transform TimerTransform;
    float RandomTimeDuration;

    List<GameObject> GeneratedIceCream = new List<GameObject>();

    void GenerateSlime()
    {
        int RandomSlime = Random.Range(0, SlimeBreeds.Length - 1);
        Instantiate(SlimeBreeds[RandomSlime], SlimePosition, Quaternion.identity, transform);
    }

    void GenerateIceCream()
    {
        int RandomScoopCount = Random.Range(1, 3);
        Vector2 CurrentScoopPosition = ConePosition;

        for (int i = 0; i < RandomScoopCount; i++)
        {
            int RandomFlavour = Random.Range(0, IceCreamFlavours.Length - 1);
            GeneratedIceCream.Add(IceCreamFlavours[RandomFlavour]);
        }

        bool AddSauce = Random.value > 0.5f;
        if (AddSauce)
        {
            int RandomSauce = Random.Range(0, IceCreamSauces.Length - 1);
            GeneratedIceCream.Add(IceCreamSauces[RandomSauce]);
        }

        bool AddToppings = Random.value > 0.5f;
        if (AddToppings)
        {
            int RandomToppings = Random.Range(0, IceCreamToppings.Length - 1);
            GeneratedIceCream.Add(IceCreamToppings[RandomToppings]);
        }
    }

    void DisplayIceCream()
    {
        Vector2 CurrentPosition = ConePosition;

        foreach (GameObject Object in GeneratedIceCream)
        {
            GameObject NewIceCreamPart = Instantiate(Object, CurrentPosition, Quaternion.identity, ConeObject.transform);
            CurrentPosition.y += ObjectOffset;
        }
    }

    void GenerateTimer()
    {
        RandomTimeDuration = Random.Range(3f, 5f);
    }

    void UpdateTimer()
    {
        TimerTransform.rotation = Quaternion.Euler(TimerTransform.rotation.eulerAngles + (Vector3.right * 360 / RandomTimeDuration * Time.deltaTime));
    }

    void OnEnterCollision2D(Collision2D Hitbox)
    {
        if (Hitbox.gameObject.CompareTag("IceCream"))
        {
            // Check the ice cream here
            List<int> receivedFlavors = Hitbox.gameObject.GetComponent<ItemController>().flavors;
            List<int> receivedSauces = Hitbox.gameObject.GetComponent<ItemController>().sauces;
            List<int> receivedToppings = Hitbox.gameObject.GetComponent<ItemController>().toppings;

            bool isCorrect = CheckIceCreamCombination(receivedFlavors, receivedSauces, receivedToppings);

            if (isCorrect)
            {
                Debug.Log("Ice cream is correct!");
            }
            else
            {
                Debug.Log("Ice cream is incorrect.");
            }
        }
    }

    bool CheckIceCreamCombination(List<int> flavors, List<int> sauces, List<int> toppings)
    {
        // Implement your logic to check the ice cream combination
        // Return true if the combination is correct, false otherwise
        return false; // Placeholder return value
    }

    void Update()
    {
        UpdateTimer();
    }

    void Start()
    {
        GenerateSlime();
        GenerateIceCream();
        DisplayIceCream();
        GenerateTimer();
    }
}
