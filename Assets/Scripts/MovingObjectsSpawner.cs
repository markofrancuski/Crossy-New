using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectsSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private bool isRightSide;
    [SerializeField] private bool isLog;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    GameObject go;

    private IEnumerator SpawnObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            if(isLog)
            {
                go = Pooler.instance.getLog();//Instantiate(objectToSpawn, spawnPosition.position, Quaternion.identity);
            }
            else
            {
                int rnd = Random.Range(0, 4);
                go = Pooler.instance.getCar(rnd);
            }

            if (go != null)
            {
                go.transform.position = spawnPosition.position;
                go.transform.rotation = Quaternion.Euler(0, spawnPosition.rotation.y, 0);
                if (!isRightSide)
                {
                    go.transform.Rotate(new Vector3(0, 180, 0));
                }
                go.SetActive(true);
            }
        }
    }
}
