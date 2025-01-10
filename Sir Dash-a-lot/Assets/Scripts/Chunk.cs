using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject FencePrefab; // prefablarını tutmak için
    [SerializeField] float[] lanes = { -4f, 0f, 4f }; // Lane'lerin konumlarını tutmak için
    void Start()
    {
        FenceSpawn();
    }

    void FenceSpawn()
    {
      
        int randomLane = Random.Range(0, lanes.Length); // Arrayden rastgele bir lane seçmek için
        Vector3 spawnPosition = new Vector3(lanes[randomLane], transform.position.y, transform.position.z); // Rastgele bir konum belirleme
        Instantiate(FencePrefab, spawnPosition, Quaternion.identity, transform); // Fence oluşturma
      
    }
   
}
