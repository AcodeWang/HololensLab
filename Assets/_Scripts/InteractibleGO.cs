using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class InteractibleGO : MonoBehaviour,IFocusable, IInputClickHandler {

    public Material[] defaultMaterials;

    [SerializeField]
    private InteractibleAction interactibleAction;

    // Use this for initialization
    void Start () {
        defaultMaterials = GetComponent<Renderer>().materials;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnFocusEnter()
    {
        for (int i = 0; i < defaultMaterials.Length; i++)
        {
            // 2.d: Uncomment the below line to highlight the material when gaze enters.
            defaultMaterials[i].color = Color.blue;
        }
    }

    public void OnFocusExit()
    {
        for (int i = 0; i < defaultMaterials.Length; i++)
        {
            // 2.d: Uncomment the below line to remove highlight on material when gaze exits.
            defaultMaterials[i].color = Color.white;
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {

        if (interactibleAction != null)
        {
            interactibleAction.PerformAction();
        }
    }
}
