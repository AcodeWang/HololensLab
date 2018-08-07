using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerClickAction : InteractibleAction {

    public GameObject infoPanel;
    public BreakerData m_data;

    // Use this for initialization
    void Start () {

        foreach (var bonduelledata in FindObjectOfType<DataLoader>().bonduelleData.bonduelledatas)
        {
            if(bonduelledata.id == gameObject.transform.parent.name)
            {
                foreach(var breakerdata in bonduelledata.breakers)
                {
                    if(name == breakerdata.id)
                    {
                        m_data = breakerdata;
                        break;
                    }
                }
                break;
            }
        }

        infoPanel = GameObject.Find("InfoPanel");

        //PerformAction();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PerformAction()
    {
        for (int i = 0; i < this.GetComponent<InteractibleGO>().defaultMaterials.Length; i++)
        {
            // 2.d: Uncomment the below line to highlight the material when gaze enters.
            this.GetComponent<InteractibleGO>().defaultMaterials[i].color = Color.red;
        }

        Debug.Log("Clicked");

        if (m_data == null)
            return;

        infoPanel.SetActive(true);

        InfoPanelManager.Instance.description.text = m_data.id + "\n" + m_data.description;
        InfoPanelManager.Instance.level.text = "Level " + m_data.level;
        InfoPanelManager.Instance.location.text = m_data.location;
        InfoPanelManager.Instance.connection.text = m_data.connection;
        InfoPanelManager.Instance.info.text = m_data.info;

        InfoPanelManager.Instance.map.transform.parent.gameObject.SetActive(false);
    }
}
