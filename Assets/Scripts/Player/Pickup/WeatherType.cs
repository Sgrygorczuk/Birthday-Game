using System;

[Serializable]
public abstract class WeatherType : PickUpType {
    public override string Category => "WeatherType";

    int Duration;
    int Increment;
    int Damage;
}

[Serializable]
public class Snow : WeatherType { }
[Serializable]
public class Heat : WeatherType { }
[Serializable]
public class Rain : WeatherType { }
