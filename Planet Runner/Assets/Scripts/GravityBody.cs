using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    [SerializeField] GravityAttractor attractor;

    public GravityAttractor Attractor { get { return attractor; } }

    private void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.useGravity = false;
    }

    // Update is called once per frame
    private void Update()
    {
        attractor.Attract(transform);
    }
}
