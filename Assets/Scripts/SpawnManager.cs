using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Object Pooler")]
    [SerializeField] ObjectPooler targetsPooler;

    [Header("Spawn Option")]
    [SerializeField] SpawnOption spawnOption = SpawnOption.RandomWave;
    [Space]
    [SerializeField] [Min(0)] int minTargetsInAllAtOnce = 3;
    [SerializeField] [Min(0)] int maxTargetsInAllAtOnce = 6;
    [Space]
    [SerializeField] [Min(0)] int minTargetsInOneByOne = 3;
    [SerializeField] [Min(0)] int maxTargetsInOneByOne = 8;

    [Header("Spawn Rate In Seconds")]
    [SerializeField] [Min(0)] float minSpawnRateInAllAtOnce = 1f;
    [SerializeField] [Min(0)] float maxSpawnRateInAllAtOnce = 4f;
    [Space]
    [SerializeField] [Min(0)] float minSpawnRateInOneByOne = 0.05f;
    [SerializeField] [Min(0)] float maxSpawnRateInOneByOne = 1.5f;
    [Space]
    [SerializeField] [Min(0)] float minSpawnRateInOneAtOnce = 2;
    [SerializeField] [Min(0)] float maxSpawnRateInOneAtOnce = 8;


    [Header("Forces Values Applied to Objects at Spawn")]
    [SerializeField] [Min(0)] float maxVerticalForce = 14f;
    [SerializeField] [Min(0)] float minVerticalForce = 8f;
    [Space]
    [SerializeField] [Min(0)] float maxHorizontalForce = 5f;
    [Space]
    [SerializeField] [Min(0)] float maxTorque = 10f;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        StartCoroutine(SelectSpawnOption());
    }

	IEnumerator SelectSpawnOption()
	{
        while (true)
        {
            SpawnOption randomSpawnOption = spawnOption;
            if (spawnOption == SpawnOption.RandomWave)
            {
                randomSpawnOption = spawnOption.RandowmWave();
            }

            Coroutine selectedCoroutine;
            if (randomSpawnOption == SpawnOption.WaveAllAtOnce)
            {
                selectedCoroutine = StartCoroutine(SpawnWaveAllAtOnce());
            }
            else if (randomSpawnOption == SpawnOption.WaveOneByOne)
			{
                selectedCoroutine = StartCoroutine(SpawnWaveOneByOne());
            }
            else // SpawnOption.OneAtOnce
            {
                selectedCoroutine = StartCoroutine(SpawnOneAtOnce());
            }

            yield return selectedCoroutine; // wait for selected coroutine
        }
	}

    IEnumerator SpawnWaveAllAtOnce()
	{
        yield return new WaitForSeconds(Random.Range(minSpawnRateInAllAtOnce, maxSpawnRateInAllAtOnce));

        if (gameManager.State == State.Playing)
        {
            int numberOfTargetsToSpawn = Random.Range(minTargetsInAllAtOnce, maxTargetsInAllAtOnce);

            Vector3 randomVerticalForce = RandomVerticalForce();

            for (int i = 0; i < numberOfTargetsToSpawn; i++)
			{
                int randomTargetIndex = Random.Range(0, targetsPooler.PoolSize());

                Target newTarget = targetsPooler.GetFromPool(randomTargetIndex);

                newTarget.ResetForces();
                newTarget.AddForce(randomVerticalForce);
                newTarget.AddRandomHorizontalForce(maxHorizontalForce);
                newTarget.AddRandomTorque(maxTorque);
            }
        }
    }

    IEnumerator SpawnWaveOneByOne()
	{
        int numberOfTargetsToSpawn = Random.Range(minTargetsInOneByOne, maxTargetsInOneByOne);

        for (int i = 0; i < numberOfTargetsToSpawn; i++)
	    {
            yield return new WaitForSeconds(Random.Range(minSpawnRateInOneByOne, maxSpawnRateInOneByOne));

            if (gameManager.State == State.Playing)
            {
                int randomTargetIndex = Random.Range(0, targetsPooler.PoolSize());

                Target newTarget = targetsPooler.GetFromPool(randomTargetIndex);

                newTarget.ResetForces();
                newTarget.AddRandomForce(minVerticalForce, maxVerticalForce, maxHorizontalForce);
                newTarget.AddRandomTorque(maxTorque);
            }
        }
    }

    IEnumerator SpawnOneAtOnce()
	{
        yield return new WaitForSeconds(Random.Range(minSpawnRateInOneAtOnce, maxSpawnRateInOneAtOnce));

        if (gameManager.State == State.Playing)
        {
            int randomTargetIndex = Random.Range(0, targetsPooler.PoolSize());

            Target newTarget = targetsPooler.GetFromPool(randomTargetIndex);

            newTarget.ResetForces();
            newTarget.AddRandomForce(minVerticalForce, maxVerticalForce, maxHorizontalForce);
            newTarget.AddRandomTorque(maxTorque);
        }
    }

    Vector3 RandomVerticalForce() => Vector3.up * Random.Range(minVerticalForce, maxVerticalForce);
}
