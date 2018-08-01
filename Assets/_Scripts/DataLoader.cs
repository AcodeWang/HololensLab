using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

    [SerializeField]
    public BonduelleData bonduelleData;

    public 
	// Use this for initialization
	void Awake () {

        string filePath = "Models/Json/Bonduelle";

        TextAsset jsonFile = Resources.Load(filePath) as TextAsset;

        string jsonString = jsonFile.ToString();

        bonduelleData = JsonUtility.FromJson<BonduelleData>(jsonString);
    }
	
}
