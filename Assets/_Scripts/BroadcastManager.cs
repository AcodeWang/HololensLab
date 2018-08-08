using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastManager : MonoBehaviour {

    public GameObject DOFPanel;

    public GameObject InfoPanel;
    public GameObject WarningPanel;

    public GameObject TargetObject;

    public GameObject Object;
    public GameObject[] repairObjects;

	// Use this for initialization
	void Start () {
        Object = TargetObject.transform.Find("Object").gameObject;
        UserMode();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UserMode()
    {
        Debug.Log("User mode");

        DOFPanel.SetActive(false);
        InfoPanel.GetComponent<InfoPanelManager>().clickAction = null;
        InfoPanel.SetActive(false);

        WarningPanel.GetComponent<HoloToolkit.Unity.SpatialMapping.TapToPlace>().enabled = false;

        Object.transform.parent = TargetObject.transform.parent;
        TargetObject.SetActive(false);

        foreach (var obj in repairObjects)
        {
            obj.SetActive(false);
        }

        BroadcastMessage("ToggleUserMode", "param");
    }

    public void DevelopMode()
    {
        Debug.Log("Develop mode");

        DOFPanel.SetActive(true);
        InfoPanel.SetActive(true);
        InfoPanel.GetComponent<InfoPanelManager>().clickAction = InfoPanel.GetComponent<PlacebleObjectClickAction>();

        WarningPanel.GetComponent<HoloToolkit.Unity.SpatialMapping.TapToPlace>().enabled = true;

        TargetObject.SetActive(true);
        Object.transform.parent = TargetObject.transform;

        foreach (var obj in repairObjects)
        {
            obj.SetActive(false);
        }

        BroadcastMessage("ToggleDevelopMode");
    }

    public void SendPlacementOK()
    {
        BroadcastMessage("DOFPlacementOK");
    }
}
