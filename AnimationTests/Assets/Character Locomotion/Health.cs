using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 50;

    [HideInInspector]
    public float health;
    [HideInInspector]
    public bool dead;

    private float cooldown;

	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        cooldown += Time.deltaTime;
	}

    public void Damage(float amount)
    {
        if (dead || cooldown < 0.5) return;

        health -= amount;
        cooldown = 0;

        if (health <= 0)
        { 
            dead = true;

            foreach (CapsuleCollider cc in GetComponentsInChildren<CapsuleCollider>())
            {
                cc.enabled = false;
            }

            GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
