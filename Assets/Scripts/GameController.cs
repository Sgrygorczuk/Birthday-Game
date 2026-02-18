using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
    public static GameController Instance;
    // Score 
    public TextMeshProUGUI scoreText;
    private int _score = 0;
    
    //NPC Timer
    public float spawnInterval = 5f; // Seconds between spawns
    private float _timer;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval) {
            SpawnFromPool();
            _timer = 0f; // Reset the timer
        }
    }

    private void SpawnFromPool()
    {
        GameObject npc = NPCPool.Instance.GetPooledObject();

        if (npc != null)
        {
            npc.transform.position = transform.position;
            npc.transform.rotation = transform.rotation;
            npc.SetActive(true); // "Spawn" the NPC
        }
    }
    
    // Updates the score and UI 
    public void UpdateScore() {
        _score++;
        scoreText.text = _score.ToString(); 
    }
  
    // Reload the current scene 
    public void ReloadLevel() {   SceneManager.LoadScene(SceneManager.GetActiveScene().name);}
}
