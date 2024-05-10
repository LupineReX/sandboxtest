using UnityEngine;
public class Gun : MonoBehaviour
{
    [Header("Camera")]
    public Camera fpsCam;
    [Header("Damage")]
    public float damage = 10f;
    public float range = 100f;

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
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit,range))
        {

        }
    }
}
