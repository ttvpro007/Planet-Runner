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
    
    private void Update()
    {
        attractor.Attract(transform);
    }

    public void SetPlanet(GravityAttractor planet)
    {
        attractor = planet;
    }
}
