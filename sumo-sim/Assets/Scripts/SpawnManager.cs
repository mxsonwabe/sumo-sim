using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  float _spawnRange;
  int _enemyCountWave, _currEnemyCount;
  [SerializeField] GameObject[] enemyPrefabs;
  [SerializeField] GameObject[] powerUpPrefabs;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    _enemyCountWave = 1;
    _spawnRange = 9.0f;
    SpawnEnemyWave(_enemyCountWave);
  }

  // Update is called once per frame
  void Update()
  {
    _currEnemyCount = EnemyController.activeEnemyCount;
    if (_currEnemyCount <= 0)
    {
      _enemyCountWave += 1;
      SpawnEnemyWave(_enemyCountWave);
    }
  }
  void SpawnEnemyWave(int wave_n)
  {
    int idx;
    for (int i = 0; i < wave_n; i++)
    {
      idx = Random.Range(0, enemyPrefabs.Length);
      Instantiate(enemyPrefabs[idx], RandomSpawnPos(), enemyPrefabs[idx].transform.rotation);
    }
    idx = Random.Range(0, powerUpPrefabs.Length);
    Instantiate(powerUpPrefabs[idx], RandomSpawnPos(), powerUpPrefabs[idx].transform.rotation);
    //Instantiate(powerUpPrefabs[2], RandomSpawnPos(), powerUpPrefabs[2].transform.rotation);
  }
  Vector3 RandomSpawnPos()
  {
    float spawnPosX = Random.Range(-_spawnRange, _spawnRange);
    float spawnPosZ = Random.Range(-_spawnRange, _spawnRange);
    return new Vector3(spawnPosX, 0.25f, spawnPosZ);
  }
}
