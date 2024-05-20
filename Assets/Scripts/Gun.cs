using UnityEngine;
public class Gun : MonoBehaviour
{
    [Header("Camera")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject Impacteffect;
    [Header("Damage")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;
     
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }

    }
    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit,range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target!= null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactzone = Instantiate(Impacteffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactzone,2f);
        }
    }
}
