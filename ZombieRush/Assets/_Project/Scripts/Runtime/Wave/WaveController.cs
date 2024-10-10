using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WaveController : MonoBehaviour
{
    [SerializeField] private WaveConfig[] waveConfigs;

    private WaveConfig curentWave;
    private SubWave curentSubwave;
    private EnemySpawner enemySpawner;
    private WaveView waveView;

    private int subWaveCount;
    private int waveCount;

    [Inject]
    private void Construct(EnemySpawner enemySpawner, WaveView waveView)
    {
        this.enemySpawner = enemySpawner;
        this.waveView = waveView;
    }
    private void OnEnable()
    {
        enemySpawner.endSpawnSubWave += NextSubWave;
    }
    private void OnDisable()
    {
        enemySpawner.endSpawnSubWave -= NextSubWave;
    }

    public void StartGame()
    {
        curentWave = waveConfigs[0];
        curentSubwave = curentWave.subWaves[0];
        subWaveCount = 0;
        waveCount = 0;
        waveView.ChangeWaveText(waveCount);
        enemySpawner.StartSpawn(curentSubwave, curentWave.timeBetweenSubwave);
        subWaveCount++;
    }
    private void NextSubWave()
    {
        if(subWaveCount < curentWave.subWaves.Length)
        {
            curentSubwave = curentWave.subWaves[subWaveCount];
            enemySpawner.StartSpawn(curentSubwave,curentWave.timeBetweenSubwave);
            subWaveCount++;
        }
        else
        {
            NextWave();
        }

    }

    private void NextWave()
    {
        waveCount++;
        subWaveCount = 0;
        curentWave = waveConfigs[waveCount];
        curentSubwave = curentWave.subWaves[subWaveCount];
        enemySpawner.StartSpawn(curentSubwave, curentWave.timeBetweenSubwave);
        waveView.ChangeWaveText(waveCount);
        subWaveCount++;
    }
}