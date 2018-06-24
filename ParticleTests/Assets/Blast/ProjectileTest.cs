using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTest : MonoBehaviour
{
    public GameObject projectile;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(ProjectileSpawner());
    }

    IEnumerator ProjectileSpawner()
    {
        while (Application.isPlaying)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(.2f);
        }
    }

    void SpawnProjectile()
    {
        if (projectile != null)
        {
            Vector3 direction = new Vector3(Random.value, Random.value, Random.value);

            GameObject proj = Instantiate(projectile, transform.position, Quaternion.Euler(direction * 360));
            proj.GetComponent<Rigidbody>().AddForce(proj.transform.forward * 10, ForceMode.Impulse);
        }
    }
}
