using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : Photon.MonoBehaviour
{
    Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();	
	}
	
	void Update ()
    {
        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("TurnLeft");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("TurnRight");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Attack");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("JumpAttack");
        }

        anim.SetBool("Blocking", Input.GetKey(KeyCode.LeftShift));        
        anim.SetFloat("VelocityX", Input.GetAxis("Horizontal"));
        anim.SetFloat("VelocityZ", Input.GetAxis("Vertical"));
    }
}
