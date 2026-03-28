using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    void Update()
    {
        // Stop shooting if the game is over
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;

        // 0 refers to the Left Mouse Button (or a tap on a mobile screen)
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Instantiate creates the projectile at the FirePoint's exact position
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // 🎧 Trigger the shooting sound!
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayShootSound();
            }
        }
    }
}
