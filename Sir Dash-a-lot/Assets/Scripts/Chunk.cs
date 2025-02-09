using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] float coinSpawnChance = 0.5f; // Coin'in spawn olma şansı
    [SerializeField] float cakeSpawnChance = 0.5f; // Cake'in spawn olma şansı
    [SerializeField] float coinSeperationLenght = 2f; // Coin'lerin ard arda gelme mesafesi
    GameObject tempoChunkObject; // tempo cache için gameobject tanımlama
    [SerializeField] float[] lanes = { -3f, 0f, 1.5f }; // Lane'lerin konumlarını tutmak için
    private List<int> availableLanes; // Lane'leri tutmak için
    void Start()
    {
        availableLanes = new List<int> { 0, 1, 2 }; // Lane'lerin indexlerini tutmak için
        FenceSpawn();
        SpawnCake();
        SpawnCoin();
    }

    void FenceSpawn()
    {   
        List<int> availableLanes = new List<int> { 0, 1, 2 }; // Lane'lerin indexlerini tutmak için
        int fencesToSpawn = Random.Range(0, lanes.Length); // Oluşturulacak fence sayısını belirleme

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count == 0) // Eğer lane kalmadıysa döngüyü sonlandır
            {
                break;
            }
            int selectedLane = SelectLane(); // Lane seçme
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z); // Rastgele bir konum belirleme
            tempoChunkObject = ChunkPool.Instance.GetFence(); // Havuzdan fence al
            tempoChunkObject.transform.position = spawnPosition;
            tempoChunkObject.transform.parent = transform; // Fence'i chunk'ın child'ı yap
            tempoChunkObject.SetActive(true);
           
            
        } 
    }
   void SpawnCake()
    {

        if(Random.value > cakeSpawnChance || availableLanes.Count<= 0) return; // Eğer spawn şansı düşükse veya lane kalmadıysa döngüyü sonlandır
    
        int selectedLane = SelectLane(); // Lane seçme

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z); // Rastgele bir konum belirleme
        tempoChunkObject = ChunkPool.Instance.GetCake(); // Havuzdan cake al
        tempoChunkObject.transform.position = spawnPosition;
        tempoChunkObject.transform.parent = transform; // Cake'i chunk'ın child'ı yap
        tempoChunkObject.SetActive(true);
        
    }
    void SpawnCoin()
    {
        if(Random.value > coinSpawnChance || availableLanes.Count<= 0) return; // Eğer spawn şansı düşükse veya lane kalmadıysa döngüyü sonlandır
    
        int selectedLane = SelectLane(); // Lane seçme

        int maxCoinCount = 6; // Oluşturulacak coin sayısını belirleme
        int coinToSpawn = Random.Range(1, maxCoinCount); // Oluşturulacak coin sayısını belirleme

        float topOfChunkPosZ = transform.position.z + (coinSeperationLenght * 2f); // Coin'lerin spawn olacağı pozisyonu belirleme

        for (int i = 0; i < coinToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkPosZ - (coinSeperationLenght * i); // Coin'lerin arkasında olacak şekilde pozisyon belirleme
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ); // Rastgele bir konum belirleme
            tempoChunkObject = ChunkPool.Instance.GetCoin(); // Havuzdan coin al
            tempoChunkObject.transform.position = spawnPosition; 
            tempoChunkObject.transform.parent = transform; // Coin'i chunk'ın child'ı yap
            tempoChunkObject.SetActive(true);
        }
        
    }
    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count); // Lane'lerin indexlerinden rastgele birini seçmek için
        int selectedLane = availableLanes[randomLaneIndex]; // Seçilen lane'ı belirleme
        availableLanes.RemoveAt(randomLaneIndex); // Seçilen lane'ı listeden çıkarma
        return selectedLane; // Seçilen lane'ı döndürme
    }
}
