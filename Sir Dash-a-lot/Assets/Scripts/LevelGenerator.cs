using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] ObjectPool chunkPool; // chunkları tutacak object pool
    [SerializeField] float chunkLength = 10f; // chunk uzunluğu
    [SerializeField] float moveSpeed = 8f; // chunkların hareket hızı
    
    List <GameObject> activeChunks = new List<GameObject>(); // chunkları tutacak liste



    void Start()
    {
        for (int i = 0; i < 12; i++) // 12 tane chunk oluşturuyorum
        {
            SpawnChunks();
        }
        
    }
    void Update()
    {
        MoveChunks();
    }

    void SpawnChunks()
    {      
        GameObject newChunk = chunkPool.GetChunk(); // object pool'dan chunk al
        newChunk.transform.position = new Vector3(0, 0, SpawnPosZCalculator()); // chunkın pozisyonunu belirle
        activeChunks.Add(newChunk); // chunkı listeye ekle
    }
    float SpawnPosZCalculator() // chunkların pozisyonunu belirle
    {
        float spawnPosZ;
            if (activeChunks.Count == 0) // eğer hiç chunk yoksa
            {
                spawnPosZ = transform.position.z;
            }
            else 
            {
                spawnPosZ = activeChunks[activeChunks.Count - 1].transform.position.z + chunkLength; // eğer ki chunk varsa en son oluşturulan chunkın pozisyonunu al ve chunk uzunluğunu ekle
            }

        return spawnPosZ; 
    }

    void MoveChunks() // chunkları hareket ettir
    {
        for (int i = 0; i < activeChunks.Count; i++)
        {
            GameObject chunk = activeChunks[i];
            chunk.transform.Translate(-transform.forward * moveSpeed * Time.deltaTime); // chunkları ileri doğru hareket ettir

            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength) // chunkın pozisyonu belirli bir değerin altına düşerse
            {  

                activeChunks.RemoveAt(i); // chunkı listeden çıkar
                chunkPool.ReturnObject(chunk); // chunkı object pool'a geri döndür
                SpawnChunks(); // yeni bir chunk oluştur
                i--; // chunklar listeden çıkarıldığı için indexi bir azalt
            }
        }
    }
    
}
