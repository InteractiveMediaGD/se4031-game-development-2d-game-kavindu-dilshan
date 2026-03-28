using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    public int damageAmount = 20;
    private bool hasDamaged = false; // Prevents spamming damage every frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the trigger is the Player
        if (collision.CompareTag("Player") && !hasDamaged)
        {
            // 💥 🎧 Heavy impact noise when player crashes
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayImpactSound();
            }

            PlayerHealth healthLogic = collision.GetComponent<PlayerHealth>();
            if (healthLogic != null)
            {
                healthLogic.TakeDamage(damageAmount);
                hasDamaged = true; // Ensure they only take damage once per obstacle part
            }
        }
    }
}
