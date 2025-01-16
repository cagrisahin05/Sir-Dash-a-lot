using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton pattern ekledim
    public ChunkPool chunkPool; // ChunkPool scriptini tutacak
    public UnityEvent OnGameStart; // Oyun başladığında çağrılacak event
    public UnityEvent OnGameEnd;
    public GameState gameState = GameState.AnaMenu; // Oyunun durumunu tutacak
    
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
        //levelGenerator.StartGenerating(); // LevelGenerator scriptini aktif et
        OnGameStart.Invoke(); // Oyun başladığında çağrıl
        
    }

    public void EndGame()
    {
        //levelGenerator.StopGenerating(); // LevelGenerator scriptini durdur
        OnGameEnd.Invoke();
    }
     public void RestartGame()
    {
        // Oyunu yeniden başlatmak için gerekli işlemler
        EndGame();
        StartGame();
    }


}

public enum GameState
{
    AnaMenu,
    Oyun,
    Duraklama,
    OyunBitti
}
