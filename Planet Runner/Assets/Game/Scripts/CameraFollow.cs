using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothFactor = 15f;

    Quaternion rotation;
    Health healthComp;

    private void Start()
    {
        StartCoroutine(GetHealthCompponent());
    }

    private void FixedUpdate()
    {
        if (healthComp.CurrentHealth != 0)
        {
            transform.position = Vector3.Lerp(transform.position, GetCameraPosition(), smoothFactor * Time.fixedDeltaTime);

            // global rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, smoothFactor * Time.fixedDeltaTime);

            // look rotation
            rotation = Quaternion.FromToRotation(transform.forward, target.position - transform.position) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothFactor * Time.fixedDeltaTime);
        }
        else
        {
            PanningOut();
        }
    }

    IEnumerator GetHealthCompponent()
    {
        while (!healthComp)
        {
            healthComp = target.GetComponentInParent<Health>();
            yield return null;
        }
    }

    private Vector3 GetCameraPosition()
    {
        Vector3 position;

        position = target.position + target.right * offset.x + target.up * offset.y + target.forward * offset.z;

        return position;
    }

    private void PanningOut()
    {
        Vector3 direction = (transform.position - target.position).normalized;
        transform.Translate(direction * Time.deltaTime);
    }
}
