using UnityEngine;

public class GunMechScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    // public float impactForce = 0.2f;
    public float fireRate = 10f;

    public Camera fpscam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;

        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            TargetEnemy target = hit.transform.GetComponent<TargetEnemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // if (hit.rigidbody != null)
            // {
            //     hit.rigidbody.AddForce(-hit.normal * impactForce);
            // }

            GameObject ImpactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            ImpactGO.transform.parent = hit.transform;
        }
    }
}
