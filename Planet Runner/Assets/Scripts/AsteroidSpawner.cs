using UnityEngine;
using ViTiet.UnityExtension.Random;
using ViTiet.UnityExtension.Gizmos;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject asteroid;
    [SerializeField] float spawnAreaHeight;
    [SerializeField] float spawnDistanceFromPlanet;
    [SerializeField] float spawnInterval;
    [SerializeField] float amountPerInterval;
    [SerializeField] bool isFollowingPlayer;
    [SerializeField] Vector3 spawnVolume;

    private GravityAttractor attractor;
    private PlanetController planet;

    private float time = Mathf.Infinity;

    private float innerRadius;
    private float outerRadius;
    private float planetRadius;

    private void Start()
    {
        attractor = GravityAttractor.instance;
        planet = attractor.GetComponent<PlanetController>();
    }

    private void FixedUpdate()
    {
        if (!isFollowingPlayer)
        {
            planetRadius = planet.Radius;
            innerRadius = planetRadius + spawnDistanceFromPlanet;
            outerRadius = planetRadius + spawnDistanceFromPlanet + spawnAreaHeight;
        }

        if (time > spawnInterval)
        {
            time = 0;
            SpawnAsteroid(amountPerInterval, innerRadius, outerRadius, isFollowingPlayer);
        }

        time += Time.deltaTime;
    }

    private void SpawnAsteroid(float amount, float lowerLimit, float upperLimit, bool isFollowingPlayer)
    {
        float scalar;
        Vector3 randomPosition;
        GameObject go;

        for (int i = 0; i < amount; i++)
        {
            if (!isFollowingPlayer)
            {
                scalar = Random.Range(lowerLimit, upperLimit);
                randomPosition = Random.onUnitSphere * scalar;
            }
            else
            {
                randomPosition = RandomExtended.RandomInCube(transform, spawnVolume);
            }

            go = Instantiate(asteroid, randomPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!isFollowingPlayer && attractor)
        {
            Gizmos.DrawWireSphere(attractor.transform.position, innerRadius);
            Gizmos.DrawWireSphere(attractor.transform.position, outerRadius);
        }
        else
        {
            GizmosExtended.DrawWireCube(transform, spawnVolume, Color.white);
        }
    }
}
