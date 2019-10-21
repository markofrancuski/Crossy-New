using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static Pooler instance;

    [SerializeField] private ObjectToPool logToPool;
    [SerializeField] private List<GameObject> logs;

    [SerializeField] private List<ObjectToPool> carsToPool;
    [SerializeField] private List<GameObject> cars;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameManager.instance.reset += DeactivateAll;

        for (int i = 0; i < logToPool.amount; i++)
        {
            GameObject go = Instantiate(logToPool.objectGO);
            go.transform.parent = logToPool.parent;
            logs.Add(go);
            go.SetActive(false);
        }

        foreach (ObjectToPool obj in carsToPool)
        {
            for (int i = 0; i < obj.amount; i++)
            {
                GameObject go = Instantiate(obj.objectGO);
                go.transform.parent = obj.parent;
                cars.Add(go);
                go.SetActive(false);
            }
        }

    }

    public GameObject getCar(int number)
    {
        for (int i = 49 * number; i < cars.Count; i++)
        {
            if (!cars[i].activeInHierarchy) return cars[i];
        }
        return null;
    }

    public GameObject getLog()
    {
        for(int i = 0; i < logs.Count; i++)
        {
            if (!logs[i].activeInHierarchy) return logs[i];
        }
        return null;
    }

    public void DeactivateAll()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].SetActive(false);
        }
        for (int i = 0; i < logs.Count; i++)
        {
            logs[i].SetActive(false);
        }
    }
}

[Serializable]
public class ObjectToPool
{
    public int amount;
    public Transform parent;
    public GameObject objectGO;
}