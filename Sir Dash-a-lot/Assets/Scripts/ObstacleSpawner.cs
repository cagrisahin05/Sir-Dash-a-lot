using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]  public ObstaclePool obstaclePool; // ObstaclePool scriptini çağırma
    [SerializeField] float obstacleSpawnTime = 2f;
    [SerializeField] float spawnWidth = 4f;

    List<GameObject> activeObstacles= new List<GameObject>(); // chunkları tutacak liste
    private Coroutine spawnRoutine; // coroutine tanımlama
    private GameObject lastObstacle; // Son engeli tutacak değişken

    

    void Start()
    {
       
    }
       public void StartSpawning()
    {
        spawnRoutine = StartCoroutine(SpawnObstacleRoutine()); // Engelleri oluşturmak için coroutine çağırma
        InvokeRepeating("ObstacleHandler", 0f, 1f); // ObstacleHandler'ı düzenli olarak çağır
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
        CancelInvoke("ObstacleHandler");
    }
   
    
    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
              GameObject obstaclePrefab = GetRandomObstacle(); // ObstaclePool'dan engel al
            if (obstaclePrefab != null)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z); // Rastgele bir konum belirleme
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0); // Rastgele bir rotasyon belirleme
                obstaclePrefab.transform.rotation = randomRotation; // Engelin rotasyonunu ayarlama
                obstaclePrefab.transform.position = spawnPosition;
                obstaclePrefab.SetActive(true);
                activeObstacles.Add(obstaclePrefab); // Aktif engeller listesine ekle
                lastObstacle = obstaclePrefab; // Son engeli güncelle   
            }
            yield return new WaitForSeconds(obstacleSpawnTime); // Belirli bir süre bekletme

        }
    }
    GameObject GetRandomObstacle()
    {
        GameObject obstaclePrefab;
        int deneme = 0;
        do
        {
            obstaclePrefab = obstaclePool.GetObstacle(); // ObstaclePool'dan engel al
            deneme++; // Deneme sayısını arttır
        } while (obstaclePrefab == lastObstacle && deneme < 10);

        return obstaclePrefab;
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
