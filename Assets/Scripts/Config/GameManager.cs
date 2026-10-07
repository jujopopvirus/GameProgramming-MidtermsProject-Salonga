using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action<int> OnScoreChanged;
    public static event Action OnPauseObjects;
    public static event Action OnGameOver;

    public PlayerBaseScript player;
    public enum GameState
    {
        Active,
        Paused,
        GameOver
    }
    public int currentScore = 0;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void addScore(int count) 
    {
        currentScore += count;
        OnScoreChanged?.Invoke(currentScore);
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        if (player != null) Destroy(player.gameObject);
    }

    public void Pause()
    {
        OnPauseObjects?.Invoke();
    }

    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;

        string ActiveScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(ActiveScene);
    }
}
