using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    //[SerializeField] ObjectType type;
    [SerializeField] GameObject[] piecePrefabs;
    [SerializeField] GameObject particleFX;

    SoundManager soundManager = SoundManager.instance;

    //public ObjectType Type { get { return type; } }
    private int piecesToBreak;

    void Start()
    {
        piecesToBreak = GetPiecesToBreak();
    }

    private void Break()
    {
        GameObject piece;

        for (int i = 0; i < piecesToBreak; i++)
        {
            piece = Instantiate(piecePrefabs[Random.Range(0, piecePrefabs.Length - 1)], transform.position, Quaternion.identity);
            piece.GetComponent<GravityBody>().Static = false;
            piece.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(100, 200), transform.position, 5);
            Destroy(piece, 10);
        }

        soundManager.Play("Asteroid Hit");
    }

    private void DestroyBreakableObject()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }

        Collider[] colliders = GetComponentsInChildren<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        
        Destroy(gameObject, 2);
    }

    private int GetPiecesToBreak()
    {
        switch (GetComponent<PlanetObject>().Type)
        {
            case ObjectType.Asteroid:
                return 10;
            case ObjectType.Tree:
                return 2;
            case ObjectType.House:
                return 7;
        }

        return 0;
    }

    private void SpawnExplosionPFX(Transform transform)
    {
        Instantiate(particleFX, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag != "Asteroid" && other.gameObject.tag == "Asteroid")
        {
            Break();
            DestroyBreakableObject();
            SpawnExplosionPFX(other.transform);
        }
        else if (gameObject.tag == "Asteroid" && other.gameObject.tag == "Planet")
        {
            Break();
            DestroyBreakableObject();
            SpawnExplosionPFX(transform);
        }
    }
}