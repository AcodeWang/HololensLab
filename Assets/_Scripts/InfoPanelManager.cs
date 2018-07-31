using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelManager : MonoBehaviour {

    public Text id;
    public RawImage image;
    public Text description;

    // Use this for initialization
    void Start () {

        id = transform.Find("Content").Find("ID").GetComponent<Text>();
        image = transform.Find("Content").Find("Image").GetComponent<RawImage>();
        description = transform.Find("Content").Find("Description").Find("Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
