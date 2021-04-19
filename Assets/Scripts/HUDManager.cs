using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;

	public void UpdateScore(int newScore)
	{
        scoreText.text = "Score: " + newScore.ToString();
	}

    public void UpdateLives(int newLives)
	{
        livesText.text = "Lives: " + newLives.ToString();
	}
}
