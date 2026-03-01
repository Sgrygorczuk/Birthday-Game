using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPickUp))]
public class PlayerPickUp : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private float speedMul; 
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = transform.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "PickUp") {
            float mul = 0;
            if (collision.transform.GetComponent<PickUpData>() != null) {
                mul = collision.transform.GetComponent<PickUpData>().Mod;
                playerMovement.SpeedMul(mul);
            }

            Destroy(collision.gameObject);
        }
    }
}
