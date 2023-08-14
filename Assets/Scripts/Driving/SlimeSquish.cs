using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSquish : MonoBehaviour
{
    [SerializeField] GameObject SquishedSlime;
    void OnCollisionEnter2D(Collision2D Hitbox) {
        FindObjectOfType<AudioManager>().PlaySound("RunOverJelly");
        transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        SquishedSlime.GetComponent<SpriteRenderer>().enabled = true;
        
        transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
