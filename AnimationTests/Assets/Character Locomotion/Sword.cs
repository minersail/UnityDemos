﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage = 10;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != gameObject.layer)
        {
            if (other.GetComponentInParent<Animator>() != null)
            {
                Animator anim = other.GetComponentInParent<Animator>();
                Health health = other.GetComponentInParent<Health>();

                if (health.health > damage) // Non-Fatal
                {
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) // Don't trigger duplicate hits
                        anim.SetTrigger("Hit");

                    if (other.GetComponentInParent<DummyController>() != null)
                    {
                        // Draw aggro
                        other.GetComponentInParent<DummyController>().SetTarget(transform.root.gameObject);
                    }
                }
            }
            
            if (other.GetComponentInParent<Health>() != null)
            {
                if (other.GetComponentInParent<Animator>().GetBool("Blocking"))
                {
                    other.GetComponentInParent<Health>().Damage(damage / 4);
                }
                else
                {
                    other.GetComponentInParent<Health>().Damage(damage);
                }
            }
        }
    }
}
