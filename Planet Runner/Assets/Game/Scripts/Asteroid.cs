using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(GravityBody))]
public class Asteroid : MonoBehaviour
{
    SoundManager soundManager = SoundManager.instance;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}