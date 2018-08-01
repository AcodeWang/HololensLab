using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class BroadcastReceiver : MonoBehaviour  {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ToggleUserMode()
    {
        foreach (var interactibleGO in GetComponentsInChildren<InteractibleGO>())
        {
            interactibleGO.SetUnvisible();
            interactibleGO.enabled = true;
            if (interactibleGO.GetComponent<BreakerClickAction>() != null)
            {
                interactibleGO.interactibleAction = interactibleGO.GetComponent<BreakerClickAction>();
            }
            else
            {
                interactibleGO.interactibleAction = interactibleGO.gameObject.AddComponent<BreakerClickAction>();
            }
        }
    }

    void ToggleDevelopMode()
    {
        foreach (var interactibleGO in GetComponentsInChildren<InteractibleGO>())
        {
            interactibleGO.enabled = true;
            interactibleGO.SetVisible();
            if (interactibleGO.GetComponent<PlacebleObjectClickAction>() != null)
            {
                interactibleGO.interactibleAction = interactibleGO.GetComponent<PlacebleObjectClickAction>();
            }
        }
    }
}
