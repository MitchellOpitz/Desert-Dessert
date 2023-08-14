using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound("IceCreamScene");
    }
}
