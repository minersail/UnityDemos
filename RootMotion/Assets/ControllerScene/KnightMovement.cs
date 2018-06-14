using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();	
	}
	
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("TurnLeft");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("TurnRight");
        }

        anim.SetFloat("VelocityX", Input.GetAxis("Horizontal"));
        anim.SetFloat("VelocityZ", Input.GetAxis("Vertical"));
    }
}
