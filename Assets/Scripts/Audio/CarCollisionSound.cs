using UnityEngine;

public class CarCollisionSound : MonoBehaviour
{
    public AudioSource crashSound; // Reference to the AudioSource for the crash sound

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            PlayCrashSound();
            FindObjectOfType<CameraBasicFollow>().UpdateDrivingSound();
        }
    }

    private void PlayCrashSound()
    {
        if (crashSound != null)
        {
            crashSound.Play();
        }
    }
}
