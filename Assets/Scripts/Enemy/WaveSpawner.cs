using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave
{
    public int count;
    public float rate;
    public string name;
    public Transform enemy;
}

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState
    {
        Counting,
        Waiting,
        Spawning
    }

    public Wave[] waves;

    private float waveCountdown;
    private float timeBetween = 5f;
    private float searchCountdown = 1f;
    private int nextWave = 0;
    private SpawnState state = SpawnState.Counting;

    void Start()
    {
        waveCountdown = timeBetween;
    }

    void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
                WaveCompleted();
            else
                return;
        }
        if (waveCountdown <= 0)
            if (state != SpawnState.Spawning)
                StartCoroutine(SpawnWave(waves[nextWave]));
            else
                waveCountdown -= Time.deltaTime;
    }

    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;
        }
        return true;
    }

    private void WaveCompleted()
    {
        state = SpawnState.Counting;
        waveCountdown = timeBetween;
        if (nextWave + 1 > waves.Length - 1)
            nextWave = 0;
        else
            nextWave++;

    }

    private void SpawnEnemy(Transform _enemy)
    {
        Instantiate(_enemy, transform.position, transform.rotation);
    }

    private IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.Spawning;
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.Waiting;
        yield break;
    }
}
