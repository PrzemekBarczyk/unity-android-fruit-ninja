using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool IsRunning { get; private set; }

    [SerializeField] int score;
    [SerializeField] int lives = 3;
    int actualScore;
    int actualLives;

    [SerializeField] UIManager uiManager;

	void Start()
	{
        actualScore = score;
        actualLives = lives;
	}

	public void StartGame()
	{
        actualScore = score;
        actualLives = lives;
        uiManager.UpdateScore(score);
        uiManager.UpdateLives(lives);
        uiManager.DisplayMainMenu(false);
        uiManager.DisplayHUD(true);
        IsRunning = true;
	}

    public void ExitGame()
	{
        Application.Quit();
	}

    public void GameOver()
	{
        if (IsRunning)
		{
            IsRunning = false;
            uiManager.DisplayHUD(false);
            uiManager.DisplayMainMenu(true);
        }
	}

    public void AddScore(int scoreToAdd)
	{
        actualScore += scoreToAdd;
        uiManager.UpdateScore(actualScore);
	}

    public void RemoveLife()
	{
        actualLives--;
        uiManager.UpdateLives(actualLives);
        if (actualLives == 0)
		{
            GameOver();
		}
	}
}
