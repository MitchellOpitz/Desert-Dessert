using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBasicFollow : MonoBehaviour
{
    [SerializeField]
    private Transform Car;

    [SerializeField]
    private Transform LeadingTarget;

    [SerializeField]
    private Rigidbody2D carRB;

    private float speedLeadFactor;
    private Vector3 velocity = Vector3.zero;
    private Vector3 adjustedTarget;

    void LateUpdate()
    {
        speedLeadFactor = carRB.velocity.magnitude / 30;
        if (speedLeadFactor < 0.3)
        {
            speedLeadFactor = 0;
        }
        speedLeadFactor -= 0.3f;
        Debug.Log(speedLeadFactor);

        adjustedTarget = Vector3.Lerp(Car.position, LeadingTarget.position, speedLeadFactor);

        transform.position = adjustedTarget + new Vector3(0, 0, -10);
        transform.rotation = Car.rotation;
    }
}
