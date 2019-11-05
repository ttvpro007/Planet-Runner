using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float turnSpeed = 3f;
    
    private float rotation;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rotation = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (rotation != 0)
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
    }

    private float GetJumpForce()
    {
        return jumpForce * GetComponent<GravityBody>().Attractor.Gravity * -1;
    }
}
