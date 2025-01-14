using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] Transform chunkParent; // chunkların parentı
    public GameObject chunkPrefab; 
    public int poolSize = 20;
    private List<GameObject> chunkPool;

     private void Start() 
        
    {
       
        CreateChunkPool();
      
    }
    public void CreateChunkPool()
    {
        chunkPool = new List<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject chunk = Instantiate(chunkPrefab, transform.position, Quaternion.identity, chunkParent);
            chunk.SetActive(false);
            chunkPool.Add(chunk);
        }
    }

    // Havuzdan chunk almak için
public GameObject GetChunk()

{
     // Havuzda aktif olmayan chunklar için foreach döngüsü oluşturuyorum
    foreach (GameObject chunk in chunkPool)
    {
        if (!chunk.activeInHierarchy) // Eğer chunk aktif değilse
        {
        chunk.SetActive(true);
        return chunk;
        }
    }

    // Havuzda chunk kalmadıysa yeni chunk oluşturuyorum
    GameObject newChunk = Instantiate(chunkPrefab, transform.position, Quaternion.identity, chunkParent);
    chunkPool.Add(newChunk);
    return newChunk;
}
   
    // Havuza chunk geri döndürmek için
    public void ReturnObject(GameObject chunk)
    {
        chunk.SetActive(false);
        
    }
    
}
