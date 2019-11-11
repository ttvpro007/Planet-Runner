using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetObject : MonoBehaviour
{
    [SerializeField] ObjectType type;
    [SerializeField] VectorUp modelVectorUp;

    public ObjectType Type { get { return type; } }

    public Vector3 ModelVectorUp
    {
        get
        {
            switch (modelVectorUp)
            {
                case VectorUp.up:
                    return transform.up;
                case VectorUp.down:
                    return transform.up * -1;
                case VectorUp.forward:
                    return transform.forward;
                case VectorUp.bacward:
                    return transform.forward * -1;
                case VectorUp.right:
                    return transform.right;
                case VectorUp.left:
                    return transform.right * -1;
                default:
                    return Vector3.zero;
            }
        }
    }
}

public enum ObjectType
{
    Asteroid, GrassOrRock, Tree, House
}

public enum VectorUp
{
    up, down, forward, bacward, right, left
}