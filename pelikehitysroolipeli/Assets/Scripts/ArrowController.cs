using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Nuoli osui: " + other.gameObject.name + " tag: " + other.gameObject.tag);

        // Check if we hit a ghost
        GhostController ghost = other.gameObject.GetComponent<GhostController>();
        if (ghost != null)
        {
            Debug.Log("Osui ghostiin! Vahinko: " + damage);
            ghost.OtaVahinko(damage);
        }

        // Check if we hit a crab
        CrabController crab = other.gameObject.GetComponent<CrabController>();
        if (crab != null)
        {
            Debug.Log("Osui rapuun! Vahinko: " + damage);
            crab.OtaVahinko(damage);
        }

        Destroy(gameObject);
    }
}