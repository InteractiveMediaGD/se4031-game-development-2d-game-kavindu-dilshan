using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    public int scoreValue = 10;
    private bool hasScored = false; // Give score only once per gap

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasScored)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
                hasScored = true;
            }
        }
    }
}
