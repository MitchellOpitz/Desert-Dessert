using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] GameObject CustomerPrefab;
    [SerializeField] Transform[] CustomerPositions;

    private List<Transform> ActiveCustomers = new List<Transform>();
    private int CurrentPositionIndex = 0;

    private void Start()
    {
        SpawnInitialCustomers();
    }

    private void SpawnInitialCustomers()
    {
        foreach (Transform CustomerTransform in CustomerPositions)
        {
            InstantiateCustomer(CustomerTransform);
        }
    }

    private void InstantiateCustomer(Transform SpawnTransform)
    {
        GameObject NewCustomer = Instantiate(CustomerPrefab, SpawnTransform.position, SpawnTransform.rotation);
        ActiveCustomers.Add(NewCustomer.transform);
    }

    public void ReplaceCustomer(GameObject CustomerObject)
    {
        ActiveCustomers.Remove(CustomerObject.transform);
        StartCoroutine(SwitchCustomers(CustomerObject));
    }

    private IEnumerator SwitchCustomers(GameObject OutgoingCustomer)
    {
        //Animator OutgoingAnimator = OutgoingCustomer.GetComponent<Animator>();
        //OutgoingAnimator.SetTrigger("WalkOut");

        yield return new WaitForSeconds(1.0f); // Adjust the duration based on your animation

        Destroy(OutgoingCustomer);

        Transform NextSpawnTransform = CustomerPositions[CurrentPositionIndex];
        GameObject NewCustomer = Instantiate(CustomerPrefab, NextSpawnTransform.position, NextSpawnTransform.rotation);
        ActiveCustomers.Add(NewCustomer.transform);

        //Animator NewAnimator = NewCustomer.GetComponent<Animator>();
        //NewAnimator.SetTrigger("WalkIn");

        CurrentPositionIndex = (CurrentPositionIndex + 1) % CustomerPositions.Length;
    }
}