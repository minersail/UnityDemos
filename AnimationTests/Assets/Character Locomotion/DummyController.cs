using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    Transform target;

	// Use this for initialization
	void Start ()
    {
        GetTarget();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 direction = target.position - transform.position;
        Vector3 localDirection = transform.InverseTransformDirection(direction);
        Vector3 normalized = localDirection.normalized;
        float distance = localDirection.magnitude;

        Animator anim = GetComponent<Animator>();

        if (Vector3.Angle(direction, transform.forward) > 72)
        {
            // Only turn if not already turning
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Turn Right") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Turn Left"))
            {
                Vector3 cross = Vector3.Cross(transform.rotation * Vector3.forward, Quaternion.LookRotation(direction) * Vector3.forward);
                if (cross.y > 0)
                {
                    anim.SetTrigger("TurnRight");
                }
                else
                {
                    anim.SetTrigger("TurnLeft");
                }
            }
        }

        if (distance > 2)
        {
            anim.SetFloat("VelocityX", Mathf.Lerp(anim.GetFloat("VelocityX"), normalized.x, Time.deltaTime));
            anim.SetFloat("VelocityZ", Mathf.Lerp(anim.GetFloat("VelocityZ"), normalized.z, Time.deltaTime));
        }
        else
        {
            anim.SetTrigger("Attack");

            anim.SetFloat("VelocityX", Mathf.Lerp(anim.GetFloat("VelocityX"), 0, Time.deltaTime));
            anim.SetFloat("VelocityZ", Mathf.Lerp(anim.GetFloat("VelocityX"), 0, Time.deltaTime));
        }
    }

    void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
