using UnityEngine;

public enum SpawnOption
{
	RandomWave,
	WaveAllAtOnce,
	WaveOneByOne,
    OneAtOnce
}

public static class SpawnOptionMenthods
{
    public static SpawnOption RandowmWave(this SpawnOption spawnOption)
    {

        int randomInt = Random.Range(0, 2);

        if (randomInt == 0)
            return SpawnOption.WaveAllAtOnce;
        else
            return SpawnOption.WaveOneByOne;
    }
}
