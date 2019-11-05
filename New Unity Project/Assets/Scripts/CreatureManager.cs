using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    [SerializeField] int creatureCountPerCycle;
    [SerializeField] int creatureLimit;
    [SerializeField] float lifeCycleInterval;
    float time = Mathf.Infinity;
    [SerializeField] GameObject creature;
    public List<Creature> creatures;

    private void Start()
    {
        creatures = new List<Creature>();
    }

    private void Update()
    {
        SpawnCreature();
    }

    private void SpawnCreature()
    {
        if (time > lifeCycleInterval)
        {
            GameObject go;

            time = 0;

            if (Random.value < creature.GetComponent<Creature>().BirthRate && creatures.Count < creatureLimit)
            {
                for (int i = 0; i < creatureCountPerCycle; i++)
                {
                    go = Instantiate(creature, transform.position, Quaternion.identity);
                    go.GetComponent<Creature>().LifeCycleInterval = lifeCycleInterval;
                    go.GetComponent<Creature>().CManager = GetComponent<CreatureManager>();
                    creatures.Add(go.GetComponent<Creature>());
                }
            }
        }
        time += Time.deltaTime;
    }
}
