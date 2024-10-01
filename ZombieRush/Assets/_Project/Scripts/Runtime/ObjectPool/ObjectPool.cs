using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Stack<T> poolObjects;
    private GameObjectFactory<T> factory;

    public ObjectPool(GameObjectFactory<T> factory,int startPoolCapasity)
    {
        this.factory = factory;
        poolObjects = new Stack<T>();

        for (int i = 0; i < startPoolCapasity; i++)
        {
            T obj = Create(Vector3.zero);
            obj.gameObject.SetActive(false);
            poolObjects.Push(obj);
        }
    }

    private T Create(Vector3 spawnPos)
    {
        return factory.Create(spawnPos);
    }
    public T Get(Vector3 spawnPos)
    {
        if (poolObjects.Count == 0)
            return Create(spawnPos);
        else
        {
            T obj = poolObjects.Pop();
            obj.gameObject.SetActive(true);
            obj.transform.position = spawnPos;
            return obj;
        }
    }

    public void Relese(T obj)
    {
        obj.gameObject.SetActive(false);
        poolObjects.Push(obj);
    }
}
