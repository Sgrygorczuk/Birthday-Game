using System;


[Serializable]
public abstract class StatType : ModifierPickUpType {
    public override string Category => "StatType";
}

[Serializable]
public class Health : StatType { }
[Serializable]
public class Attack : StatType { }
[Serializable]
public class Defense : StatType { }
[Serializable]
public class Speed : StatType { }
[Serializable]
public class FireRate : StatType { }