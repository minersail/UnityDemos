using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOffHelmet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Health health = GetComponent<Health>();

        if (health.health <= 0.2 * health.maxHealth)
        {
            transform.Find("Paladin_J_Nordstrom_Helmet").GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
	}
}
