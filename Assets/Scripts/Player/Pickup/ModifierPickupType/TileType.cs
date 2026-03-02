using System;

[Serializable]
public abstract class TileType : ModifierPickUpType
{
    public override string Category => "TileType";
}

[Serializable]
public class Fire : TileType { }
[Serializable]
public class Poison : TileType { }
[Serializable]
public class Spike : TileType { }
[Serializable]
public class Pit : TileType { }
[Serializable]
public class Recover : TileType
{
    public int Duration;
    public int Increment;
    public int Heal;
}
