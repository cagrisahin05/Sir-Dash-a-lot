using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject FencePrefab; // prefablarını tutmak için
    [SerializeField] float[] lanes = { -3f, 0f, 1.5f }; // Lane'lerin konumlarını tutmak için
    void Start()
    {
        FenceSpawn();
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

            int randomLaneIndex = Random.Range(0, availableLanes.Count); // Lane'lerin indexlerinden rastgele birini seçmek için
            int selectedLane = availableLanes[randomLaneIndex]; // Seçilen lane'ı belirleme
            availableLanes.RemoveAt(randomLaneIndex); // Seçilen lane'ı listeden çıkarma

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z); // Rastgele bir konum belirleme
            Instantiate(FencePrefab, spawnPosition, Quaternion.identity, transform); // Fence oluşturma
            
        }
      
      
    }
   
}
