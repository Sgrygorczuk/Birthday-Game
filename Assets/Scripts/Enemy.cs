using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //==================================================================================================================
    // Variables 
    //==================================================================================================================

    //Movement Controls 
    public Rigidbody2D _rigidbody2D; //The rigidbody that will move the bullet 
    public float minSpeed = 1f;           //Speed at which the bullet moves 
    public float maxSpeed = 4f;           //Speed at which the bullet moves 

    //Flag and Timer 
    public float deathTime = 100f;   //How long before the bullet dies 
    public bool playerBullet = true; //Is the bullet used by player or enemy 

    // Component 
    private GameController _gameController;

    //==================================================================================================================
    // Base Method  
    //==================================================================================================================

    //Checks who is shooting the bullet and set up the bullet settings 
    private void Start()
    {
        _gameController = GameObject.Find(Structs.Enemy.gameControllerComponent).GetComponent<GameController>();
        StartCoroutine(Death());
    }

    public void SetSpeed(Vector3 newSpeed)
    {
        _rigidbody2D.velocity = newSpeed * Random.Range(minSpeed,maxSpeed);
    }

    //==================================================================================================================
    // Bullet Set Up  
    //==================================================================================================================

    //Waits till timer is out then destroys the bullet 
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it touches the bullet, it updates 
        if (collision.gameObject.CompareTag(Structs.Enemy.bulletTag))
        {
            //Updates the Score 
            _gameController.UpdateScore();
            //Destroys the bullet
            Destroy(collision.gameObject);
            //Destroys the enemy 
            Destroy(gameObject);
        }
        // If the enemy touches a bound it gets destroyed   
        else if(collision.gameObject.CompareTag(Structs.Enemy.boundsTag))
        {
            Destroy(gameObject);
        }
    }
}
