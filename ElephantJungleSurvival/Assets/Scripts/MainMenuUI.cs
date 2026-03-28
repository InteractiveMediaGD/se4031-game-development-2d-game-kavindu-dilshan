using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject highScorePanel;
    
    [Header("High Score Display")]
    public TextMeshProUGUI bestScoreDisplayText;
    
    [Header("Scene Settings")]
    public string gameplaySceneName = "ElephantJungle_Scene";

    public void StartGame()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClickSound();

        // No more profiles to save, just instantly load the game!
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OpenHighScores()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClickSound();

        if (highScorePanel != null)
        {
            UpdateHighScoreDisplay(); // Force the text to refresh BEFORE we show the panel!
            highScorePanel.SetActive(true);
        }
    }

    private void UpdateHighScoreDisplay()
    {
        // 1. Read the global score from the hard drive (returns 0 if completely new game)
        int currentBestScore = PlayerPrefs.GetInt("BestHighScore", 0);

        // 2. Update the text string visually on the panel
        if (bestScoreDisplayText != null)
        {
            bestScoreDisplayText.text = "Best Score: " + currentBestScore;
        }
    }

    public void CloseHighScores()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClickSound();

        if (highScorePanel != null)
        {
            highScorePanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClickSound();

        Debug.Log("Application Quitting... (Closing window in Build)");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
