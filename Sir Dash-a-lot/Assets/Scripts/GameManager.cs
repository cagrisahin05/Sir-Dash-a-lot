using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton pattern ekledim
    public LevelGenerator levelGenerator; // LevelGenerator scriptini tutacak
    public ObstacleSpawner obstacleSpawner; // ObstacleSpawner scriptini tutacak
    public ChunkPool chunkPool; // ChunkPool scriptini tutacak
    
    private void Awake() // Singleton pattern ekledim
     {
        if (Instance == null) 
        {
            Instance = this; 
        } else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        levelGenerator.StartGenerating(); // LevelGenerator scriptini aktif et
        obstacleSpawner.StartSpawning(); // ObstacleSpawner scriptini aktif et
    }

    public void EndGame()
    {
        levelGenerator.StopGenerating(); // LevelGenerator scriptini durdur
        obstacleSpawner.StopSpawning(); // ObstacleSpawner scriptini durdur
    }
     public void RestartGame()
    {
        // Oyunu yeniden başlatmak için gerekli işlemler
        EndGame();
        StartGame();
    }


}
