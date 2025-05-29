using UnityEngine;

public class GunMechScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpscam;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
