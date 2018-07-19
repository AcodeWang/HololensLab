using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POI : MonoBehaviour {

    public string modelId;
    public string referenceId;
    public string designation;
    public string info;
    public SDK3DConnectionInfo connectionInfo;
    public Texture image;
    public string designationLabel;
    public GameObject label;
    public GameObject linkedGO;

    public List<POI> linkedPOIs = new List<POI>();

}
