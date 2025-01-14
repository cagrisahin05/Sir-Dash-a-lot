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
            GameObject coin = Instantiate(coinPrefab, chunkParent);
            coin.SetActive(false);
            coinPool.Add(coin);

            GameObject cake = Instantiate(cakePrefab, chunkParent);
            cake.SetActive(false);
            cakePool.Add(cake);

            GameObject fence = Instantiate(fencePrefab, chunkParent);
            fence.SetActive(false);
            fencePool.Add(fence);
        }
    }

    public GameObject GetCoin()
    {
        foreach (GameObject coin in coinPool)
        {
            if (!coin.activeInHierarchy)
            {
                coin.SetActive(true);
                return coin;
            }
        }

        // Eğer havuzda aktif olmayan coin kalmadıysa yeni bir coin oluştur
        GameObject newCoin = Instantiate(coinPrefab, chunkParent);
        newCoin.SetActive(false);
        coinPool.Add(newCoin);
        return newCoin;
    }

    public GameObject GetCake()
    {
        foreach (GameObject cake in cakePool)
        {
            if (!cake.activeInHierarchy)
            {
                cake.SetActive(true);
                return cake;
            }
        }

        // Eğer havuzda aktif olmayan cake kalmadıysa yeni bir cake oluştur
        GameObject newCake = Instantiate(cakePrefab, chunkParent);
        newCake.SetActive(false);
        cakePool.Add(newCake);
        return newCake;
    }

    public GameObject GetFence()
    {
        foreach (GameObject fence in fencePool)
        {
            if (!fence.activeInHierarchy)
            {
                fence.SetActive(true);
                return fence;
            }
        }

        // Eğer havuzda aktif olmayan fence kalmadıysa yeni bir fence oluştur
        GameObject newFence = Instantiate(fencePrefab, chunkParent);
        newFence.SetActive(false);
        fencePool.Add(newFence);
        return newFence;
    }

    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
    }

    public void ReturnCake(GameObject cake)
    {
        cake.SetActive(false);
    }

    public void ReturnFence(GameObject fence)
    {
        fence.SetActive(false);
    }
}