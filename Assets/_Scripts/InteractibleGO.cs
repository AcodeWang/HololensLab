using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class InteractibleGO : MonoBehaviour,IFocusable, IInputClickHandler {

    public Renderer _renderer;

    public Material[] defaultMaterials;

    [SerializeField]
    private InteractibleAction interactibleAction;

    // Use this for initialization
    void Start () {
        _renderer = GetComponent<Renderer>();
        //_renderer.enabled = false;
        defaultMaterials = GetComponent<Renderer>().materials;

        interactibleAction = gameObject.GetComponent<InteractibleAction>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetUnvisible()
    {

        if (_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
        }

        _renderer.enabled = false;
    }

    public void SetVisible()
    {
    
        if (_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
        }

        _renderer.enabled = true;
    }


    public void OnFocusEnter()
    {
        //_renderer.enabled = true;
        //if(GetComponent<cakeslice.Outline>() == null)
        //{
        //    var outline = gameObject.AddComponent<cakeslice.Outline>();
        //}

        //foreach (Material material in defaultMaterials)
        //{
        //    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        //    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //    material.SetInt("_ZWrite", 0);
        //    material.DisableKeyword("_ALPHATEST_ON");
        //    material.DisableKeyword("_ALPHABLEND_ON");
        //    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        //    material.renderQueue = 3000;

        //    material.SetFloat("_Mode", 3f);
        //    material.SetColor("_Color", new Color(material.color.r, material.color.g, material.color.b, 0.2f));
        //}
    }

    public void OnFocusExit()
    {
        if (GetComponent<cakeslice.Outline>() != null)
        {
            Destroy(_renderer.GetComponent<cakeslice.Outline>());
        }
        //_renderer.enabled = false;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {

        if (interactibleAction != null)
        {
            interactibleAction.PerformAction();
        }
    }
}
