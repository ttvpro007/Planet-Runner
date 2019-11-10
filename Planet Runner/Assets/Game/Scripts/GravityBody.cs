using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    [SerializeField] bool _static;
    private GravityAttractor attractor;
    public bool Static { get { return _static; } set { _static = value; } }

    Rigidbody rigidbody;

    private void Start()
    {
        attractor = GravityAttractor.instance;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.useGravity = false;
    }
    
    private void FixedUpdate()
    {
        if (_static)
        {
            rigidbody.isKinematic = true;
            gameObject.isStatic = true;
            return;
        }
        else
        {
            rigidbody.isKinematic = false;
            gameObject.isStatic = false;
        }

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
