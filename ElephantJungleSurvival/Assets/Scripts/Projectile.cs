using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore the player, score zones, and health packs
        if (collision.CompareTag("Player") || collision.CompareTag("ScoreZone") || collision.CompareTag("HealthPack"))
        {
            return;
        }

        // If it hits an Enemy, tell the enemy to die and grant score!
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemyScript = collision.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.DieFromProjectile();
            }
            Destroy(gameObject); // Destroy the rock after hitting the elephant
        }
        // If it hits a tree wall, just destroy the rock harmlessly
        else if (collision.CompareTag("Obstacle")) 
        {
            Destroy(gameObject);
        }
    }
}
