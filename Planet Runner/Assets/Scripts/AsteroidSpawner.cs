using UnityEngine;
using ViTiet.UnityExtension.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] PlanetController planet;
    [SerializeField] GameObject asteroid;
    [SerializeField] float spawnAreaHeight;
    [SerializeField] float spawnDistanceFromPlanet;
    [SerializeField] float spawnInterval;
    [SerializeField] float amountPerInterval;
    [SerializeField] bool isFollowingPlayer;
    [SerializeField] Vector3 spawnVolume;

    float time = Mathf.Infinity;

    float innerRadius;
    float outerRadius;
    float planetRadius;

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
            go.GetComponent<GravityBody>().SetPlanet(planet.GetComponent<GravityAttractor>());
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!isFollowingPlayer)
        {
            Gizmos.DrawWireSphere(planet.transform.position, innerRadius);
            Gizmos.DrawWireSphere(planet.transform.position, outerRadius);
        }
        else
        {
            Gizmos.DrawWireCube(transform.position, spawnVolume);
        }
    }
}
