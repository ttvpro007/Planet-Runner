using UnityEngine;

public class PlanetPopulator : MonoBehaviour
{
    [SerializeField] GameObject[] housePrefabs;
    [SerializeField] int houseCount = 0;
    [SerializeField] GameObject[] treePrefabs;
    [SerializeField] int treeCount = 0;
    [SerializeField] GameObject[] rockPrefabs;
    [SerializeField] int rockCount = 0;
    [SerializeField] LayerMask spawnOnLayer;

    private GravityAttractor attractor;
    private PlanetController planet;
    private GravityBody body;

    Vector3 raycastOrigin = Vector3.zero;
    private float raycastRadius;
    private float raycastRadiusOffset = 5f;
    private float raycastRadiusMultiplier = 5;
    private float planetRadius;

    private void Start()
    {
        attractor = GravityAttractor.instance;
        planet = attractor.GetComponent<PlanetController>();

        planetRadius = planet.Radius * raycastRadiusMultiplier;
        raycastRadius = planetRadius + raycastRadiusOffset;

        SpawnStuff(housePrefabs, houseCount);
        SpawnStuff(treePrefabs, treeCount);
        SpawnStuff(rockPrefabs, rockCount);
    }

    private void SpawnStuff(GameObject[] prefabs, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            body = Instantiate(prefabs[Random.Range(0, prefabs.Length)], GetSpawnPosition(), Quaternion.identity).GetComponent<GravityBody>();
            AlignBodyRotationToPlanetNormal(body);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        raycastOrigin = Random.onUnitSphere * raycastRadius;
        Vector3 direction = transform.position - raycastOrigin;
        RaycastHit hit;
        bool hasHit;

        int count = 0;
        do
        {
            hasHit = Physics.Raycast(raycastOrigin, direction, out hit, raycastRadiusOffset + 1, spawnOnLayer.value);

            if (count > 1000) break;
            count++;
        }
        while (!hasHit);

        return hit.point;
    }
    
    private void AlignBodyRotationToPlanetNormal(GravityBody body)
    {
        body.transform.rotation = Quaternion.FromToRotation(transform.up, raycastOrigin - transform.position) * body.transform.rotation;
    }
}
