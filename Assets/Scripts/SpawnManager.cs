using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Targets List")]
    [SerializeField] Target[] targetPrefabs;

    [Header("Targets Spawn Rate")]
    [SerializeField] float minSpawnRate = 0.05f;
    [SerializeField] float maxSpawnRate = 2f;

    [Header("Forces Values Applied to Targets at Start")]
    [SerializeField] float maxVerticalForce = 14f;
    [SerializeField] float minVerticalForce = 8f;
    [SerializeField] float maxHorizontalForce = 5f;
    [SerializeField] float maxTorque = 10f;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        StartCoroutine(SpawnAndThrowTarget());
    }

    IEnumerator SpawnAndThrowTarget()
	{
        while (true)
		{
            yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));

            if (gameManager.State == State.Playing)
			{
                int randomTargetIndex = Random.Range(0, targetPrefabs.Length);
                Target newTarget = Instantiate(targetPrefabs[randomTargetIndex], transform);
                newTarget.MyRigidbody.AddForce(RandomForce(), ForceMode.Impulse);
                newTarget.MyRigidbody.AddTorque(RandomTorque(), ForceMode.Impulse);
            }
        }
	}

    public Vector3 RandomForce()
    {
        return new Vector3(Random.Range(-maxHorizontalForce, maxHorizontalForce), Random.Range(minVerticalForce, maxVerticalForce), 0f);
    }

    public Vector3 RandomTorque()
    {
        return new Vector3(Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque));
    }
}
