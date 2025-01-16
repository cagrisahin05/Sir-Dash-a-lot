using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChunkPool : MonoBehaviour
{
    [SerializeField] Transform chunkParent; // chunkların parentı
    [SerializeField] int poolSize = 10; // havuzdaki nesne sayısı
    [SerializeField] GameObject coinPrefab; // coin prefabı
    [SerializeField] GameObject cakePrefab; // cake prefabı
    [SerializeField] GameObject fencePrefab; // fence prefabı
    GameObject tempoPickUpObject; // tempo cache için gameobject tanımlama

    private List<GameObject> coinPool; // coinleri tutacak liste
    private List<GameObject> cakePool; // cakeleri tutacak liste
    private List<GameObject> fencePool; // fence'leri tutacak liste

    private void Awake()
    {
        coinPool = new List<GameObject>(poolSize);
        cakePool = new List<GameObject>(poolSize);
        fencePool = new List<GameObject>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            tempoPickUpObject = Instantiate(coinPrefab, chunkParent);
            tempoPickUpObject.SetActive(false);
            coinPool.Add(tempoPickUpObject);

            tempoPickUpObject = Instantiate(cakePrefab, chunkParent);
            tempoPickUpObject.SetActive(false);
            cakePool.Add(tempoPickUpObject);

            tempoPickUpObject = Instantiate(fencePrefab, chunkParent);
            tempoPickUpObject.SetActive(false);
            fencePool.Add(tempoPickUpObject);
        }
    }

    public GameObject GetCoin()
    {
        foreach (GameObject coin in coinPool)
        {
            if (!coin.activeInHierarchy)
            {
                this.tempoPickUpObject = coin;
                this.tempoPickUpObject.SetActive(true);
                return this.tempoPickUpObject;
            }
        }

        // Eğer havuzda aktif olmayan coin kalmadıysa yeni bir coin oluştur
        tempoPickUpObject = Instantiate(coinPrefab, chunkParent);
        tempoPickUpObject.SetActive(false);
        coinPool.Add(tempoPickUpObject);
        return tempoPickUpObject;
    }

    public GameObject GetCake()
    {
        foreach (GameObject cake in cakePool)
        {
            if (!cake.activeInHierarchy)
            {   
                this.tempoPickUpObject = cake;
                this.tempoPickUpObject.SetActive(true);
                return this.tempoPickUpObject;
            }
        }

        // Eğer havuzda aktif olmayan cake kalmadıysa yeni bir cake oluştur
        tempoPickUpObject = Instantiate(cakePrefab, chunkParent);
        tempoPickUpObject.SetActive(false);
        cakePool.Add(tempoPickUpObject);
        return tempoPickUpObject;
    }

    public GameObject GetFence()
    {
        foreach (GameObject fence in fencePool)
        {
            if (!fence.activeInHierarchy)
            {
                tempoPickUpObject = fence;
                tempoPickUpObject.SetActive(true);
                return tempoPickUpObject;
            }
        }

        // Eğer havuzda aktif olmayan fence kalmadıysa yeni bir fence oluştur
        tempoPickUpObject = Instantiate(fencePrefab, chunkParent);
        tempoPickUpObject.SetActive(false);
        fencePool.Add(tempoPickUpObject);
        return tempoPickUpObject;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }

   
}