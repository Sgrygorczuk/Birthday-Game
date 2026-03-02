using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPool : MonoBehaviour
{
    public static NPCPool Instance;
    public GameObject NPCTrash; 

    public GameObject npcPrefab;
    public int poolSize = 10;
    private List<GameObject> pooledObjects = new List<GameObject>();

    private void Awake() {
        Instance = this;

        // Pre-spawn NPCs and set them to inactive
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(npcPrefab, NPCTrash.transform, true);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        // Find an NPC that is currently not being used
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }
        return null; // All NPCs are currently active
    }
}
