using System.Collections;
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
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
                {
                    anim.SetTrigger("Hit");
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
