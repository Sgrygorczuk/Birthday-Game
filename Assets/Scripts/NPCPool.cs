using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class NPCPool : MonoBehaviour
{
    public static NPCPool Instance;
    public GameObject NPCTrash;
    public float speed = 5;
    
    public GameObject npcPrefab;
    public int poolSize = 10;
    public  List<GameObject> pooledObjects = new List<GameObject>();
    public List<GameObject> spawnPositions = new List<GameObject>();

    private void Awake() {
        Instance = this;

        // Pre-spawn NPCs and set them to inactive
        for (int i = 0; i < poolSize; i++)
        {        
            GameObject obj = Instantiate(npcPrefab, NPCTrash.transform, true);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        GameObject parentObj = GameObject.Find("SpawnPoints");
        if (parentObj != null) {
            foreach (Transform child in parentObj.transform) {
                spawnPositions.Add(child.gameObject);
            }
        }
    }

    public void  GetPooledObject()
    {
        // Find an NPC that is currently not being used
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy) {
                int randomIndex = Random.Range(0, spawnPositions.Count);
                GameObject position = spawnPositions[randomIndex];
                pooledObjects[i].SetActive(true);
                speed *= 1.1f;
                pooledObjects[i].GetComponent<AIPath>().maxSpeed = speed;
                pooledObjects[i].transform.position = position.transform.position;
                return;
            }
        }
    }
}
