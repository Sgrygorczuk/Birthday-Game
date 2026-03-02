using System;

[Serializable]
public abstract class EffectType : PickUpType 
{
    public override string Category => "EffectType";
}

[Serializable]
public class Dash : EffectType { }
[Serializable]
public class Shield : EffectType { }
[Serializable]
public class Heal : EffectType { }
[Serializable]
public class Invincibility : EffectType { }
