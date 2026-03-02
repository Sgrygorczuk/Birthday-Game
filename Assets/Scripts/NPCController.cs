using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Random = UnityEngine.Random;

public class NPCController : MonoBehaviour
{
    // --- COMPONENT REFERENCES ---
    // These hold references to other parts of the NPC or the Pathfinding system
    private AIDestinationSetter _aiDestinationSetter;
    private AIPath _aiPath;
    private GameObject _spawnPoints;

    // --- TARGETS & POINTS ---
    // These track where the NPC is, where it’s going, and potential destinations
    private readonly List<GameObject> _points = new List<GameObject>(); // List of all possible spawn/target points
    public GameObject startPoint;    // Where the NPC just came from
    public GameObject destination;   // The final target point
    
    private Animator _animator;

    private void Start() 
    {
        _animator = GetComponent<Animator>();
        _animator.Play(new string[] { "Snake", "Bird" }[Random.Range(0, 2)]);
        
        // 1. Link the code to the components attached to this NPC
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
        _aiPath = GetComponent<AIPath>();

        // 3. Find all spawn points in the scene and put them in the list
        _spawnPoints = GameObject.Find("SpawnPoints");
        for (int i = 0; i < _spawnPoints.transform.childCount; i++) {
            _points.Add(_spawnPoints.transform.GetChild(i).gameObject);
        }
        
        // 4. Pick a random starting point and teleport there
        startPoint = _points[Random.Range(0, _points.Count)];  
        gameObject.transform.position = startPoint.transform.position;
        
        destination = GameObject.Find("Player") ? GameObject.Find("Player") : gameObject;
        _aiDestinationSetter.target = destination.transform;
    }

    public void Update() {
        // Keep the NPC strictly on the 2D plane (Z = 0)
        transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }
}