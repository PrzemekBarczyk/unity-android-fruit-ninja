using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject hud;
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;

	[SerializeField] GameObject mainMenu;

	public void DisplayHUD(bool display)
	{
		hud.SetActive(display);
	}

	public void UpdateScore(int newScore)
	{
        scoreText.text = "Score: " + newScore.ToString();
	}

    public void UpdateLives(int newLives)
	{
        livesText.text = "Lives: " + newLives.ToString();
	}

	public void DisplayMainMenu(bool display)
	{
		mainMenu.SetActive(display);
	}
}
