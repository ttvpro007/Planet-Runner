using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Creature : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float birthRate;
    [Range(0, 1)]
    [SerializeField] float deathRate;
    [Range(0, 1)]
    [SerializeField] float reproductionRate;
    [Range(0, 1)]
    [SerializeField] float reproductionMutationChance;
    [Range(1, 10)]
    [SerializeField] float perceptionRange;
    float lifeCycleInterval;
    float time = 0;

    [SerializeField] GameObject[] offsprings;

    public float BirthRate { get { return birthRate; } }
    public float DeathRate { get { return deathRate; } }
    public float ReproductionRate { get { return reproductionRate; } }
    public float ReproductionMutationChance { get { return reproductionMutationChance; } }
    public float PerceptionRange { get { return perceptionRange; } }
    public float LifeCycleInterval { get { return lifeCycleInterval; } set { lifeCycleInterval = value; } }

    NavMeshAgent agent;
    CreatureManager cManager;

    public CreatureManager CManager { get { return cManager; } set { cManager = value; } }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(NextDestination(perceptionRange));
    }

    private void Update()
    {
        Move();

        if (time >= lifeCycleInterval)
        {
            time = 0;
            Reproduce();
            Death();
        }
        time += Time.deltaTime;
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 1.2f || agent.isPathStale)
        {
            agent.SetDestination(NextDestination(perceptionRange));
        }
    }

    private Vector3 NextDestination(float range)
    {
        Vector2 randomSpot = Random.insideUnitCircle;
        return new Vector3(randomSpot.x * range + transform.position.x, 0, randomSpot.y * range + transform.position.z);
    }

    private void Reproduce()
    {
        GameObject offspring;

        if (Random.Range(0f, 100f) / 100 <= reproductionRate)
        {
            Vector2 randomSpot = Random.insideUnitCircle;
            if (reproductionMutationChance != 0 && Random.Range(0f, 100f) / 100 <= reproductionMutationChance)
            {
                offspring = Instantiate(offsprings[1], new Vector3(randomSpot.x, 0, randomSpot.y) + transform.position, Quaternion.identity);
            }
            else
            {
                offspring = Instantiate(offsprings[0], new Vector3(randomSpot.x, 0, randomSpot.y) + transform.position, Quaternion.identity);
            }
            offspring.GetComponent<Creature>().LifeCycleInterval = lifeCycleInterval;
            offspring.GetComponent<Creature>().CManager = cManager;
            cManager.creatures.Add(offspring.GetComponent<Creature>());
        }
    }

    private void Death()
    {
        if (Random.Range(0f, 100f) / 100 <= deathRate)
        {
            cManager.creatures.Remove(this);
            Destroy(gameObject);
        }
    }
}
