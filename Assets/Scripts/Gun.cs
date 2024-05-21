using UnityEngine;
using System.Collections;
public class Gun : MonoBehaviour
{
    [Header("Camera")]
    public Camera fpsCam;
    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject Impacteffect;
    [Header("Ammo")]
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    [Header("Damage")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;
    [SerializeField] KeyCode reloadKey = KeyCode.R;
    public Animator animator;
    // Update is called once per frame
    void Start()
    {
        currentAmmo = maxAmmo;
    }
    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    void Update()
    {
        if (isReloading)
            return;
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetKeyDown(reloadKey) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }


    }
    void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;
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
    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime-.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
