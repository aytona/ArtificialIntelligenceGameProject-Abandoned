using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public Vector3 spawnLocation;
    public int enemyCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private GameObject player;

    void Start()
    {
        StartCoroutine(SpawnWaves());
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnLevelWasLoaded()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = new Vector3(spawnLocation.x, spawnLocation.y,Random.Range(-spawnLocation.z, spawnLocation.z));
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemy, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (player == null)
                break;
        }
    }
}
