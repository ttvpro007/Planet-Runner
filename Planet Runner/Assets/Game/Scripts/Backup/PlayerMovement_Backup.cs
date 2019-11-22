using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement_Backup : MonoBehaviour
{
    [SerializeField] float startSpeed = 15f;
    [SerializeField] float maxJumpForce = 50f;
    [SerializeField] float turnSpeed = 3f;
    [SerializeField] float flyTimeLimiter = 5f;
    [SerializeField] bool canJump;
    [SerializeField] UnityEvent jumpEvent = new UnityEvent();
    [SerializeField] UnityEvent landingEvent = new UnityEvent();

    private GravityAttractor attractor;
    private Rigidbody rb;

    private float rotation = 0f;
    public float Rotation { get { return rotation; } }

    private bool isGrounded;
    public bool IsGrounded { get { return isGrounded; } }

    private bool isRunning;
    public bool IsRunning { get { return isRunning; } }

    private float speed = 0;
    private float flyTime = 0;
    private float groundTime = 0;

    private void Start()
    {
        attractor = GravityAttractor.instance;
        rb = GetComponent<Rigidbody>();
        speed = startSpeed;
    }

    private void Update()
    {
        rotation = Input.GetAxis("Horizontal");

        LimitFlyTimeByGravity();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && rotation == 0 && canJump)
        {
            Jump();
        }
    }

    private void LimitFlyTimeByGravity()
    {
        if (!isGrounded)
        {
            flyTime += Time.deltaTime;
        }
        else
        {
            groundTime += Time.deltaTime;
        }

        if (flyTime >= flyTimeLimiter)
        {
            attractor.Gravity -= Time.deltaTime;
        }
        if (groundTime >= flyTimeLimiter)
        {
            attractor.Gravity = -10;
        }
    }

    private void FixedUpdate()
    {
        Run();

        if (rotation != 0 && isGrounded)
        {
            Turn();
        }
    }

    private void Run()
    {
        isRunning = true;
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private void Turn()
    {
        Vector3 yRotation = Vector3.up * rotation * turnSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(yRotation);
        Quaternion targetRotation = rb.rotation * deltaRotation;
        rb.transform.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 50f * Time.fixedDeltaTime);
        //rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 50f * Time.fixedDeltaTime));
    }

    private void Jump()
    {
        Vector3 force = transform.TransformDirection(Vector3.up) * GetJumpForce();
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        jumpEvent.Invoke();
    }

    private float GetJumpForce()
    {
        return maxJumpForce * attractor.Gravity * -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Planet")
        {
            isGrounded = true;
            landingEvent.Invoke();
            flyTime = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Planet")
        {
            isGrounded = false;
            isRunning = false;
            groundTime = 0;
        }
    }
}
