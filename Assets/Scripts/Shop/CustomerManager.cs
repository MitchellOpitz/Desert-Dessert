using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] GameObject CustomerPrefab;
    [SerializeField] Vector2 CustomerPosition;
    [SerializeField] float CustomerOffset;
    [SerializeField] int CustomerLimit;
    int CurrentCustomers;

    void Update() {
        while(CurrentCustomers <= CustomerLimit) {
            Instantiate(CustomerPrefab, new Vector3(CustomerPosition.x + (CurrentCustomers * CustomerOffset), CustomerPosition.y, 1), Quaternion.identity);
            CurrentCustomers += 1;
        }
    }
}
