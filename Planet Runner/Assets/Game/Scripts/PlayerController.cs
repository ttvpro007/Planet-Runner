using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float maxJumpForce = 50f;
    [SerializeField] float turnSpeed = 3f;
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

    private void Start()
    {
        attractor = GravityAttractor.instance;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rotation = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && rotation == 0)
        {
            Jump();
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
        rb.transform.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 50f);
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Planet")
        {
            isGrounded = false;
            isRunning = false;
        }
    }
}
