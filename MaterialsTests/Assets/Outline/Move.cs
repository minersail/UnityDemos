using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos += new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 2, 0, Input.GetAxis("Vertical") * Time.deltaTime * 2);
        transform.position = pos;
	}
}
