using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float jumpForce = 10f;

    Vector3 direction;

    private void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.TransformDirection(direction) * speed * Time.deltaTime);
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
