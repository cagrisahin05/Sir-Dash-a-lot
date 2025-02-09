using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton pattern ekledim
    public UnityEvent OnGameStart; // Oyun başladığında çağrılacak event
    public UnityEvent OnGameEnd;
    public GameState currentState; // Oyun durumu
    
    public enum GameState
    {
        AnaMenu,
        Oyun,
        Duraklama,
        OyunBitti
    }
    private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
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
        SetGameState(GameState.Oyun); // Oyun durumunu başlat
        
        OnGameStart.Invoke(); // Oyun başladığında çağrıl
        
    }

    public void EndGame()
    {
        SetGameState(GameState.OyunBitti); // Oyun durumunu bitir
        
        OnGameEnd.Invoke();
    }
     public void RestartGame()
    {
        // Oyunu yeniden başlatmak için gerekli işlemler
        EndGame();
        StartGame();
    }
    private void SetGameState(GameState newState) 
    {
        currentState = newState;
        // Oyun durumu değiştiğinde yapılacak işlemler
        Debug.Log("Game State changed to: " + currentState);
    }


}

