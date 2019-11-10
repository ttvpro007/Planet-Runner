using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] ObjectType objectType;
    [SerializeField] GameObject[] piecePrefabs;

    private int piecesToBreak;

    void Start()
    {
        piecesToBreak = GetPiecesToBreak();
    }

    private void Break()
    {
        GameObject piece;
        //Vector3 explosionPosition;
        //Vector3 spawnPosition;

        for (int i = 0; i < piecesToBreak; i++)
        {
            //spawnPosition = transform.up * 2 + transform.position;
            piece = Instantiate(piecePrefabs[Random.Range(0, piecePrefabs.Length - 1)], transform.position, Quaternion.identity);
            //piece = Instantiate(piecePrefabs[Random.Range(0, piecePrefabs.Length - 1)], spawnPosition, Quaternion.identity);
            //explosionPosition = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), Random.Range(-.5f, .5f)) + transform.position;
            piece.GetComponent<GravityBody>().Static = false;
            //piece.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(100, 200), explosionPosition, 5);
            piece.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(100, 200), transform.position, 5);
            Destroy(piece, 10);
        }
    }

    private void DestroyBreakableObject()
    {
        GetComponent<Renderer>().enabled = false;
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        
        Destroy(gameObject, 2);
    }

    private int GetPiecesToBreak()
    {
        switch (objectType)
        {
            case ObjectType.Asteroid:
                return 5;
            case ObjectType.Tree:
                return 3;
            case ObjectType.House:
                return 10;
        }

        return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag != "Asteroid" && other.gameObject.tag == "Asteroid" /*|| other.gameObject.tag == "Debris"*/)
        {
            Break();
            DestroyBreakableObject();
        }
        else if (gameObject.tag == "Asteroid" && other.gameObject.tag == "Planet")
        {
            Break();
            DestroyBreakableObject();
        }
    }
}

public enum ObjectType
{
    Asteroid, Tree, House
}