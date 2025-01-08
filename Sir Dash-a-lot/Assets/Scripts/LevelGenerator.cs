using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab; // chunk prefabı
    [SerializeField] int startingChunkAmount = 12; // baştaki chunk sayısı
    [SerializeField] Transform chunkParent; // chunkların parentı
    [SerializeField] float chunkLength = 10f; // chunk uzunluğu
    [SerializeField] float moveSpeed = 8f; // chunkların hareket hızı
    List <GameObject> chunks = new List<GameObject>(); // chunkları tutacak dizi



    void Start()
    {
        SpawnStartingChunks();
        
    }
    void Update()
    {
        MoveChunks();
    }


    void SpawnStartingChunks()
    {
       for (int i = 0; i < startingChunkAmount; i++) // başlangıçta belirtilen sayıda chunk oluştur
        {
            SpawnChunks();

        }
    }
    void SpawnChunks()
    {      
        float spawnPosZ = SpawnPosZCalculator(); 
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, spawnPosZ); // chunkın pozisyonunu belirle
        GameObject newChunk = Instantiate(chunkPrefab, spawnPos, Quaternion.identity, chunkParent); // chunkı oluştur
        chunks.Add(newChunk); // chunkı listeye ekle
    }
    float SpawnPosZCalculator() // chunkların pozisyonunu belirle
    {
        float spawnPosZ;
            if (chunks.Count == 0) // eğer hiç chunk yoksa
            {
                spawnPosZ = transform.position.z;
            }
            else // diğer chunkların pozisyonunu belirle
            {
                spawnPosZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
            }

        return spawnPosZ; 
    }

    void MoveChunks() // chunkları hareket ettir
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * moveSpeed * Time.deltaTime); // chunkları ileri doğru hareket ettir

            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength) // chunkın pozisyonu belirli bir değerin altına düşerse
            {  

                chunks.RemoveAt(i); // chunkı listeden çıkar
                Destroy(chunk); // chunkı yok et
                SpawnChunks(); // yeni bir chunk oluştur
                i--; // chunklar listeden çıkarıldığı için indexi bir azalt
            }
        }
    }
    
}
