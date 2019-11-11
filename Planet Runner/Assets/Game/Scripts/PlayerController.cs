using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Health health;

    private void Start()
    {
        health = PlayerData.health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Debris")
        {
            health.TakeDamage(other.GetComponent<Debris>().DamageToPlayer);
        }
    }
}
