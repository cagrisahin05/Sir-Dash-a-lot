using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]  GameObject[] obstaclePrefabs; // Engellerin prefablarını tutmak için
    [SerializeField] float obstacleSpawnTime = 2f;
    [SerializeField] float spawnWidth = 4f;
    [SerializeField] Transform obstacleParent; // Engellerin parent olması için
    

    void Start()
    {
       StartCoroutine(SpawnObstacleRoutine()); // Engelleri oluşturmak için coroutine çağırma
    }

   
    
    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            GameObject obstaclePrefab = obstaclePrefabs [Random.Range(0, obstaclePrefabs.Length)]; //  Arrayden rastgele bir engel seçmek için
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z); // Rastgele bir konum belirleme
            yield return new WaitForSeconds(obstacleSpawnTime); // Belirli bir süre bekletme
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent); // Engel oluşturma
            
        }
    }
}
