using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float speed = 2f;
    public int health = 30;
    private Transform player;

    void Start()
    {
        // Find the player in the scene
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Move slowly toward the player
        Vector2 suunta = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void OtaVahinko(int maara)
    {
        health -= maara;
        Debug.Log("Ghost health: " + health);

        if (health <= 0)
        {
            Debug.Log("Ghost kuoli!");
            Destroy(gameObject);
        }
    }
}