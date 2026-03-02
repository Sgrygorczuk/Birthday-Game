using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController: MonoBehaviour
{
    public static GameController Instance;
    // Score 
    public TextMeshProUGUI scoreText;
    private int _score = 0;
    public int goal = 10;
    
    public bool gameOver = false;
    
    //NPC Timer
    public float spawnInterval = 3f; // Seconds between spawns
    private float _timer;

    private void Awake() {
        Instance = this;
        _timer = 0.5f;
    }

    private void Update() {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval && !gameOver) {
            SpawnFromPool();
            _timer = 0f; // Reset the timer
            _timer *= 0.95f;
        }
    }

    private void SpawnFromPool()
    {
        NPCPool.Instance.GetPooledObject();

    }
    
    // Updates the score and UI 
    public void UpdateScore() {
        _score++;
        scoreText.text = _score.ToString();
        if (_score >= goal) {
            //gameOver = true;
            //foreach (Transform child in enemies) { child.gameObject.SetActive(false); }
            //ShowRaccoon();
        }
    }
    
    // Reload the current scene 
    public void ReloadLevel() {   SceneManager.LoadScene(SceneManager.GetActiveScene().name);}
}
