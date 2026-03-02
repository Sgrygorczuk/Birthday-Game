using System;
using UnityEngine;

[Serializable]
public abstract class ModifierPickUpType : PickUpType
{
    [SerializeReference]
    [SubclassSelector]
    public ModifierType ModifierType;

    public float CalculateStatChange(float baseStat) => ModifierType switch
    {
        Fixed => ModifierType.Amount,
        Additive => baseStat + ModifierType.Amount,
        Multiplicative => (baseStat * ModifierType.Amount),
        _ => baseStat // fallback/default case
    };
}

[Serializable]
public abstract class ModifierType
{
    public float Amount = 1f;
}

[Serializable]
public class Fixed : ModifierType { }
[Serializable]
public class Additive : ModifierType { }
[Serializable]
public class Multiplicative : ModifierType { }