using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] int lives = 3;

    [SerializeField] HUDManager hudManager;

    void Start()
    {
        hudManager.UpdateScore(score);
        hudManager.UpdateLives(lives);
    }

    public void AddScore(int scoreToAdd)
	{
        score += scoreToAdd;
        hudManager.UpdateScore(score);
	}

    public void RemoveLife()
	{
        lives--;
        hudManager.UpdateLives(lives);
        if (lives == 0)
		{
            Debug.Log("Game Over");
		}
	}
}
