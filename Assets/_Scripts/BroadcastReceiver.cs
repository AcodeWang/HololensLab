using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class BroadcastReceiver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ToggleUserMode()
    {
        this.GetComponent<TapToPlace>().enabled = false;

        foreach (var interactibleGO in GetComponentsInChildren<InteractibleGO>())
        {
            interactibleGO.enabled = true;
            interactibleGO.SetUnvisible();
        }
    }

    void ToggleDevelopMode()
    {
        this.GetComponent<TapToPlace>().enabled = true;

        foreach (var interactibleGO in GetComponentsInChildren<InteractibleGO>())
        {
            interactibleGO.enabled = true;
            interactibleGO.SetVisible();
        }
    }
}
