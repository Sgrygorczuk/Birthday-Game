using UnityEngine;

public class PickUpData : MonoBehaviour
{
    [SerializeReference]
    [SubclassSelector]
    public PickUpType pickUpType;
}
