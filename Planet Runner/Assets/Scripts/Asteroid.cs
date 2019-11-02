using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(GravityBody))]
public class Asteroid : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10);
    }
}