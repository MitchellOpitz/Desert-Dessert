using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEndZone : MonoBehaviour
{
    public bool PlayerFinished;

    void OnCollisionEnter2D(Collision2D Hitbox) {
        if(Hitbox.gameObject.tag == "playerCar") {
            PlayerFinished = true;
        }
    }
}
