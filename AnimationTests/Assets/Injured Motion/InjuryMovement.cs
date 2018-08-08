using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryMovement : MonoBehaviour
{
    Animator anim;

    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        anim.SetFloat("Velocity", Mathf.Lerp(anim.GetFloat("Velocity"), Input.GetAxis("Vertical"), Time.deltaTime * 3));

        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetBool("Left", !anim.GetBool("Left"));
	}
}
