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
        gameObject.SetActive(false);
    }

    public void DisableMapPanel()
    {
        map.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        clickAction.PerformAction();
    }
}
