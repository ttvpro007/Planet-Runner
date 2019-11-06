using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float turnSpeed = 3f;
    [SerializeField] UnityEvent jumpEvent;
    [SerializeField] UnityEvent landingEvent;
    
    private float rotation;
    public float Rotation { get { return rotation; } }

    private bool isGrounded;
    public bool IsGrounded { get { return isGrounded; } }

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isGrounded) rotation = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && rotation == 0)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (rotation != 0 && isGrounded)
        {
            Turn();
        }
    }

    private void Move()
    {
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
        GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.up) * GetJumpForce());
        jumpEvent.Invoke();
    }

    private float GetJumpForce()
    {
        return jumpForce * GetComponent<GravityBody>().Attractor.Gravity * -1;
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
        }
    }
}
