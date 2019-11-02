using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    [SerializeField] float gravity = -10f;
    [SerializeField] float smoothFactor = 50f;

    public float Gravity { get { return gravity; } }

    public void Attract(Transform body)
    {
        Vector3 up = (body.position - transform.position).normalized;
        Vector3 localUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(up * gravity);

        Quaternion rotation = Quaternion.FromToRotation(localUp, up) * body.rotation;

        body.rotation = Quaternion.Slerp(body.rotation, rotation, smoothFactor * Time.deltaTime);
    }
}
