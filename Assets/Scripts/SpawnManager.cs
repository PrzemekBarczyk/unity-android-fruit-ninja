using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] targetPrefabs;
    [SerializeField] float minSpawnRate;
    [SerializeField] float maxSpawnRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
	{
        while (true)
		{
            yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
            if (GameManager.State == State.Playing)
			{
                int randomTargetIndex = Random.Range(0, targetPrefabs.Length);
                Instantiate(targetPrefabs[randomTargetIndex], transform.position, transform.rotation);
            }
        }
	}
}
