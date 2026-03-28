using UnityEngine;
using TMPro;
using UnityEngine.UI; // Needed to control the Image component

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public Image dangerOverlayImage;

    [Header("Danger Mode Settings")]
    public int mediumHealthThreshold = 60;
    public int lowHealthThreshold = 30;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;

            // Health color logic
            if (currentHealth <= lowHealthThreshold) 
            {
                healthText.color = Color.red;
                SetDangerOverlay(true); // Turn on the red screen tint
            }
            else if (currentHealth <= mediumHealthThreshold) 
            {
                healthText.color = Color.yellow;
                SetDangerOverlay(false); // Make sure it's off if we heal
            }
            else 
            {
                healthText.color = Color.white; // Or Color.green
                SetDangerOverlay(false);
            }
        }
    }

    private void SetDangerOverlay(bool isActive)
    {
        if (dangerOverlayImage != null)
        {
            Color color = dangerOverlayImage.color;
            // If active, set alpha to 0.3 (30% opacity). If false, set to 0 (invisible)
            color.a = isActive ? 0.3f : 0f; 
            dangerOverlayImage.color = color;
        }
    }

    public void UpdateScore(int currentScore)
    {
        if (scoreText != null) scoreText.text = "Score: " + currentScore;
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }
}
