using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakerClickAction : InteractibleAction {

    public GameObject infoPanel;
    public BreakerData m_data;

    // Use this for initialization
    void Start () {

        foreach (var bonduelledata in FindObjectOfType<DataLoader>().bonduelleData.bonduelledatas)
        {
            if(bonduelledata.id + "F" == gameObject.transform.parent.name)
            {
                foreach(var breakerdata in bonduelledata.breakers)
                {
                    if(name == breakerdata.id)
                    {
                        m_data = breakerdata;
                        m_data.upstream = bonduelledata.id;
                        break;
                    }
                }
                break;
            }
        }

        

        //PerformAction();
    }
	
	// Update is called once per frame
	void Update () {
		if(infoPanel == null)
        {
            infoPanel = FindObjectOfType<BroadcastManager>().InfoPanel;
        }
	}

    public override void PerformAction()
    {
        Debug.Log("Clicked");

        if (m_data == null)
            return;

        infoPanel.SetActive(true);

        InfoPanelManager.Instance.Breaker = gameObject;

        InfoPanelManager.Instance.description.text = m_data.id + "\n" + m_data.description;
        InfoPanelManager.Instance.level.text = "Level " + m_data.level;
        InfoPanelManager.Instance.location.text = m_data.location;
        InfoPanelManager.Instance.connection.text = m_data.connection;
        InfoPanelManager.Instance.info.text = m_data.info;

        InfoPanelManager.Instance.map.transform.parent.gameObject.SetActive(false);

        if (GetComponent<InteractibleGO>().isOn)
        {
            InfoPanelManager.Instance.icon.GetComponent<RawImage>().texture = InfoPanelManager.Instance.iconImages[0];
        }
        else
        {
            InfoPanelManager.Instance.icon.GetComponent<RawImage>().texture = InfoPanelManager.Instance.iconImages[1];
        }
    }
}
