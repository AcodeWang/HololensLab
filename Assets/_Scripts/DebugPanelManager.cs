using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPanelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ToggleUserMode()
    {
        GetComponent<HoloToolkit.Unity.Tagalong>().enabled = false;

        foreach(var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }

    }

    void ToggleDevelopMode()
    {
        GetComponent<HoloToolkit.Unity.Tagalong>().enabled = true;

        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = true;
        }
    }
}
