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
    private List<int> needFlavours = new List<int>();
    private List<int> needSauces = new List<int>();
    private List<int> needToppings = new List<int>();

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
            int RandomFlavour = Random.Range(0, IceCreamFlavours.Length);
            GeneratedIceCream.Add(IceCreamFlavours[RandomFlavour]);

            // Add the index of the flavor to the needFlavours list
            needFlavours.Add(RandomFlavour);
        }

        bool AddSauce = Random.value > 0.5f;
        if (AddSauce)
        {
            int RandomSauce = Random.Range(0, IceCreamSauces.Length);
            GeneratedIceCream.Add(IceCreamSauces[RandomSauce]);

            // Add the index of the sauce to the needSauces list
            needSauces.Add(IceCreamFlavours.Length + RandomSauce);
        }

        bool AddToppings = Random.value > 0.5f;
        if (AddToppings)
        {
            int RandomToppings = Random.Range(0, IceCreamToppings.Length);
            GeneratedIceCream.Add(IceCreamToppings[RandomToppings]);

            // Add the index of the topping to the needToppings list
            needToppings.Add(IceCreamFlavours.Length + IceCreamSauces.Length + RandomToppings);
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

    void OnCollisionEnter2D(Collision2D Hitbox)
    {
        Debug.Log("Test");
        if (Hitbox.gameObject.CompareTag("IceCream"))
        {
            // Check the ice cream here
            List<int> receivedFlavors = FindObjectOfType<ItemController>().flavors;
            List<int> receivedSauces = FindObjectOfType<ItemController>().sauces;
            List<int> receivedToppings = FindObjectOfType<ItemController>().toppings;

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
        Debug.Log(flavors[0]);
        Debug.Log(flavors[1]);
        Debug.Log(flavors[2]);
        Debug.Log(needFlavours[0]);
        Debug.Log(needFlavours[1]);
        Debug.Log(needFlavours[2]);
        Debug.Log(sauces[0]);
        Debug.Log(needSauces[0]);
        Debug.Log(toppings[0]);
        Debug.Log(needToppings[0]);
        if (flavors[0] == needFlavours[0] && flavors[1] == needFlavours[1] && flavors[2] == needFlavours[2] && sauces[0] == needSauces[0] && toppings[0] == needToppings[0])
        {
            return true;
        }
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
