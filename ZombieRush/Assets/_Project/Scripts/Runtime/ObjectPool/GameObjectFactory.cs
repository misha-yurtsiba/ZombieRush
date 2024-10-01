using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class GameObjectFactory<T> where T : MonoBehaviour
{
    protected DiContainer diContainer;
    protected T objectPrefab;

    public GameObjectFactory(DiContainer diContainer, T objectPrefab)
    {
        this.diContainer = diContainer;
        this.objectPrefab = objectPrefab;
    }

    public abstract T Create(Vector3 spawnPos);
    

}
