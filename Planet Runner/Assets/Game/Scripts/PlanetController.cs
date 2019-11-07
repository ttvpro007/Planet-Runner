using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] float radius = 30f;
    [SerializeField] float minimumSize = 10f;
    [SerializeField] float shrinkingFactor = 0.1f;

    public float Radius { get { return radius; } }

    void Start()
    {
        transform.localScale = Vector3.one * radius;
    }
    
    void FixedUpdate()
    {
        if (radius < minimumSize) return;
        radius -= shrinkingFactor;
        transform.localScale = Vector3.one * radius;
    }
}
