using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "ScriptableObjects/Customer", order = 1)]
public class CustomerSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite[] sprites;
}
