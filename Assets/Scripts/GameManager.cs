using UnityEngine;

public enum State { MainMenu, PauseMenu, Playing };

public class GameManager : MonoSingleton<GameManager>
{
    public State State { get; private set; } = State.MainMenu;

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

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            if (State == State.Playing)
			{
                PauseGame();
			}
			else if (State == State.PauseMenu)
			{
                ContinueGame();
			}
		}
	}

	public void StartGame()
	{
        actualScore = score;
        actualLives = lives;
        uiManager.UpdateScore(score);
        uiManager.UpdateBestScore(PlayerPrefs.GetInt("best score"));
        uiManager.UpdateLives(lives);
        uiManager.DisplayMainMenu(false);
        uiManager.DisplayHUD(true);
        State = State.Playing;
	}

    public void ExitGame()
	{
        Application.Quit();
	}

    public void PauseGame()
	{
        State = State.PauseMenu;
        Time.timeScale = 0f;
        uiManager.DisplayPauseMenu(true);
	}

    public void ContinueGame()
	{
        uiManager.DisplayPauseMenu(false);
        Time.timeScale = 1f;
        State = State.Playing;
    }

    public void GameOver()
	{
        if (State == State.Playing)
		{
            if (actualScore > PlayerPrefs.GetInt("best score"))
                PlayerPrefs.SetInt("best score", actualScore);
            State = State.MainMenu;
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
