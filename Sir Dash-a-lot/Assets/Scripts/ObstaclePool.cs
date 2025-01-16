using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] Transform obstacleParent; 
    public List<GameObject> obstaclePrefabs;
    public int poolSize = 4;
    private GameObject tempoObstaclePool;  
    private List<GameObject> obstaclePool;

    private void Start()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        obstaclePool = new List<GameObject>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            tempoObstaclePool = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)], obstacleParent);
            tempoObstaclePool.SetActive(false);
            obstaclePool.Add(tempoObstaclePool);
        }
    }

    public GameObject GetObstacle()
    {
        foreach ( GameObject obstacle in obstaclePool) 
        {
            if (!obstacle.activeInHierarchy)
            {
                obstacle.SetActive(true);
                return obstacle;
            }
    
        }
    
        tempoObstaclePool = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count) ], obstacleParent); //
        tempoObstaclePool.SetActive(false);
        obstaclePool.Add(tempoObstaclePool);
        return tempoObstaclePool;
    }

    public void ReturnObstacle(GameObject obstacle)
    {
        obstacle.SetActive(false);
    }    
}
