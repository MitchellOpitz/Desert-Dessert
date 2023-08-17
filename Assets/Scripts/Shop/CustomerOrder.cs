using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    [SerializeField] GameObject[] IceCreamFlavours;
    [SerializeField] GameObject[] IceCreamSauces;
    [SerializeField] GameObject[] IceCreamToppings;

    [SerializeField] List<int> GeneratedFlavours = new List<int>();
    [SerializeField] List<int> GeneratedSauces = new List<int>();
    [SerializeField] List<int> GeneratedToppings = new List<int>();


    bool CheckIceCreamCombination(List<int> Flavors, List<int> Sauces, List<int> Toppings)
    {
        if (Flavors.Count != GeneratedFlavours.Count || Sauces.Count != GeneratedSauces.Count || Toppings.Count != GeneratedToppings.Count)
        {
            Debug.Log(IceCreamFlavours[Flavors[0]].name);
            Debug.Log(IceCreamToppings[Toppings[0]].name);
            Debug.Log(IceCreamSauces[Sauces[0]].name);
            return false;
        }

        for (int i = 0; i < Flavors.Count; i++)
        {
            Debug.Log("Recieved Flavor: " + IceCreamFlavours[Flavors[i]].name);
            Debug.Log("Generated Flavor: " + IceCreamFlavours[GeneratedFlavours[i]].name);
            if (Flavors[i] != GeneratedFlavours[i])
            {
                return false;
            }
        }

        for (int i = 0; i < Sauces.Count; i++)
        {
            Debug.Log("Recieved Sauce: " + IceCreamSauces[Sauces[i]].name);
            Debug.Log("Generated Sauce: " + IceCreamSauces[GeneratedSauces[i]].name);
            if (Sauces[i] != GeneratedSauces[i])
            {
                return false;
            }
        }

        for (int i = 0; i < Toppings.Count; i++)
        {
            Debug.Log("Recieved Toppings: " + IceCreamToppings[Toppings[i]].name);
            Debug.Log("Generated Toppings: " + IceCreamToppings[GeneratedToppings[i]].name);
            if (Toppings[i] != GeneratedToppings[i])
            {
                return false;
            }
        }

        return true;
    }
}
