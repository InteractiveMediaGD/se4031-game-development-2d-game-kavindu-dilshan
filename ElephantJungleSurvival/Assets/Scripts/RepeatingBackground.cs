using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D bgCollider;
    private float backgroundWidth;

    [Tooltip("Set below 1 to make the background move slower than the foreground trees (Parallax effect)")]
    public float parallaxSpeedMultiplier = 0.5f;

    void Start()
    {
        bgCollider = GetComponent<BoxCollider2D>();
        
        // Calculate the exact width of this specific background sprite
        // We multiply the collider's raw X size by the localScale X to get the true Unity world-space width
        backgroundWidth = bgCollider.size.x * transform.localScale.x;

        // If your box collider was a trigger to avoid physics bugs, ensure it's set as a trigger.
        bgCollider.isTrigger = true; 
    }

    void Update()
    {
        // Stop moving if the game is over
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;

        // Calculate speed (Global speed * Parallax Multiplier)
        // A 0.5 multiplier means the background moves half as fast as the trees, creating depth!
        float currentSpeed = GameManager.Instance.currentGlobalSpeed * parallaxSpeedMultiplier;

        // Move to the left
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);

        // If the background moves exactly one full width to the left past the center of the camera...
        if (transform.position.x < -backgroundWidth)
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        // Jump the background perfectly forward by exactly twice its width 
        // to loop cleanly behind the second background piece!
        Vector2 offset = new Vector2(backgroundWidth * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
