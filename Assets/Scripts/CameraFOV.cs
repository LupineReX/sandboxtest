using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    [SerializeField] float startFov;
    [SerializeField] Camera fpscam;
    private float fovSmoothTime = 10f;

    private float targetFov;
    private float wallRunFovDelta = 0;
    void Start()
    {
        startFov = fpscam.fieldOfView;
        targetFov = startFov;
    }

    void Update()
    {
        fpscam.fieldOfView = Mathf.Lerp(fpscam.fieldOfView, targetFov + wallRunFovDelta, fovSmoothTime * Time.deltaTime);
    }

    public void SetFov(float newFov, float fovSmoothTime)
    {
        targetFov = newFov;
        this.fovSmoothTime = fovSmoothTime;
    }

    public void SetWallrunFov(float newFov)
    {
        wallRunFovDelta = newFov;
    }

    public void ResetFov()
    {
        targetFov = startFov;
        fovSmoothTime = 10f;
    }
}
