using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if it's the player touching us
        if (collision.CompareTag("Player"))
        {
            // 🍌 🎧 Banana get! Play the pickup chime!
            if (AudioManager.Instance != null) AudioManager.Instance.PlayHealthPickupSound();

            // Grab the health script attached to the player
            PlayerHealth healthLogic = collision.GetComponent<PlayerHealth>();
            if (healthLogic != null)
            {
                // Heal the player (clamping to max health is safely handled inside PlayerHealth)
                healthLogic.Heal(healAmount);
                
                // Destroy this health pack so it can't be picked up twice
                Destroy(gameObject);
            }
        }
    }
}
