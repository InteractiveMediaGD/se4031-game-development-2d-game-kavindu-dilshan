using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float offScreenX = -15f;

    // Optional: We can add an 'offsetSpeed' if we want elephants to move slightly faster than trees later, 
    // but for now, everything moves perfectly synchronized.
    public float extraSpeedOffset = 0f;

    void Update()
    {
        // Don't move if GameManager is missing or game is over
        if (GameManager.Instance == null || GameManager.Instance.isGameOver) return;

        // Grab the global speed and add our offset (0 by default)
        float currentSpeed = GameManager.Instance.currentGlobalSpeed + extraSpeedOffset;

        // Move the object to the left using that precise global speed
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);

        // Destroy the object if it goes too far off screen
        if (transform.position.x < offScreenX)
        {
            Destroy(gameObject);
        }
    }
}
