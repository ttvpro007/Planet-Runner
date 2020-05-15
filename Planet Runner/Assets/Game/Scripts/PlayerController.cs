using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Health health;
    SoundManager soundManager = SoundManager.instance;

    private void Start()
    {
        health = PlayerData.healthComp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other) return;

        if (other.gameObject.tag == "Debris")
        {
            health.TakeDamage(other.GetComponent<Debris>().DamageToPlayer);
            soundManager.Play("Impact Hit");
        }
    }
}
