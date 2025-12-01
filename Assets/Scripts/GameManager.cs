using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    GameState currentState = null;
    public GameStateMachine gameStateMachine;
    public PlayerLevelManager playerLevelManager;

    void Awake()
    {
        gameStateMachine = FindAnyObjectByType<GameStateMachine>();
        playerLevelManager = FindAnyObjectByType<PlayerLevelManager>();
    }
    void OnEnable()
    {
        playerLevelManager.OnLevelUp += HandleLevelUp;
    }

    void OnDisable()
    {
        playerLevelManager.OnLevelUp -= HandleLevelUp;
    }

    void Start()
    {
        gameStateMachine.ChangeGameState(new PlayingState(gameStateMachine));
    }

    void HandleLevelUp(int newLevel)
    {
        gameStateMachine.ChangeGameState(new MotherboardState(gameStateMachine));
    }

}
