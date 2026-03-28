using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // A simple static instance so the player can trigger it instantly
    public static CameraShake Instance;

    private Vector3 originalPos;
    private float shakeTimer = 0f;
    public float shakeMagnitude = 0.3f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            // Pick a random spot inside a tiny circle and move the camera there
            transform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reset perfectly when the timer runs out
            shakeTimer = 0f;
            transform.localPosition = originalPos;
        }
    }

    // Call this from PlayerHealth when taking damage!
    public void ShakeCamera(float duration)
    {
        shakeTimer = duration;
    }
}
