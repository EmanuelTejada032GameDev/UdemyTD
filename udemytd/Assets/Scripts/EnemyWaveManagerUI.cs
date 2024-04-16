using System;
using TMPro;
using UnityEngine;

public class EnemyWaveManagerUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveSpawner _enemyWaveSpawner;
    [SerializeField] private TextMeshProUGUI _currentWaveText;
    [SerializeField] private TextMeshProUGUI _nextWaveMessageText;

    void Start()
    {
        _enemyWaveSpawner.OnWaveChanged += OnWaveChanged;
    }

    void Update()
    {
        UpdateTimerText();
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
