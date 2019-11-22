using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool useTouchControl;
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

    private void OnEnable()
    {
        InputTouchManager.OnSwipe += PlayerSwipeControl;
    }

    private void OnDisable()
    {
        InputTouchManager.OnSwipe -= PlayerSwipeControl;
    }

    private void Start()
    {
        attractor = GravityAttractor.instance;
        rb = GetComponent<Rigidbody>();
        speed = startSpeed;
    }

    private void Update()
    {
        if (useTouchControl)
        {
            if (rotation != 0)
            {
                SwipeControlRotationZerolize();
            }
        }
        else
        {
            rotation = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && rotation == 0 && canJump)
            {
                Jump();
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && !isGrounded)
            {
                Fall();
            }
            //else if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    Dodge(false);
            //}
            //else if (Input.GetKeyDown(KeyCode.E))
            //{
            //    Dodge(true);
            //}
        }

        //LimitFlyTimeByGravity();
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
        Quaternion targetRotation = rb.transform.rotation * deltaRotation;
        //rb.MoveRotation(Quaternion.Slerp(rb.transform.rotation, targetRotation, 50f * Time.fixedDeltaTime));
        rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, targetRotation, 50f * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        Vector3 force = transform.TransformDirection(Vector3.up) * GetJumpForce();
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        jumpEvent.Invoke();
    }

    private void Fall()
    {
        Vector3 force = transform.TransformDirection(Vector3.down) * GetJumpForce();
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
    }

    private void Dodge(bool moveRight)
    {
        Vector3 direction = (moveRight) ? Vector3.right : Vector3.left;
        GetComponent<Rigidbody>().MovePosition(rb.position + transform.TransformDirection(direction) * speed * 15 * Time.fixedDeltaTime);
    }

    private void PlayerSwipeControl(SwipeInfo swipeInfo)
    {
        if (!useTouchControl) return;

        switch (swipeInfo.direction)
        {
            case SwipeDirection.Right:
            case SwipeDirection.Left:
                rotation = swipeInfo.axis;
                break;
            case SwipeDirection.Up:
                if (isGrounded && rotation == 0 && canJump)
                {
                    Jump();
                }
                break;
            case SwipeDirection.Down:
                if (!isGrounded)
                {
                    Fall();
                }
                break;
        }
    }

    private void SwipeControlRotationZerolize()
    {
        if (rotation > 0)
        {
            rotation -= 0.1f;
            if (rotation < .1) rotation = 0;
        }

        if (rotation < 0)
        {
            rotation += .1f;
            if (rotation > -0.1) rotation = 0;
        }
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
