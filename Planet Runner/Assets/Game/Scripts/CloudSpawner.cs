using UnityEngine;
using ViTiet.ProceduralGenerator.Helper;
using ViTiet.UnityExtension.Gizmos;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] int cloudNumber = 25;
    [SerializeField] float spawnAreaHeight;
    [SerializeField] float spawnDistanceFromPlanet;
    [SerializeField] float scalingFactor;
    [SerializeField] GameObject[] cloudPrefabs = null;

    private GravityAttractor attractor;
    private PlanetController planet;

    private float innerRadius;
    private float outerRadius;
    private float planetRadius;

    private float multiplier = 5f;

    private void Start()
    {
        attractor = GravityAttractor.instance;
        planet = attractor.GetComponent<PlanetController>();

        GenerateCloud();
    }

    private void GenerateCloud()
    {
        planetRadius = planet.Radius * multiplier;
        innerRadius = planetRadius + spawnDistanceFromPlanet;
        outerRadius = planetRadius + spawnDistanceFromPlanet + spawnAreaHeight;

        float scalar;
        Vector3 randomPosition;
        GravityBody cloud;

        for (int i = 0; i < cloudNumber; i++)
        {
            scalar = Random.Range(innerRadius, outerRadius);
            randomPosition = Random.onUnitSphere * scalar;

            cloud = Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Length - 1)], randomPosition, Quaternion.identity).GetComponent<GravityBody>();
            cloud.transform.localScale *= scalingFactor;
            AlignBodyRotationToPlanetNormal(cloud);

        }
    }

    private void AlignBodyRotationToPlanetNormal(GravityBody body)
    {
        body.transform.rotation = Quaternion.FromToRotation(transform.up, body.transform.position - transform.position) * body.transform.rotation;
    }
}
