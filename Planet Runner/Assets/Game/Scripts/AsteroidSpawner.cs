using UnityEngine;
using ViTiet.UnityExtension.Random;
using ViTiet.UnityExtension.Gizmos;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] float spawnAreaHeight;
    [SerializeField] float spawnDistanceFromPlanet;
    [SerializeField] float spawnInterval;
    [SerializeField] float amountPerInterval;
    [SerializeField] bool isFollowingPlayer;
    [SerializeField] Vector3 spawnVolume;

    private GravityAttractor attractor;
    private PlanetController planet;
    GameObject asteroidVarian;

    private float time = Mathf.Infinity;

    private float innerRadius;
    private float outerRadius;
    private float planetRadius;

    private float multiplier = 5f;

    private void Start()
    {
        attractor = GravityAttractor.instance;
        planet = attractor.GetComponent<PlanetController>();
    }

    private void FixedUpdate()
    {
        if (!isFollowingPlayer)
        {
            planetRadius = planet.Radius * multiplier;
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

        int randomIndex = 0;

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
            
            GameObject asteroid = Instantiate(asteroidPrefab, randomPosition, Quaternion.identity);

            randomIndex = Random.Range(0, asteroidPrefab.transform.childCount);
            asteroid.transform.GetChild(randomIndex).gameObject.SetActive(true);
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
