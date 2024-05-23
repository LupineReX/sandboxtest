using UnityEngine;

public class Recoil : MonoBehaviour
{
    [Header("Rotations")]
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    [Header("Hipfire Recoil")]
    [SerializeField] private Vector3 recoil;
    [Header("Recoil Settings")]
    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    // Update is called once per frame
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire(Vector3 recoil)
    {
        targetRotation += new Vector3(-recoil.y, Random.Range(-recoil.x, recoil.x), Random.Range(-recoil.z, recoil.z));
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(-recoil.y, Random.Range(-recoil.x, recoil.x), Random.Range(-recoil.z, recoil.z));
    }
}
