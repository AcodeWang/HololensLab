using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using TMPro.Examples;

public enum BreakerStatus { User, Develop, Infopanel };

public class InteractibleGO : MonoBehaviour,IFocusable, IInputClickHandler {

    public GameObject textPrefab;

    private GameObject m_text;

    public GameObject upstream;
    public GameObject conncetionLine;

    public Renderer _renderer;

    public Material[] defaultMaterials;

    [SerializeField]
    public InteractibleAction interactibleAction;

    public BreakerStatus m_status = BreakerStatus.User;

    public bool isOn = true;

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
        m_status = BreakerStatus.User;
    }

    public void SetVisible()
    {
    
        if (_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
        }

        _renderer.enabled = true;
        m_status = BreakerStatus.Develop;
    }

    public void FocusedInfoPanel(BreakerData data = null)
    {
        m_status = BreakerStatus.Infopanel;

        _renderer.enabled = true;
        if (GetComponent<cakeslice.Outline>() == null)
        {
            var outline = gameObject.AddComponent<cakeslice.Outline>();
        }

        if (isOn)
        {
            GetComponent<cakeslice.Outline>().color = 2;
        }
        else
        {
            GetComponent<cakeslice.Outline>().color = 1;
        }

        if(upstream == null)
        {
            upstream = GameObject.Find(data.upstream);
            conncetionLine = GameObject.CreatePrimitive(PrimitiveType.Cube);
            conncetionLine.transform.parent = gameObject.transform;
            conncetionLine.transform.position = (transform.position + upstream.transform.position) / 2;
            conncetionLine.transform.rotation = transform.rotation;
            conncetionLine.transform.localScale = new Vector3(Vector3.Distance(upstream.transform.position, transform.position) * 1000f, 2f, 2f);
            conncetionLine.AddComponent<cakeslice.Outline>().color = 2;
        }
        else
        {
            if (isOn)
            {
                conncetionLine.AddComponent<cakeslice.Outline>().color = 2;
            }
            else
            {
                conncetionLine.AddComponent<cakeslice.Outline>().color = 1;
            }
        }

        if(conncetionLine != null)
        {
            conncetionLine.SetActive(true);
        }

        var upInteractible = upstream.GetComponent<InteractibleGO>();

        if (upInteractible != null)
        {
            upInteractible._renderer.enabled = true;
            if (upstream.GetComponent<cakeslice.Outline>() == null)
            {
                var outline = upInteractible.gameObject.AddComponent<cakeslice.Outline>();
            }

            upInteractible.GetComponent<cakeslice.Outline>().color = 2;

            upInteractible.m_status = BreakerStatus.Infopanel;
        }

        if(m_text == null)
        {
            m_text = GameObject.Instantiate(textPrefab);
            m_text.transform.parent = transform;
            m_text.transform.localPosition = new Vector3(10, 100, 0);
            m_text.transform.rotation = Quaternion.LookRotation(-transform.forward, transform.up); 
            m_text.GetComponent<TMPro.TextMeshPro>().text = data.description;
        }
        else
        {
            m_text.SetActive(true);
        }

    }

    public void UnFocusedInfoPanel()
    {
        m_status = BreakerStatus.User;

        if(m_text != null)
        {
            m_text.SetActive(false);
        }

        if(conncetionLine != null)
        {
            Destroy(conncetionLine.GetComponent<cakeslice.Outline>());
            conncetionLine.SetActive(false);
        }

        var upInteractible = upstream.GetComponent<InteractibleGO>();
        upInteractible.m_status = BreakerStatus.User;
        upInteractible.OnFocusEnter();
        upInteractible.OnFocusExit();

        OnFocusEnter();
        OnFocusExit();
    }


    public void OnFocusEnter()
    {
        if (m_status != BreakerStatus.User)
        {
            return;
        }

        _renderer.enabled = true;
        if (GetComponent<cakeslice.Outline>() == null)
        {
            var outline = gameObject.AddComponent<cakeslice.Outline>();
        }

        foreach (Material material in defaultMaterials)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;

            material.SetFloat("_Mode", 3f);
            material.SetColor("_Color", new Color(material.color.r, material.color.g, material.color.b, 0.4f));
        }
    }

    public void OnFocusExit()
    {
        if (m_status != BreakerStatus.User)
        {
            return;
        }

        if (GetComponent<cakeslice.Outline>() != null)
        {
            Destroy(_renderer.GetComponent<cakeslice.Outline>());
        }
        _renderer.enabled = false;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {

        if (interactibleAction != null)
        {
            interactibleAction.PerformAction();
        }
    }
}
