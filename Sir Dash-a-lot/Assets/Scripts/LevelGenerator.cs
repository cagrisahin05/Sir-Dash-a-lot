using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab; // chunk prefabı
    [SerializeField] int startingChunkAmount = 12; // baştaki chunk sayısı
    [SerializeField] Transform chunkParent; // chunkların parentı
    [SerializeField] float chunkLength = 10f; // chunk uzunluğu
    void Start()
    {
        for (int i = 0; i < startingChunkAmount; i++) // başlangıçta belirtilen sayıda chunk oluştur
        {
            float spawnPosZ;
            if (i == 0) // ilk chunkın pozisyonunu belirle
            {
                spawnPosZ = transform.position.z;
            }
            else // diğer chunkların pozisyonunu belirle
            {
                spawnPosZ = transform.position.z + (chunkLength * i);
            }


            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, spawnPosZ); // chunkın pozisyonunu belirle
            Instantiate(chunkPrefab, spawnPos, Quaternion.identity, chunkParent);
        }
        
    }

    
    
}
