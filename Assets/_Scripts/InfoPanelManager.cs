using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelManager : MonoBehaviour, IInputClickHandler
{


    public Animator screwAnimator;

    private static InfoPanelManager instance;

    public static InfoPanelManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameObject LastBreaker;
    public GameObject Breaker;

    public GameObject icon;
    public Texture[] iconImages;

    public InteractibleAction clickAction;

    public Text id;
    public Text level;
    public Text description;
    public Text location;
    public Text connection;
    public Text info;

    public RawImage map;

    // Use this for initialization
    void Awake () {
        instance = FindObjectOfType<InfoPanelManager>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = Mathf.Clamp01(Vector3.Distance(transform.position, Camera.main.transform.position) / 2) * Vector3.one;
        
        if(Breaker != null)
        {
            if(LastBreaker == null)
            {
                LastBreaker = Breaker;
                Breaker.GetComponent<InteractibleGO>().FocusedInfoPanel(Breaker.GetComponent<BreakerClickAction>().m_data);
            }

            if(Breaker != LastBreaker)
            {
                LastBreaker.GetComponent<InteractibleGO>().UnFocusedInfoPanel();
                Breaker.GetComponent<InteractibleGO>().FocusedInfoPanel(Breaker.GetComponent<BreakerClickAction>().m_data);
                LastBreaker = Breaker;
            }
        }
    }

    public void RepairButtonClick()
    {
        screwAnimator.SetTrigger("ScrewTrigger");
    }

    public void InfoButtonClick()
    {
        info.gameObject.transform.parent.gameObject.SetActive(!info.gameObject.transform.parent.gameObject.activeSelf);
    }

    public void MapButtonClick()
    {
        map.gameObject.transform.parent.gameObject.SetActive(!map.gameObject.transform.parent.gameObject.activeSelf);
    }

    public void DisableInfoPanel()
    {
        LastBreaker.GetComponent<InteractibleGO>().UnFocusedInfoPanel();
        Breaker.GetComponent<InteractibleGO>().UnFocusedInfoPanel();
        LastBreaker = null;
        Breaker = null;
        gameObject.SetActive(false);
    }

    public void DisableMapPanel()
    {
        map.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void IconClick()
    {
        if(Breaker.GetComponent<InteractibleGO>().isOn)
        {
            Breaker.GetComponent<InteractibleGO>().isOn = false;
            icon.GetComponent<RawImage>().texture = iconImages[1];
            Breaker.GetComponent<cakeslice.Outline>().color = 1;
            Breaker.GetComponent<InteractibleGO>().conncetionLine.GetComponent<cakeslice.Outline>().color = 1;
        }
        else
        {
            Breaker.GetComponent<InteractibleGO>().isOn = true;
            icon.GetComponent<RawImage>().texture = iconImages[0];
            Breaker.GetComponent<cakeslice.Outline>().color = 2;
            Breaker.GetComponent<InteractibleGO>().conncetionLine.GetComponent<cakeslice.Outline>().color = 2;
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        clickAction.PerformAction();
    }
}
