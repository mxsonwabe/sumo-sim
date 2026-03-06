using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  float _spawnRange;
  int _enemyCountWave, _currEnemyCount;
  [SerializeField] GameObject[] emenyPrefab;
  [SerializeField] GameObject powerUpPrefab;
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
    if (_currEnemyCount <= 0) {
      _enemyCountWave += 1;
      SpawnEnemyWave(_enemyCountWave);
    }
  }
  void SpawnEnemyWave(int wave_n)
  {
    for (int i = 0; i < wave_n; i++)
    {
      int idx = Random.Range(0, emenyPrefab.Length);
      Instantiate(emenyPrefab[idx],RandomSpawnPos(), emenyPrefab[idx].transform.rotation);
    }
    Instantiate(powerUpPrefab,RandomSpawnPos(), powerUpPrefab.transform.rotation);
  }
  Vector3 RandomSpawnPos()
  {
    float spawnPosX = Random.Range(-_spawnRange, _spawnRange);
    float spawnPosZ = Random.Range(-_spawnRange, _spawnRange);
    return  new Vector3(spawnPosX, 0.25f, spawnPosZ);
  }
}
