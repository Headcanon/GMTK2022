using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnableThings;

    private Bounds bounds;

    private void Awake()
    {
        bounds = GetComponent<BoxCollider>().bounds;
        GameObject thing = Instantiate(spawnableThings[Random.Range(0, spawnableThings.Count)], transform.parent);
        thing.transform.position = bounds.GetRandomPoint();

    }
}
