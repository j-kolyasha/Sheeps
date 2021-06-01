using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public event UnityAction OnNextLevel;

    public static GameManager Instance { get; private set; }

    public GameState GameState { get; private set; }
    public int Score { get; private set; }

    public void SetGameState(GameState newState)
    {
        GameState = newState;
        switch (GameState)
        {
            case GameState.Menu:

                break;
            case GameState.Game:

                break;
            case GameState.Pause:

                break;
        }
    }
    public void StartGame()
    {

    }
    public void ExitToMainMenu()
    {

    }
    public void Pause()
    {
        SetGameState(GameState.Pause);
    }
    public void UnPause()
    {

    }
    public void NextLevel()
    {
        OnNextLevel?.Invoke();
    }
    private void Start()
    {
        OnNextLevel?.Invoke();
        this.GameState = GameState.Game;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public enum GameState
{
    Menu,
    Game,
    Pause,
}