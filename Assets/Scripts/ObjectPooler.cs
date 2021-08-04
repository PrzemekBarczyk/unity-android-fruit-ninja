using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Object Pooler")]

    [SerializeField] List<Pool> _poolsToCreate;

    List<Queue<Target>> _pools;

    [System.Serializable]
    public class Pool
	{
        [SerializeField] Target _prefab;
        [SerializeField] int _size;

        public Target Prefab { get => _prefab; }
        public int Size { get => _size; }
	}

    protected virtual void Start()
    {
        _pools = new List<Queue<Target>>();

        foreach (Pool poolToCreate in _poolsToCreate)
		{
            Queue<Target> pool = new Queue<Target>();

            for (int i = 0; i < poolToCreate.Size; i++)
			{
                Target obj = Instantiate(poolToCreate.Prefab);
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(transform);
                pool.Enqueue(obj);
			}

            _pools.Add(pool);
		}
    }

    protected Target SpawnFromPool(int index)
	{
        Target objectToSpawn = _pools[index].Dequeue();

        objectToSpawn.transform.position = transform.position;
        objectToSpawn.gameObject.SetActive(true);

        _pools[index].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    protected int PoolSize()
	{
        return _poolsToCreate.Count;
	}
}
