using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControlInfo : MonoBehaviour
{
    // Global variable
    private static TouchControlInfo _instance;
    public static TouchControlInfo instance { get { return _instance; } }

    private Screen screen;
    private Touch touchInput = new Touch();

    private InputSwipeDirection swipeDirection = new InputSwipeDirection();
    public InputSwipeDirection SwipeDirection { get { return swipeDirection; } }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {

    }
    
    private void Update()
    {
        
    }
}

public enum InputSwipeDirection
{
    Right, Left, Up
}