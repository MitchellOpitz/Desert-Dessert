using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] GameObject CustomerPrefab;
    [SerializeField] Transform CustomerPositions;

    private List<GameObject> ActiveCustomers = new List<GameObject>();

    void Start()
    {
        CreateInitialCustomers();
    }

    void Update()
    {
        UpdateCustomerPositions();
    }

    void CreateInitialCustomers()
    {
        GameObject Customer = Instantiate(CustomerPrefab, CustomerPositions.position, Quaternion.identity);
        ActiveCustomers[0] = Customer;
    }

    void UpdateCustomerPositions()
    {
        Debug.Log(ActiveCustomers[0].activeInHierarchy);
        if (ActiveCustomers[0] == null)
        {
            GameObject Customer = Instantiate(CustomerPrefab, CustomerPositions.position, Quaternion.identity);
            ActiveCustomers[0] = Customer;
        }
    }
}
