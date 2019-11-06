using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    private GravityAttractor attractor;

    public GravityAttractor Attractor { get { return attractor; } }
    
    private void Start()
    {
        attractor = GravityAttractor.instance;

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.useGravity = false;
    }
    
    private void FixedUpdate()
    {
        attractor.Attract(this);
    }
    
    public Vector3 Up()
    { 
        return (transform.position - attractor.transform.position).normalized;
    }
    
    public Vector3 LocalUp()
    {
        return transform.TransformDirection(Vector3.up);
    }
}
