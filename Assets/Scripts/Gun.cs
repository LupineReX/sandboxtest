using UnityEngine;
using System.Collections;
public class Gun : MonoBehaviour
{
    [SerializeField] private bool useScope = false;
    [SerializeField] private Scoped scoped;

    [Header("Camera")]
    [SerializeField] CameraFOV cameraFOV;
    [SerializeField] Camera fpsCam;
    [SerializeField] GameObject weaponCam;
    [Header("Effects")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject Impacteffect;
    [SerializeField] GameObject scopeOverlay;
    [Header("Effects")]
    [SerializeField] float fovSmoothTime = 10f;
    [SerializeField] float scopeFOV = 15f;
    [SerializeField] bool isScoped = false;
    [SerializeField] Vector3 recoil;
    private float normalFOV;
    [Header("Ammo")]
    [SerializeField] int maxAmmo = 10;
    private int currentAmmo;
    [SerializeField] float reloadTime = 1f;
    private bool isReloading = false;
    [Header("Damage")]
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 15f;
    private float nextTimeToFire = 0f;
    [SerializeField] KeyCode reloadKey = KeyCode.R;
    [SerializeField] Animator animator;



    [SerializeField] private Recoil Recoil_Script;
    // Update is called once per frame
    void Start()
    {
        currentAmmo = maxAmmo;
    }
    void OnEnable()
    {
        scoped.enabled = useScope;
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    void Update()
    {
        if (isReloading)
            return;
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
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
        /*
        if (Input.GetButton("Fire2"))
        {
            animator.SetBool("Scoped", true);        
        }
        else
            animator.SetBool("Scoped", false);
        */
    }
    IEnumerator OnScope()
    {
        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(true);
        weaponCam.SetActive(false);
        cameraFOV.SetFov(scopeFOV, fovSmoothTime);
    }
    void OnUnScoped()
    {
        scopeOverlay.SetActive(false);
        weaponCam.SetActive(true);
        cameraFOV.ResetFov();
    }
    void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;
        Recoil_Script.RecoilFire(recoil);
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
