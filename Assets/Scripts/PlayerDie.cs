using UnityEngine;

public class PlayerDie : MonoBehaviour {
    // Game Object 
    public GameObject canvas;

    private void OnCollisionEnter2D(Collision2D collision) {
        // Checks if the collsion is with an enemy 
        if(collision.gameObject.CompareTag(Structs.Tags.Enemy))
        {
            // Turn on the canvas and turn off the player game object 
            canvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
