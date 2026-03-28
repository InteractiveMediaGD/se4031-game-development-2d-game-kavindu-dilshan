using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    // Limits how far up and down the player can go
    public float maxY = 4.5f;
    public float minY = -4.5f;

    private float verticalInput;

    private void Update()
    {
        // Stop the player from moving if the game is over
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            return;
        }

        // Get vertical input (W/S keys or Up/Down arrows). Returns between -1 and 1.
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // If the game is over, skip physics updates
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;

        // Calculate the new Position
        Vector3 currentPosition = transform.position;
        currentPosition.y += verticalInput * moveSpeed * Time.fixedDeltaTime;

        // Clamp restricts the Y value so the player can't fly off screen
        currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);

        // Apply movement
        transform.position = currentPosition;
    }
}
