using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFollow : MonoBehaviour
{
    [SerializeField] Transform Target;
    
    void LateUpdate() {
        transform.position = new Vector3(transform.position.x, Target.position.y, transform.position.z);
    }
}
