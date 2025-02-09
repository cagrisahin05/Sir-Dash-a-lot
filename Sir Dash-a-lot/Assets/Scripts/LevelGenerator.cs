using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] ObjectPool chunkPool; // chunkları tutacak object pool
    [SerializeField] float chunkLength = 10f; // chunk uzunluğu
    [SerializeField] float moveSpeed = 8f; // chunkların hareket hızı
    [SerializeField] float minMoveSpeed = 2f;
    GameObject tempoLevelObject; // tempo cache için gameobject tanımlama
    private float currentTime = 0f;
    private bool isGenerating = false;
    List <GameObject> activeChunks = new List<GameObject>(); // chunkları tutacak liste
    private Coroutine generateRoutine; // coroutine tanımlama

    // chunkların hareket hızını değiştirmek için kullanılacak değişken

    private void Update() 
    {
        if (!isGenerating) return;
        
        currentTime += Time.deltaTime;
        if (currentTime >= chunkLength / moveSpeed)
        {
            SpawnChunks();
            currentTime = 0f;
        }

        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount) // chunkların hareket hızını değiştir

    {
        moveSpeed += speedAmount;
        if (moveSpeed < minMoveSpeed) moveSpeed = minMoveSpeed;
    }

    
      public void StartGenerating() 
    {
        isGenerating = true;
        for (int i = 0; i < 12; i++)
        {
            SpawnChunks();
        }
    }

    public void StopGenerating()
    {
       isGenerating = false;
    }


    void SpawnChunks()
    {      
        tempoLevelObject = chunkPool.GetChunk(); // object pool'dan chunk al
        tempoLevelObject.transform.position = new Vector3(0, 0, SpawnPosZCalculator()); // chunkın pozisyonunu belirle
        tempoLevelObject.SetActive(true); // chunkı aktif et
        activeChunks.Add(tempoLevelObject); // chunkı listeye ekle
        
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
            tempoLevelObject = activeChunks[i];
            tempoLevelObject.transform.Translate(-transform.forward * moveSpeed * Time.deltaTime); // chunkları ileri doğru hareket ettir

            if(tempoLevelObject.transform.position.z <= Camera.main.transform.position.z - chunkLength) // chunkın pozisyonu belirli bir değerin altına düşerse
            {  

                activeChunks.RemoveAt(i); // chunkı listeden çıkar
                chunkPool.ReturnObject(tempoLevelObject); // chunkı object pool'a geri döndür
                SpawnChunks(); // yeni bir chunk oluştur
                i--; // chunklar listeden çıkarıldığı için indexi bir azalt
            }
        }
    }
    
}
