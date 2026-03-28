using UnityEngine;

public class ScoreSaveManager : MonoBehaviour
{
    // A single, global key for the highest score ever achieved on this device!
    private const string ScoreKey = "BestHighScore";

    // Called automatically by the GameManager right as the player dies
    public void SaveBestScore(int currentScore)
    {
        // 1. See what the global best score is (returns 0 if nobody has played yet)
        int previousBest = PlayerPrefs.GetInt(ScoreKey, 0);

        // 2. Only overwrite the file if this run was better!
        if (currentScore > previousBest)
        {
            PlayerPrefs.SetInt(ScoreKey, currentScore);
            PlayerPrefs.Save(); // Forces the save to disk instantly
            Debug.Log("New Global High Score: " + currentScore + "!");
        }
        else
        {
            Debug.Log("You scored " + currentScore + ". Global best remains: " + previousBest);
        }
    }
}
