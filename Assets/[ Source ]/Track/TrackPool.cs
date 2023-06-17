using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPool : MonoBehaviour
{
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject frontWall;
    [SerializeField] private int initialSpawn = 5;
    [SerializeField] private List<GameObject> possibleBlocks;
    [SerializeField] private float distanceToLoad = 20;

    private List<GameObject> currentlySpawned;
    private int farthestBlockId = 0;

    private void Start()
    {
        currentlySpawned = new List<GameObject>();
        for (int i = 0; i < initialSpawn; i++) PositionBlock(SpawnBlock());
    }

    private void Update()
    {
        GameObject farthestBlock = currentlySpawned[currentlySpawned.Count-1];
        if (DiceManager.Instance.farthestDie.transform.position.z > farthestBlock.transform.position.z - distanceToLoad)
        {
            PrepareNextBlock();
        }
    }

    private void PrepareNextBlock()
    {
        if (DiceManager.Instance.closestDie.transform.position.z > currentlySpawned[0].transform.position.z)
        {
            PositionBlock(currentlySpawned[0]);
            currentlySpawned.Add(currentlySpawned[0]);
            currentlySpawned.RemoveAt(0);

            backWall.transform.localPosition = new Vector3(0, 2, currentlySpawned[0].transform.position.z - 5);
            frontWall.transform.localPosition = new Vector3(0, 2, currentlySpawned[currentlySpawned.Count-1].transform.position.z + 5);
        }
        else PositionBlock(SpawnBlock()); 
    }

    private GameObject SpawnBlock()
    {
        GameObject newBlock = Instantiate(possibleBlocks[0], transform);
        currentlySpawned.Add(newBlock);
        return newBlock;
    }

    private void PositionBlock(GameObject block)
    {
        block.transform.localPosition = new Vector3(0, 0, 10 * farthestBlockId);
        farthestBlockId++;
    }
}
