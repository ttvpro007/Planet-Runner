using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 0f;
    [SerializeField] public OnTakeDamageEvent takeDamage = null;
    [SerializeField] public UnityEvent playerDead = null;
    GameObject player;

    private float health;
    public float CurrentHealth { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }

    private void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0);

        if (health == 0)
        {
            Dead();
            playerDead.Invoke();
        }
        else
        {
            takeDamage.Invoke(health / maxHealth);
        }
    }

    public void AddHealth(float amount)
    {
        health += amount;
        health = Mathf.Min(health, maxHealth);
    }

    private void Dead()
    {
        PlayerData.playerControllerComp.enabled = false;
        PlayerData.playerMovementComp.enabled = false;
        PlayerData.playerAnimationComp.enabled = false;

        SkinnedMeshRenderer[] renderers = player.GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }

        player.GetComponent<GravityBody>().Static = true;

        Collider[] colliders = player.GetComponents<Collider>();

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
}

[System.Serializable]
public class OnTakeDamageEvent : UnityEvent<float>
{
}