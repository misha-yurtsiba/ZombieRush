using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ObjectPool<Explosion> explosionPool;

    private ParticleSystem exp;
    public void Init(ObjectPool<Explosion> explosionPool)
    {
        this.explosionPool = explosionPool;

        exp =GetComponent<ParticleSystem>();
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
}
