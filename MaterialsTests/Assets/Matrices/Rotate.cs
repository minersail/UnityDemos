using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speed = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rot = transform.eulerAngles;

        rot.y += Time.deltaTime * speed;

        transform.eulerAngles = rot;
	}
}
