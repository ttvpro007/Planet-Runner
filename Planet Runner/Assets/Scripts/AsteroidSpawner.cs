using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] PlanetController planet;
    [SerializeField] GameObject asteroid;
    [SerializeField] float spawnAreaHeight;
    [SerializeField] float spawnDistanceFromPlanet;
    [SerializeField] float spawnInterval;
    [SerializeField] float amountPerInterval;

    float time = Mathf.Infinity;

    float innerRadius;
    float outerRadius;
    float planetRadius;

    private void FixedUpdate()
    {
        planetRadius = planet.Radius;
        innerRadius = planetRadius + spawnDistanceFromPlanet;
        outerRadius = planetRadius + spawnDistanceFromPlanet + spawnAreaHeight;

        if (time > spawnInterval)
        {
            time = 0;
            SpawnAsteroid(amountPerInterval, innerRadius, outerRadius);
        }

        time += Time.deltaTime;
    }

    private void SpawnAsteroid(float amount, float lowerLimit, float upperLimit)
    {
        float scalar;
        Vector3 randomPosition;
        GameObject go;

        for (int i = 0; i < amount; i++)
        {
            scalar = Random.Range(lowerLimit, upperLimit);
            randomPosition = Random.onUnitSphere * scalar;

            go = Instantiate(asteroid, randomPosition, Quaternion.identity);
            go.GetComponent<GravityBody>().SetPlanet(planet.GetComponent<GravityAttractor>());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(planet.transform.position, innerRadius);
        Gizmos.DrawWireSphere(planet.transform.position, outerRadius);
    }
}
