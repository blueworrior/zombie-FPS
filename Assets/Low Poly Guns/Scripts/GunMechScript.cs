using UnityEngine;
using System.Collections; 

public class GunMechScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    // public float impactForce = 0.2f;
    public float fireRate = 10f;

    public int maxAmmo = 20;
    private int currentAmmo;
    public float reloadTime = 1f;

    public Camera fpscam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    public Animator animator;
  void Start()
  {
    currentAmmo = maxAmmo;
  }
  // Update is called once per frame
  void Update()
    {
        if (isReloading)
        { 
            return;
        }
        if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("rEALOADING....");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
                yield return new WaitForSeconds(0.25f);


        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;
        RaycastHit hit;

        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            TargetEnemy target = hit.transform.GetComponentInParent<TargetEnemy>();
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
