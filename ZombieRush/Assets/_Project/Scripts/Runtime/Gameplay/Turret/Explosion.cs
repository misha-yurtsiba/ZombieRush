using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Explosion : MonoBehaviour, IPauseble
{
    private ObjectPool<Explosion> explosionPool;
    private ParticleSystem exp;
    private IPause pauseGame;

    [Inject]
    private void Construct(IPause pauseGame)
    {
        this.pauseGame = pauseGame;
    }
    private void OnEnable() => pauseGame.pause += IsGamePaused;
    private void OnDisable() => pauseGame.pause -= IsGamePaused;
    public void Init(ObjectPool<Explosion> explosionPool)
    {
        this.explosionPool = explosionPool;

        exp = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = exp.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }
    public void PlayEfect()
    {
        exp.Play();
    }

    private void OnParticleSystemStopped()
    {
        explosionPool.Relese(this);
    }
    public void IsGamePaused(bool isGamePaused)
    {
        if (isGamePaused)
            exp.Pause();
        else
            exp.Play();
    }
}
