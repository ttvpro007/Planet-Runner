using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] float radius = 30f;
    [SerializeField] float shrinkingFactor = 0.1f;

    public float Radius { get { return radius; } }

    void Start()
    {
        transform.localScale = Vector3.one * radius;
    }
    
    void FixedUpdate()
    {
        if (radius < 1) return;
        radius -= shrinkingFactor;
        transform.localScale = Vector3.one * radius;
    }
}
