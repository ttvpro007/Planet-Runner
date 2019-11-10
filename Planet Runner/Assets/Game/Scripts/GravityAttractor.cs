using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    [SerializeField] float gravity = -10f;
    public float Gravity { get { return gravity; } set { gravity = value; } }

    private static GravityAttractor _instance;
    public static GravityAttractor instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }

    public void Attract(GravityBody body)
    {
        body.GetComponent<Rigidbody>().AddForce(body.Up() * gravity);
        AlignBodyRotation(body);
    }

    private void AlignBodyRotation(GravityBody body)
    {
        Quaternion rotation = Quaternion.FromToRotation(body.LocalUp(), body.Up()) * body.transform.rotation;
        //body.transform.rotation = Quaternion.Slerp(body.transform.rotation, rotation, 50f * Time.fixedDeltaTime);
        //body.transform.rotation = rotation;
        body.GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(body.transform.rotation, rotation, 50f * Time.fixedDeltaTime));
    }
}
