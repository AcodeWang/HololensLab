
[System.Serializable]
public class BonduelleData
{
    public BreakerData[] breakers;
}

[System.Serializable]
public class BreakerData
{
    public string id;
    public string line;
    public string description;
    public string info;
}
