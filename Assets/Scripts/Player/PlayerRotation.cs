using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Game Object 
    private Camera _camera;
    // Game Object Name 
    private const string CameraName = "Game_Camera";
    public float cameraOffset = 0;

    // Hold position of the mouse 
    private Vector3 _mousePos;

    // Connects to the camera 
    private void Start() {_camera = GameObject.Find(CameraName).GetComponent<Camera>();  }

    private void Update() {
        // Gets the position of the mouse 
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        // Getting the position difference between the mouse and the object 
        Vector3 pos = _mousePos - transform.position;
        // Gets the Z rotation given that position 
        float rotZ = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        // Save that rotation to the player rotation 
        transform.rotation = Quaternion.Euler(0, 0, rotZ - cameraOffset);
    }
}
