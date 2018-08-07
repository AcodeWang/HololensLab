
[System.Serializable]
public class BonduelleData
{
    public BonduelleGroupData[] bonduelledatas;
}

[System.Serializable]
public class BonduelleGroupData
{
    public string id;
    public BreakerData[] breakers;
}

[System.Serializable]
public class BreakerData
{
    public string id;
    public string level;
    public string description;
    public string connection;
    public string location;
    public string info;
}
