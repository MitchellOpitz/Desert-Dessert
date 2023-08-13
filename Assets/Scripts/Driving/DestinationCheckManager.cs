using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinationCheckManager : MonoBehaviour
{
    [SerializeField]
    private GameObject destinationScreen;

    [SerializeField]
    private GameObject player;

    private Rigidbody2D playerRB;

    public bool playerReachedDestination { get; private set;}

    private void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "finishLine")
        {
            PlayerReachedDestination();
        }
        Debug.Log(other.gameObject.tag);
    }

    private void PlayerReachedDestination()
    {
        destinationScreen.SetActive(true);
        playerRB.bodyType = RigidbodyType2D.Static;
        playerReachedDestination = true;
    }

    public void ConfirmServiceStartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playerRB.bodyType = RigidbodyType2D.Dynamic;
    }
}
