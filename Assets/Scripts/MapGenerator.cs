using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int distanceFromPlayer;

    public Vector3 positionToSpawn;
    [SerializeField] private int maxTerrains;
    [SerializeField] private int tempMax;
    
    [Header("")]
    [SerializeField] private List<TerrainData> terrains;

    [SerializeField]private List<GameObject> currentTerrains;

    [SerializeField] private Transform parent;

    [SerializeField] private TerrainData startingTerrain;

    private void Start()
    { 
        GameManager.instance.reset += ResetObject;
        tempMax = maxTerrains;
        SpawnStarting();
    }

    private void OnDisable(){
        GameManager.instance.reset -= ResetObject;
    }
  
    public void SpawnStarting()
    {
        for (int i = 0; i < startingTerrain.maxInRow; i++)
        {
            GameObject go = Instantiate(startingTerrain.prefabs[0], positionToSpawn, Quaternion.identity, parent);
            currentTerrains.Add(go);
            positionToSpawn.x++;
        }

        maxTerrains += startingTerrain.maxInRow;

        for (int i = 0; i < maxTerrains; i++)
        {
            SpawnTerrain(true, Vector3.zero);
        }
        maxTerrains = currentTerrains.Count;
    }

    public void SpawnTerrain(bool isStart, Vector3 playerPosition)
    {

        //Current position x of the next terrain spawn - player position x
        if((positionToSpawn.x - playerPosition.x < distanceFromPlayer) || isStart)
        {
            int rndTerrain = Random.Range(0, terrains.Count);
            int rndInRow = Random.Range(1, terrains[rndTerrain].maxInRow);

            for (int i = 0; i < rndInRow; i++)
            {
                GameObject go = Instantiate(terrains[rndTerrain].prefabs[Random.Range(0, terrains[rndTerrain].prefabs.Count)], positionToSpawn, Quaternion.identity, parent);
                currentTerrains.Add(go);
                if (!isStart)
                {
                    if (currentTerrains.Count > maxTerrains)
                    {
                        Destroy(currentTerrains[0]);
                        currentTerrains.RemoveAt(0);
                    }
                }
                positionToSpawn.x++;
            }
        }
    }


    private void ResetObject()
    {
        maxTerrains = tempMax;
        positionToSpawn.x = -3;
        for (int i = 0; i < currentTerrains.Count; i++)
        {
            Destroy(currentTerrains[i]);
        }
        currentTerrains.Clear();

        SpawnStarting();

    }

}
