using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastManager : MonoBehaviour {

    public GameObject DOFPanel;

    public GameObject InfoPanel;

    public GameObject TargetObject;

    public GameObject Object;

	// Use this for initialization
	void Start () {
        Object = TargetObject.transform.Find("Object").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UserMode()
    {
        Debug.Log("User mode");

        DOFPanel.SetActive(false);
        InfoPanel.GetComponent<InfoPanelManager>().clickAction = null;

        Object.transform.parent = TargetObject.transform.parent;
        TargetObject.SetActive(false);

        BroadcastMessage("ToggleUserMode", "param");
    }

    public void DevelopMode()
    {
        Debug.Log("Develop mode");

        DOFPanel.SetActive(true);
        InfoPanel.GetComponent<InfoPanelManager>().clickAction = InfoPanel.GetComponent<PlacebleObjectClickAction>();

        TargetObject.SetActive(true);
        Object.transform.parent = TargetObject.transform;

        BroadcastMessage("ToggleDevelopMode");
    }

    public void SendPlacementOK()
    {
        BroadcastMessage("DOFPlacementOK");
    }
}
