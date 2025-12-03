using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    GameState currentState = null;
    public GameStateMachine gameStateMachine;
    public PlayerLevelManager playerLevelManager;
    public MotherboardConfirmButton motherboardConfirmButton;

    void Awake()
    {
        gameStateMachine = FindAnyObjectByType<GameStateMachine>();
        playerLevelManager = FindAnyObjectByType<PlayerLevelManager>();
        motherboardConfirmButton = FindAnyObjectByType<MotherboardConfirmButton>();
    }
    void OnEnable()
    {
        playerLevelManager.OnLevelUp += HandleLevelUp;
        motherboardConfirmButton.OnConfirmButtonClicked += HandleConfirmButtonClicked;
    }

    void OnDisable()
    {
        playerLevelManager.OnLevelUp -= HandleLevelUp;
        motherboardConfirmButton.OnConfirmButtonClicked -= HandleConfirmButtonClicked;
    }

    void Start()
    {
        gameStateMachine.ChangeGameState(new PlayingState(gameStateMachine));
    }

    void HandleLevelUp(int newLevel)
    {
        gameStateMachine.ChangeGameState(new MotherboardState(gameStateMachine));
    }

    void HandleConfirmButtonClicked()
    {
        gameStateMachine.ChangeGameState(new PlayingState(gameStateMachine));
    }

}
