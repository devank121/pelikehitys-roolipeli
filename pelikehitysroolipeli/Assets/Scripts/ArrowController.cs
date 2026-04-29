using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    void OnCollisionEnter2D(Collision2D other)
    {
        // Don't react to the player
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }

        // Print what we hit and destroy the arrow
        Debug.Log("Nuoli osui: " + other.gameObject.name);
        Destroy(gameObject);
    }
}