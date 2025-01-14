using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]  public ObstaclePool obstaclePool; // ObstaclePool scriptini çağırma
    [SerializeField] float obstacleSpawnTime = 2f;
    [SerializeField] float spawnWidth = 4f;

    List<GameObject> activeObstacles= new List<GameObject>(); // chunkları tutacak liste

    

    void Start()
    {
       StartCoroutine(SpawnObstacleRoutine()); // Engelleri oluşturmak için coroutine çağırma
       InvokeRepeating("ObstacleHandler", 0f, 1f); // ObstacleHandler'ı düzenli olarak çağır
    }

   
    
    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
              GameObject obstaclePrefab = obstaclePool.GetObstacle(); // ObstaclePool'dan engel al
            if (obstaclePrefab != null)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z); // Rastgele bir konum belirleme
                obstaclePrefab.transform.rotation = Random.rotation;
                obstaclePrefab.transform.position = spawnPosition;
                obstaclePrefab.SetActive(true);
                activeObstacles.Add(obstaclePrefab); // Aktif engeller listesine ekle
            }
            yield return new WaitForSeconds(obstacleSpawnTime); // Belirli bir süre bekletme

        }
    }

    void ObstacleHandler()
    {
         for (int i = 0; i < activeObstacles.Count; i++)
        {
            GameObject obstacle = activeObstacles[i];
            if (obstacle.transform.position.z <= Camera.main.transform.position.z) // chunkın pozisyonu belirli bir değerin altına düşerse
            {
                Debug.Log("Returning obstacle: " + obstacle.name);
                activeObstacles.RemoveAt(i);
                obstaclePool.ReturnObstacle(obstacle);
                i--;
            }
        }
        
    }
}
