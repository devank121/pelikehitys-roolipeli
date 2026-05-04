using UnityEngine;

public class CrabController : MonoBehaviour
{
    public float speed = 1f;       // crabs are slower than ghosts
    public int health = 50;        // but tougher!
    public float attackRange = 1f; // how close before it attacks
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    private float viimeksiHyokkasi = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float etaisyys = Vector2.Distance(transform.position, player.position);

        // Move toward player if not in attack range
        if (etaisyys > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            // Attack if close enough and cooldown has passed
            if (Time.time - viimeksiHyokkasi >= attackCooldown)
            {
                HyökkääPelaajaan();
                viimeksiHyokkasi = Time.time;
            }
        }
    }

    void HyökkääPelaajaan()
    {
        Debug.Log("Rapu hyökkää!");
        PlayerDataManager.Instance.RemoveHealth(attackDamage);
    }

    public void OtaVahinko(int maara)
    {
        health -= maara;
        Debug.Log("Crab health: " + health);

        if (health <= 0)
        {
            Debug.Log("Rapu kuoli!");
            Destroy(gameObject);
        }
    }
}