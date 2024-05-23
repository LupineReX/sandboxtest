using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    [SerializeField] float startFov;
    [SerializeField] Camera fpscam;
    [SerializeField] Camera gunCam;
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
        float fov = Mathf.Lerp(fpscam.fieldOfView, targetFov + wallRunFovDelta, fovSmoothTime * Time.deltaTime);
        fpscam.fieldOfView = fov;
        gunCam.fieldOfView = fov;
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
