using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject hud;
    [SerializeField] TextMeshProUGUI scoreText;
	[SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI livesText;

	[SerializeField] GameObject mainMenu;

	[SerializeField] GameObject pauseMenu;

	public void DisplayHUD(bool display)
	{
		hud.SetActive(display);
	}

	public void UpdateScore(int newScore)
	{
        scoreText.text = newScore.ToString();
	}

	public void UpdateBestScore(int newBestScore)
	{
		bestScoreText.text = "BEST " + newBestScore.ToString();
	}

    public void UpdateLives(int newLives)
	{
        livesText.text = "Lives: " + newLives.ToString();
	}

	public void DisplayMainMenu(bool display)
	{
		mainMenu.SetActive(display);
	}

	public void DisplayPauseMenu(bool display)
	{
		pauseMenu.SetActive(display);
	}
}
