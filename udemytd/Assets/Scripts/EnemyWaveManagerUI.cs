using System;
using TMPro;
using UnityEngine;

public class EnemyWaveManagerUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveSpawner _enemyWaveSpawner;
    [SerializeField] private TextMeshProUGUI _currentWaveText;
    [SerializeField] private TextMeshProUGUI _nextWaveMessageText;
    [SerializeField] private RectTransform _nextWaveSpawnPositionIndicator;
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
        _enemyWaveSpawner.OnWaveChanged += OnWaveChanged;
    }

    void Update()
    {
        UpdateTimerText();
        HandleNextEnemyWaveSpawnIndicator();
    }

    private void HandleNextEnemyWaveSpawnIndicator()
    {
        Vector3 nextEnemyWaveSpawnPosition = _enemyWaveSpawner.GetNextSpawnPosition();
        Vector3 directionFromCameraToSpawnPosition = (nextEnemyWaveSpawnPosition - _mainCamera.transform.position).normalized;
        _nextWaveSpawnPositionIndicator.anchoredPosition = directionFromCameraToSpawnPosition * 400f;
        _nextWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, Utils.GetEulerAngleFromVector3(directionFromCameraToSpawnPosition));

        float distanceBetweenCameraAndSpawnPositionOnMap = Vector3.Distance(nextEnemyWaveSpawnPosition, _mainCamera.transform.position);
        _nextWaveSpawnPositionIndicator.gameObject.SetActive(distanceBetweenCameraAndSpawnPositionOnMap > _mainCamera.orthographicSize * 1.5);
    }

    private void UpdateTimerText()
    {
        float timerToSpawnNextWave = _enemyWaveSpawner.GetTimerToSpawnNextWave();

        if (timerToSpawnNextWave <= 0)
        {
            SetTimerToSpawnNextWaveMessage("");
        }
        else
        {
            SetTimerToSpawnNextWaveMessage("Next wave in " + _enemyWaveSpawner.GetTimerToSpawnNextWave().ToString("0")+ "s");
        }
    }

    private void OnWaveChanged(object sender, EventArgs e)
    {
        SetCurrentWaveText("Wave " + _enemyWaveSpawner.GetCurrentWave());
    }


    private void SetCurrentWaveText(string newValue)
    {
        _currentWaveText.SetText(newValue);
    }

    private void SetTimerToSpawnNextWaveMessage(string newValue)
    {
        _nextWaveMessageText.SetText(newValue);
    }
}
