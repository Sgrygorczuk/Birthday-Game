using System;


[Serializable]
public abstract class PickUpType 
{
    public virtual string Category => "Default"; // fallback category
}