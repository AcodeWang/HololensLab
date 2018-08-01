using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class PlacebleObjectClickAction : InteractibleAction {

    [Tooltip("Place parent on tap instead of current game object.")]
    public bool PlaceParentOnTap = true;

    public DOFPanelManager dofPanel;

    [Tooltip("Specify the parent game object to be moved on tap, if the immediate parent is not desired.")]
    public GameObject ParentGameObjectToPlace;



    // Use this for initialization
    void Start () {

        dofPanel = FindObjectOfType<DOFPanelManager>();

        if (PlaceParentOnTap)
        {
            ParentGameObjectToPlace = GetParentToPlace();
        }
        else
        {
            ParentGameObjectToPlace = gameObject;
        }

        if (WorldAnchorManager.Instance != null)
        {
            // Add world anchor when object placement is done.
            WorldAnchorManager.Instance.AttachAnchor(ParentGameObjectToPlace);
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    public override void PerformAction()
    {

        dofPanel.m_original = ParentGameObjectToPlace;

        dofPanel.IsBeingPlaced = !dofPanel.IsBeingPlaced;
        dofPanel.HandlePlacement();
    }

    /// <summary>
    /// Returns the predefined GameObject or the immediate parent when it exists
    /// </summary>
    /// <returns></returns>
    public GameObject GetParentToPlace()
    {
        if (ParentGameObjectToPlace)
        {
            return ParentGameObjectToPlace;
        }

        return gameObject.transform.parent ? gameObject.transform.parent.gameObject : null;
    }
}
