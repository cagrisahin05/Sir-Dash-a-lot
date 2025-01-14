using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] Transform obstacleParent; 
    public List<GameObject> obstaclePrefabs;
    public int poolSize = 4;
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
            GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)], obstacleParent);
            obstacle.SetActive(false);
            obstaclePool.Add(obstacle);
        }
    }

    public GameObject GetObstacle()
    {
        foreach (GameObject obstacle in obstaclePool) 
        {
            if (!obstacle.activeInHierarchy)
            {
                obstacle.SetActive(true);
                return obstacle;
            }
    
        }
    
        GameObject newObstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count) ], obstacleParent); //
        newObstacle.SetActive(false);
        obstaclePool.Add(newObstacle);
        return newObstacle;
    }

    public void ReturnObstacle(GameObject obstacle)
    {
        obstacle.SetActive(false);
    }    
}
