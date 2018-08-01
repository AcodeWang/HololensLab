using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelManager : MonoBehaviour {

    private static InfoPanelManager instance;

    public static InfoPanelManager Instance
    {
        get
        {
            return instance;
        }
    }

    public Text id;
    public RawImage image;
    public Text description;

    // Use this for initialization
    void Awake () {

        instance = FindObjectOfType<InfoPanelManager>();

        id = transform.Find("Content").Find("ID").GetComponent<Text>();
        image = transform.Find("Content").Find("Image").GetComponent<RawImage>();
        description = transform.Find("Content").Find("Description").Find("Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
