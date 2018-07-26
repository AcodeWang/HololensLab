using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBox1 : MonoBehaviour {

    public Transform box2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = box2.position + new Vector3(-0.02f, 0.15f, 0);
        transform.rotation = box2.rotation;
	}
}
