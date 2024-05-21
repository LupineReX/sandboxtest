using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoped : MonoBehaviour
{
    [SerializeField] CameraFOV cameraFOV;
    [SerializeField] float fovSmoothTime = 10f;
    [SerializeField] Animator animator;
    [SerializeField] GameObject scopeOverlay;
    [SerializeField] bool isScoped = false;
    [SerializeField] GameObject weaponCamera;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject reticle;

    [SerializeField] float scopedFOV = 15f;
    private float normalFOV;
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);

            if (isScoped)
                StartCoroutine(OnScoped());
            else
                OnUnscoped();
        }

    }

    void OnDisable()
    {
        OnUnscoped();
        isScoped = false;
        animator.SetBool("Scoped", isScoped);
    }

    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        reticle.SetActive(true);
        cameraFOV.ResetFov();
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);

        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
        reticle.SetActive(false);

        normalFOV = mainCamera.fieldOfView;
     
        cameraFOV.SetFov(scopedFOV, fovSmoothTime);
    }
}
