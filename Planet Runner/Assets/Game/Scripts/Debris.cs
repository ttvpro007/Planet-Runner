using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] float damageToPlayer;
    public float DamageToPlayer { get { return damageToPlayer; } }

    private SphereCollider sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Planet")
        {
            sphereCollider.isTrigger = false;
        }
    }
}
