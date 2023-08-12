using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationSlimeManager : MonoBehaviour
{
    [SerializeField]
    private Transform leftPoint;

    [SerializeField]
    private Transform rightPoint;

    [SerializeField]
    private GameObject slimeBoi;

    [Header("Keep below 1, or they risk escape  :(")]
    [SerializeField]
    private float speed;

    private bool movingRight;

    void Awake()
    {
        movingRight = true;
    }

    void FixedUpdate()
    {
        if (movingRight)
        {
            slimeBoi.transform.localPosition += new Vector3(speed, 0, 0);
        }
        else
        {
            slimeBoi.transform.localPosition += new Vector3(-speed, 0, 0);
        }

        if (Vector3.Distance(slimeBoi.transform.position, rightPoint.transform.position) < 1.0)
        {
            movingRight = false;
        }
        if (Vector3.Distance(slimeBoi.transform.position, leftPoint.transform.position) < 1.0)
        {
            movingRight = true;
        }
    }
}
