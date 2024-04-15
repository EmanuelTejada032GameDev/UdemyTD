using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    

    [SerializeField] List<Transform> _spawnPoints;
    [SerializeField] Vector3 _currentSpawnPoint;
    private float _timerToSpawnWave;
    private float _nextEnemySpawnTimer;
    private float _remaininEnemiesToSpawn;

    private void Start()
    {
        _timerToSpawnWave = 3f;
    }

    private void Update()
    {
        _timerToSpawnWave -= Time.deltaTime;
        if(_timerToSpawnWave <= 0)
        {
            SpawnWave();
        }

        if(_remaininEnemiesToSpawn > 0)
        {
            _nextEnemySpawnTimer -= Time.deltaTime;
            if(_nextEnemySpawnTimer < 0f)
            {
                _nextEnemySpawnTimer = Random.Range(0f, .3f);
                Enemy.Create(_currentSpawnPoint + Utils.GetRandomDirection() * Random.Range(0, 10f));
                _remaininEnemiesToSpawn--;
            }
        }
        
    }


    private void SpawnWave()
    {
         _currentSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
        _timerToSpawnWave = 10f;
        _remaininEnemiesToSpawn = 10;
    }
}
