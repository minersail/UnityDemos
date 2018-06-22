using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    GameObject target;

	// Use this for initialization
	void Start ()
    {
        FindTarget();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (target == null) return;

        Vector3 direction = target.transform.position - transform.position;
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
                    anim.ResetTrigger("TurnLeft");
                }
                else
                {
                    anim.SetTrigger("TurnLeft");
                    anim.ResetTrigger("TurnRight");
                }
            }
        }
        else
        {
            anim.ResetTrigger("TurnRight");
            anim.ResetTrigger("TurnLeft");
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
            anim.SetFloat("VelocityZ", Mathf.Lerp(anim.GetFloat("VelocityZ"), 0, Time.deltaTime));
        }

        if (target.GetComponent<Health>().dead)
        {
            FindTarget();
        }
    }

    void FindTarget()
    {
        float min = float.MaxValue;

        foreach (GameObject knight in GameObject.FindGameObjectsWithTag("Knight"))
        {
            Team otherTeam = knight.GetComponent<Team>();
            Team thisTeam = GetComponent<Team>();
            Health health = knight.GetComponent<Health>();

            if (otherTeam.team != thisTeam.team && !health.dead)
            {
                if (Vector3.Distance(knight.transform.position, transform.position) < min)
                {
                    target = knight;
                    min = Vector3.Distance(knight.transform.position, transform.position);
                }
            }
        }

        if (min == float.MaxValue)
        {
            target = null;

            Animator anim = GetComponent<Animator>();

            anim.SetFloat("VelocityX", 0);
            anim.SetFloat("VelocityZ", 0);
        }
    }

    public void SetTarget(GameObject go)
    {
        target = go;
    }
}
