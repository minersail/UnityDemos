using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMove : MonoBehaviour
{
	void Start () {
		
	}

    void Update ()
    {
        Vector3 pos = transform.position;
        pos += new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 5, 0, Input.GetAxis("Vertical") * Time.deltaTime * 5);
        transform.position = pos;
	}
}
