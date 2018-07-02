using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttacking : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartAttack()
    {
        GetComponentInChildren<Sword>().gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    void EndAttack()
    {
        GetComponentInChildren<Sword>().gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }
}
