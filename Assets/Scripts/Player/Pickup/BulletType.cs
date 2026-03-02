using System;

[Serializable]
public class BulletType : PickUpType 
{
    public override string Category => "BulletType";
}

[Serializable]
public class Spread : BulletType { }
[Serializable]
public class Pierce : BulletType { }
[Serializable]
public class Explode : BulletType { }
[Serializable]
public class Homing : BulletType { }