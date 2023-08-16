using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] GameObject CustomerPrefab;
    [SerializeField] Transform[] CustomerPositions;

    private List<Transform> ActiveCustomers = new List<Transform>();

    public void ReplaceCustomer(GameObject CustomerObject) {
        ActiveCustomers.Remove(CustomerObject.transform);
        Destroy(CustomerObject);

        foreach(Transform CustomerTransform in CustomerPositions) {
            if(ActiveCustomers.Contains(CustomerTransform)) {
                continue;
            }
            Instantiate(CustomerPrefab, CustomerTransform.position, CustomerTransform.rotation);
            ActiveCustomers.Add(CustomerTransform);
        }
    }
}
