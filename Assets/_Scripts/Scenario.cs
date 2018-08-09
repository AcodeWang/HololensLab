using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour {

    public GameObject navObj;
    public GameObject Outline;
    public GameObject objOnWarning;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartScenerio()
    {
        navObj.SetActive(true);
        Outline.GetComponent<Renderer>().enabled = true;
        objOnWarning.AddComponent<cakeslice.Outline>().color = 1;
        objOnWarning.GetComponent<InteractibleGO>().OnFocusEnter();
    }

    public void StopScenerio()
    {
        navObj.SetActive(false);
        Outline.GetComponent<Renderer>().enabled = false;
    }
}
