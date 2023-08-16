using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Jobs;

public class Customer : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject[] SlimeBreeds;
    GameObject CurrentSlime;

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

    [Space]
    [SerializeField] float MinTime;
    [SerializeField] float MaxTime;

    float TotalTime;
    float RandomTimeDuration;
    float Interval = 1f;

    [SerializeField] GameObject ScoreManager;

    [Space(10)]
    [SerializeField] List<int> GeneratedFlavours = new List<int>();
    [SerializeField] List<int> GeneratedSauces = new List<int>();
    [SerializeField] List<int> GeneratedToppings = new List<int>();

    List<GameObject> GeneratedIceCream = new List<GameObject>();

    void GenerateSlime()
    {
        int RandomSlime = Random.Range(0, SlimeBreeds.Length - 1);
        CurrentSlime = Instantiate(SlimeBreeds[RandomSlime], new Vector3(SlimePosition.x, SlimePosition.y, 1f), Quaternion.identity, transform);
    }

    void GenerateIceCream()
    {
        int RandomScoopCount = Random.Range(1, 3);
        Vector2 CurrentScoopPosition = ConePosition;

        for (int i = 0; i < RandomScoopCount; i++)
        {
            int RandomFlavour = Random.Range(0, IceCreamFlavours.Length - 1);
            GeneratedFlavours.Add(RandomFlavour);
            GeneratedIceCream.Add(IceCreamFlavours[RandomFlavour]);
        }

        bool AddSauce = Random.value > 0.5f;
        if (AddSauce)
        {
            int RandomSauce = Random.Range(0, IceCreamSauces.Length - 1);
            GeneratedSauces.Add(RandomSauce);
            GeneratedIceCream.Add(IceCreamSauces[RandomSauce]);
        }

        bool AddToppings = Random.value > 0.5f;
        if (AddToppings)
        {
            int RandomToppings = Random.Range(0, IceCreamToppings.Length - 1);
            GeneratedToppings.Add(RandomToppings);
            GeneratedIceCream.Add(IceCreamToppings[RandomToppings]);
        }
    }

    void DisplayIceCream()
    {
        Vector2 CurrentPosition = ConePosition;
        int SortingOrder = 0; // Start sorting order

        foreach (GameObject Object in GeneratedIceCream)
        {
            GameObject NewIceCreamPart = Instantiate(Object, CurrentPosition, Quaternion.identity, ConeObject.transform);
            SpriteRenderer Renderer = NewIceCreamPart.GetComponent<SpriteRenderer>();

            if (Renderer != null)
            {
                Renderer.sortingOrder = SortingOrder;
                SortingOrder++;
            }

            CurrentPosition.y += ObjectOffset;
        }
    }


    void GenerateTimer()
    {
        RandomTimeDuration = Random.Range(MinTime, MaxTime);
        TotalTime = Time.time + RandomTimeDuration;
    }

    void UpdateTimer()
    {
        if(Time.time >= TotalTime) {
            FindObjectOfType<CustomerManager>().ReplaceCustomer(transform.gameObject);
        }

        TimerTransform.rotation = Quaternion.Euler(TimerTransform.rotation.eulerAngles + (Vector3.forward * 180 / RandomTimeDuration * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D Hitbox)
    {
        if (Hitbox.gameObject.CompareTag("IceCream"))
        {
            // Check the ice cream here
            List<int> ReceivedFlavors = FindObjectOfType<ItemController>().flavors;
            List<int> ReceivedSauces = FindObjectOfType<ItemController>().sauces;
            List<int> ReceivedToppings = FindObjectOfType<ItemController>().toppings;

            bool IsCorrect = CheckIceCreamCombination(ReceivedFlavors, ReceivedSauces, ReceivedToppings);

            if (IsCorrect)
            {
                Destroy(Hitbox.gameObject);
                FindObjectOfType<CustomerManager>().ReplaceCustomer(transform.gameObject);
            }
            else
            {
                return;
            }
        }
    }

    bool CheckIceCreamCombination(List<int> Flavors, List<int> Sauces, List<int> Toppings)
    {
        if(Flavors.Count != GeneratedFlavours.Count || Sauces.Count != GeneratedSauces.Count ||  Toppings.Count != GeneratedToppings.Count) {
            Debug.Log(IceCreamFlavours[Flavors[0]].name);
            Debug.Log(IceCreamToppings[Toppings[0]].name);
            Debug.Log(IceCreamSauces[Sauces[0]].name);
            return false;
        }

        for(int i = 0; i < Flavors.Count; i ++) {
            Debug.Log("Recieved Flavor: " + IceCreamFlavours[Flavors[i]].name);
            Debug.Log("Generated Flavor: " + IceCreamFlavours[GeneratedFlavours[i]].name);
            if(Flavors[i] != GeneratedFlavours[i]) {
                return false;
            }
        }

        for(int i = 0; i < Sauces.Count; i ++) {
            Debug.Log("Recieved Sauce: " + IceCreamSauces[Sauces[i]].name);
            Debug.Log("Generated Sauce: " + IceCreamSauces[GeneratedSauces[i]].name);
            if(Sauces[i] != GeneratedSauces[i]) {
                return false;
            }
        }

        for(int i = 0; i < Toppings.Count; i ++) {
            Debug.Log("Recieved Toppings: " + IceCreamToppings[Toppings[i]].name);
            Debug.Log("Generated Toppings: " + IceCreamToppings[GeneratedToppings[i]].name);
            if(Toppings[i] != GeneratedToppings[i]) {
                return false;
            }
        }

        return true;
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
