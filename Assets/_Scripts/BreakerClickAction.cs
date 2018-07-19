using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerClickAction : InteractibleAction {

    public GameObject infoPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PerformAction()
    {
        for (int i = 0; i < this.GetComponent<InteractibleGO>().defaultMaterials.Length; i++)
        {
            // 2.d: Uncomment the below line to highlight the material when gaze enters.
            this.GetComponent<InteractibleGO>().defaultMaterials[i].color = Color.red;
        }

        infoPanel.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
    }
}
