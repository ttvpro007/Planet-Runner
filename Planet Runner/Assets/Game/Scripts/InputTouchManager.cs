using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouchManager : MonoBehaviour
{
    // Global variable
    private static InputTouchManager _instance;
    public static InputTouchManager instance { get { return _instance; } }
    
    [SerializeField] bool mouseTouchSimulation;
    [SerializeField] float minDistanceForSwipe = 10f;
    [SerializeField] float maxSwipeDistance = 100f;

    private bool isSwiping;

    private Vector2 beginTouchPosition = new Vector2();
    private Vector2 endTouchPosition = new Vector2();

    private SwipeDirection swipeDirection = new SwipeDirection();

    public static event Action<SwipeInfo> OnSwipe = delegate { };

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        isSwiping = false;
    }
    
    private void Update()
    {
#if UNITY_EDITOR

        if (mouseTouchSimulation)
        {
            if (Input.GetMouseButtonDown(0))
            {
                beginTouchPosition = Input.mousePosition;
            }

            if (!IsVerticalSwipe() && Input.GetMouseButton(0))
            {
                isSwiping = true;
                endTouchPosition = Input.mousePosition;
                if (SwipeRegistered()) SendSwipe();
            }

            if (Input.GetMouseButtonUp(0))
            {
                endTouchPosition = Input.mousePosition;
                if (SwipeRegistered()) SendSwipe();
                isSwiping = false;
            }
        }

#endif

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    beginTouchPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    isSwiping = true;
                    endTouchPosition = touch.position;
                    if (SwipeRegistered()) SendSwipe();
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    endTouchPosition = touch.position;
                    if (SwipeRegistered()) SendSwipe();
                    isSwiping = false;
                }
            }
        }
    }

    private float XDifference()
    {
        return endTouchPosition.x - beginTouchPosition.x;
    }

    private float YDifference()
    {
        return endTouchPosition.y - beginTouchPosition.y;
    }

    private float HorizontalSwipeDistance()
    {
        return Mathf.Abs(XDifference());
    }

    private float VerticalSwipeDistance()
    {
        return Mathf.Abs(YDifference());
    }

    private bool IsVerticalSwipe()
    {
        return VerticalSwipeDistance() > HorizontalSwipeDistance();
    }

    private SwipeDirection GetInputSwipeDirection()
    {
        if (IsVerticalSwipe())
        {
            return (YDifference() > 0) ? SwipeDirection.Up : SwipeDirection.Down;
        }
        else
        {
            return (XDifference() > 0) ? SwipeDirection.Right : SwipeDirection.Left;
        }
    }

    private bool SwipeRegistered()
    {
        if (IsVerticalSwipe())
        {
            return VerticalSwipeDistance() > minDistanceForSwipe;
        }
        else
        {
            return HorizontalSwipeDistance() > minDistanceForSwipe;
        }
    }

    private float GetDistance()
    {
        if (IsVerticalSwipe())
        {
            if (swipeDirection == SwipeDirection.Up)
            {
                return Mathf.Min(YDifference(), maxSwipeDistance);
            }
            else if (swipeDirection == SwipeDirection.Down)
            {
                return Mathf.Max(YDifference(), -maxSwipeDistance);
            }
        }
        else
        {
            if (swipeDirection == SwipeDirection.Right)
            {
                return Mathf.Min(XDifference(), maxSwipeDistance);
            }
            else if (swipeDirection == SwipeDirection.Left)
            {
                return Mathf.Max(XDifference(), -maxSwipeDistance);
            }
        }

        return 0;
    }

    private float GetDistanceNormalize()
    {
        return GetDistance() / maxSwipeDistance;
    }

    private void SendSwipe()
    {
        swipeDirection = GetInputSwipeDirection();

        SwipeInfo swipeInfo = new SwipeInfo()
        {
            startPosition = beginTouchPosition,
            endPosition = endTouchPosition,
            distance = GetDistance(),
            axis = GetDistanceNormalize(),
            direction = swipeDirection
        };

        OnSwipe(swipeInfo);
    }
}

public struct SwipeInfo
{
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float distance;
    public float axis;
    public SwipeDirection direction;
}

public enum SwipeDirection
{
    Right,
    Left,
    Up,
    Down
}