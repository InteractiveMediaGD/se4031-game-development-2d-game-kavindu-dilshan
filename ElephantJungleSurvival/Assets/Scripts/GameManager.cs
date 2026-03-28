using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public int score = 0;
    public bool isGameOver = false;

    [Header("Speed Settings")]
    public float startingSpeed = 4f;
    public float speedIncreaseRate = 0.25f;
    public float maxSpeed = 12f;
    
    [HideInInspector]
    public float currentGlobalSpeed;

    private ScoreSaveManager scoreSaveManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Hook up the new score saver automatically
        scoreSaveManager = GetComponent<ScoreSaveManager>();

        currentGlobalSpeed = startingSpeed;
        UpdateScoreDisplay();

        // 🎼 🎧 Play Jungle BGM instantly when game starts!
        if (AudioManager.Instance != null && AudioManager.Instance.bgmClip != null)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.bgmClip);
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        currentGlobalSpeed += speedIncreaseRate * Time.deltaTime;
        currentGlobalSpeed = Mathf.Clamp(currentGlobalSpeed, startingSpeed, maxSpeed);
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        score += amount;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScore(score);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        
        // 🎧 Stop background music and play Game Over sound!
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGameOverSound();
        }

        // 🌟 NEW: Instantly save the player's score the exact millisecond they die!
        if (scoreSaveManager != null)
        {
            scoreSaveManager.SaveBestScore(score);
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowGameOver();
        }
    }

    public void RestartGame()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClickSound();

        // Because PlayerPrefs.Save() was called inside GameOver(), it is 100% safe to reload the scene instantly here!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 🌟 NEW: Link this to your new Main Menu button on the Game Over Panel!
    public void LoadMainMenu()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClickSound();

        SceneManager.LoadScene("MainMenu"); // Make sure this perfectly matches your Main Menu scene filename!
    }
}
