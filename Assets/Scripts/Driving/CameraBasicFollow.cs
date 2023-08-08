using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBasicFollow : MonoBehaviour
{
    [SerializeField] Transform Target;
    void LateUpdate() {
        transform.position = Target.position + new Vector3(0, 0, -10);
        transform.rotation = Target.rotation;
    }
}
