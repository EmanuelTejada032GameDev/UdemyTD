using S = System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{

    public event S.EventHandler OnWaveChanged;

    private enum State
    {
        SpawningWave,
        WaitingToSpawnWave
    }

    [SerializeField] private State _currentState;
    [SerializeField] private int _currentWave;
    [SerializeField] private int _currentWaveEnemies;
    [SerializeField] private int _remaininEnemiesToSpawn;

    [SerializeField] private Transform _nextWaveSpawnPositionIndicator;

    [SerializeField] List<Transform> _spawnPoints;
    private Vector3 _currentSpawnPoint;
    private float _timerToSpawnNextWave;
    private float _nextEnemySpawnTimer;

    private void Start()
    {
        _currentState = State.WaitingToSpawnWave;
        _currentSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
        _nextWaveSpawnPositionIndicator.position = _currentSpawnPoint;
        _timerToSpawnNextWave = 3f;
    }

    private void Update()
    {
         switch (_currentState)
        {
            case State.WaitingToSpawnWave: CountDownToSpawnWave(); break;
            case State.SpawningWave:  SpawnEnemyWave(); break;
        }
    }

    private void CountDownToSpawnWave()
    {
        _timerToSpawnNextWave -= Time.deltaTime;
        if (_timerToSpawnNextWave <= 0)
        {
            SpawnWave();
        }
    }

    private void SpawnEnemyWave()
    {
        if (_remaininEnemiesToSpawn > 0)
        {
            _nextEnemySpawnTimer -= Time.deltaTime;
            if (_nextEnemySpawnTimer < 0f)
            {
                _nextEnemySpawnTimer = Random.Range(0f, .3f);
                Enemy.Create(_currentSpawnPoint + Utils.GetRandomDirection() * Random.Range(0, 10f));
                _remaininEnemiesToSpawn--;
            }
        }
        else
        {
            _currentState = State.WaitingToSpawnWave;
            _currentSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
            _nextWaveSpawnPositionIndicator.position = _currentSpawnPoint;
            _timerToSpawnNextWave = 10f;
        }
    }


    private void SpawnWave()
    {
        _remaininEnemiesToSpawn = 5 + 3 * _currentWave;
        _currentWaveEnemies = _remaininEnemiesToSpawn;
        _currentState = State.SpawningWave;
        _currentWave++;
        OnWaveChanged?.Invoke(this, S.EventArgs.Empty);
    }

    public int GetCurrentWave() => _currentWave;
    public float GetTimerToSpawnNextWave() => _timerToSpawnNextWave;
}
