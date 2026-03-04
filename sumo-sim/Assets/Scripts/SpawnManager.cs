using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  float _spawnRange;
  int _enemyCount, _currEnemyCount;
  [SerializeField] GameObject emenyPrefab;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    _enemyCount = 3;
    _spawnRange = 9.0f;
    SpawnEnemyWave(_enemyCount);
  }
  
  // Update is called once per frame
  void Update()
  {
    _currEnemyCount = GameObject.FindObjectsByType<EnemyController>(FindObjectsSortMode.None).Length;
    if (_currEnemyCount <= 0) { 
      SpawnEnemyWave(_enemyCount);
    }
  }
  void SpawnEnemyWave(int wave_n)
  {
    for (int i = 0; i < wave_n; i++)
    {
      Instantiate(emenyPrefab,RandomSpawnPos(), emenyPrefab.transform.rotation);
    }
  }
  Vector3 RandomSpawnPos()
  {
    float spawnPosX = Random.Range(-_spawnRange, _spawnRange);
    float spawnPosZ = Random.Range(-_spawnRange, _spawnRange);
    return  new Vector3(spawnPosX, 0.25f, spawnPosZ);
  }
}
