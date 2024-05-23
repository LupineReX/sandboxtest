using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [Header("References")]

    [SerializeField] Gun gunScript;
    [SerializeField] Rigidbody rb;
    [SerializeField] BoxCollider coll;
    [SerializeField] Transform player, gunContainer, fpsCam;

    [Header("Pickup Settings")]
    [SerializeField] float pickUpRange;
    [SerializeField] float dropForwardForce, dropUpwardForce;

    [SerializeField] bool equipped;

    private void Start()
    {
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
        }
    }
    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E)) PickUp();

        if (equipped && Input.GetKeyDown(KeyCode.F)) Drop();
    }
    private void PickUp()
    {
        equipped = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;

        gunScript.enabled = true;
    }
    private void Drop()
    {
        equipped = false;

        transform.SetParent(null);
        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);

        rb.AddTorque(new Vector3(random, random, random) * 10);
        gunScript.enabled = false;
    }
}