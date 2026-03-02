using Pathfinding;
using UnityEngine;

public class KeepRotationStatic : MonoBehaviour {
    private AIPath _aiPath;
    private SpriteRenderer spriteRenderer;
    private void Start() {
        _aiPath = transform.parent.GetComponent<AIPath>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        float directionToDestination = _aiPath.destination.x - transform.position.x;

        spriteRenderer.flipX = directionToDestination switch {
            > 0.1f => true, // Destination is to the Right
            < -0.1f => false, // Destination is to the Left
            _ => spriteRenderer.flipX // Maintain current flip if very close
        };
    }
    
    private void LateUpdate() {
        // Keeps the world rotation at exactly 0,0,0
        // and ignores any rotation passed down from the parent.
        transform.rotation = Quaternion.identity;
    }
}
