using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    [SerializeField] bool Static;
    private GravityAttractor attractor;

    private void Start()
    {
        attractor = GravityAttractor.instance;

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.useGravity = false;
        if (Static) rigidbody.isKinematic = true;
    }
    
    private void FixedUpdate()
    {
        if (Static) return;

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
