using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerClickAction : InteractibleAction {

    public BreakerData m_data;

    // Use this for initialization
    void Start () {

        foreach (var breaker in FindObjectOfType<DataLoader>().bonduelleData.breakers)
        {
            if(breaker.id == gameObject.name)
            {
                m_data = breaker;
            }
        }
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


        InfoPanelManager.Instance.description.text = m_data.info;
    }
}
