using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] float radius = 30f;
    [SerializeField] float shrinkingFactor = 0.1f;

    float time = Mathf.Infinity;

    void Start()
    {
        transform.localScale = Vector3.one * radius;
    }
    
    void FixedUpdate()
    {
        if (radius < 1) return;
        radius -= shrinkingFactor;
        transform.localScale = Vector3.one * (radius);
    }
}
