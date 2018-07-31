using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UserMode()
    {
        Debug.Log("User mode");
        BroadcastMessage("ToggleUserMode", "param");
    }

    public void DevelopMode()
    {
        Debug.Log("Develop mode");
        BroadcastMessage("ToggleDevelopMode");
    }
}
