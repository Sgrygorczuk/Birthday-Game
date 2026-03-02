using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigRigidbody2D;
    private PlayerDialogue _playerDialogue;
    private float _xVelocity = 0f;
    private float _yVelocity = 0f;
    public float speed = 3;
    public bool isInArena = false;
    public GameObject spriteRender;

    public float timeLeft = 10.0f;
    
    // Start is called before the first frame update
    private void Start() {
        _rigRigidbody2D = GetComponent<Rigidbody2D>();
        _playerDialogue = GetComponent<PlayerDialogue>();    
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (isInArena) {
            if (_playerDialogue.IsSpeaking()) {
                _xVelocity = 0;
                _yVelocity = 0;
            }
            else {
                _xVelocity = Input.GetAxis(Structs.Input.horizontal);
                _yVelocity = Input.GetAxis(Structs.Input.vertical);
            }   
        }
        else {
            _xVelocity = Input.GetAxis(Structs.Input.horizontal);
            _yVelocity = Input.GetAxis(Structs.Input.vertical);
        }
        
        _rigRigidbody2D.velocity = new Vector2(_xVelocity, _yVelocity) * speed; 
    
        HandleRotation(_xVelocity, _yVelocity);
    }

private void HandleRotation(float h, float v)
{
// Diagonal Up-Left
    if (v > 0 && h < 0) transform.eulerAngles = new Vector3(0, 0, 225);
    // Diagonal Up-Right
    else if (v > 0 && h > 0) transform.eulerAngles = new Vector3(0, 0, 135);
    // Diagonal Down-Left
    else if (v < 0 && h < 0) transform.eulerAngles = new Vector3(0, 0, 315);
    // Diagonal Down-Right
    else if (v < 0 && h > 0) transform.eulerAngles = new Vector3(0, 0, 45);
    
    // Straight Up
    else if (v > 0) transform.eulerAngles = new Vector3(0, 0, 180);
    // Straight Down
    else if (v < 0) transform.eulerAngles = new Vector3(0, 0, 0);
    // Straight Left
    else if (h < 0) transform.eulerAngles = new Vector3(0, 0, 270);
    // Straight Right
    else if (h > 0) transform.eulerAngles = new Vector3(0, 0, 90);
    else {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}

    public void SpeedMul(float mul) {
        StartCoroutine(SpeedMulCo(mul));
    }
    
    private IEnumerator SpeedMulCo(float mul) {
        float ogSpeed = speed;
        speed *= mul;

        // Pause execution for 2 seconds
        yield return new WaitForSeconds(2.0f);

        speed = ogSpeed;    
    }
}
