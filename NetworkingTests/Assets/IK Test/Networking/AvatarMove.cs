using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMove : Photon.MonoBehaviour
{
	void Start () {
		
	}

    void Update ()
    {
        if (!photonView.isMine)
        {
            return;
        }

        Vector3 pos = transform.Find("Player").transform.position;
        pos += new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 5, 0, Input.GetAxis("Vertical") * Time.deltaTime * 5);
        transform.Find("Player").transform.position = pos;
	}
}
