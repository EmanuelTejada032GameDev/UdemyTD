using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private List<WorldGeneratorSpawner> _worldGeneratorSpawners;

    void Start()
    {
        SpawnWorldObjects();
    }

    public void SpawnWorldObjects()
    {
        foreach (WorldGeneratorSpawner spawner in _worldGeneratorSpawners)
        {
            for (int i = 0; i < spawner.quantity; i++)
            {
                Vector3 randomPosition = new Vector3(
                    Random.Range(spawner.TopRigthLimits.x, -spawner.TopRigthLimits.x),
                    Random.Range(spawner.LeftBottomLimits.y, -spawner.LeftBottomLimits.y),
                    0f
                );

                Instantiate(spawner.pfToSpawn[Random.Range(0,spawner.pfToSpawn.Count)], randomPosition, Quaternion.identity, spawner.Parent);
            }
        }
    }
}

[System.Serializable]
public class WorldGeneratorSpawner {
    public Transform Parent;
    public List<Transform> pfToSpawn;
    public int quantity;
    public Vector3 TopRigthLimits;
    public Vector3 LeftBottomLimits;
}
