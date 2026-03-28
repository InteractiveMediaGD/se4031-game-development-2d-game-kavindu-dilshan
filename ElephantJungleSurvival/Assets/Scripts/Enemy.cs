using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageAmount = 30;
    public int scoreOnKill = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the elephant tramples the player
        if (collision.CompareTag("Player"))
        {
            // 🐘 🎧 Player is trampled! Play the damage/roar sound!
            if (AudioManager.Instance != null) AudioManager.Instance.PlayEnemyHitPlayerSound();

            PlayerHealth healthLogic = collision.GetComponent<PlayerHealth>();
            if (healthLogic != null)
            {
                healthLogic.TakeDamage(damageAmount);
            }
            // Destroy the elephant upon crashing into the player
            Destroy(gameObject);
        }
    }

    // This is explicitly called by the Projectile script when an arrow/rock hits the elephant
    public void DieFromProjectile()
    {
        // 🎯 🎧 Target hit sound triggered!
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayHitSound();
        }

        if (GameManager.Instance != null)
        {
            // Reward the player for actively shooting the enemy!
            GameManager.Instance.AddScore(scoreOnKill);
        }
        
        // Remove the elephant from the game
        Destroy(gameObject);
    }
}
