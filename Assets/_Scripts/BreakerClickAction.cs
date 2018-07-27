using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerClickAction : InteractibleAction {

    public InfoPanelManager infoPanelManager;

    // Use this for initialization
    void Start () {
        infoPanelManager = GameObject.Find("InfoPanel").GetComponent<InfoPanelManager>();
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

        infoPanelManager.description.text = this.name;
    }
}
