using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    GameState currentState = null;
    public GameStateMachine gameStateMachine;
    public PlayerLevelManager playerLevelManager;
    public MotherboardConfirmButton motherboardConfirmButton;
    public PlayUIManager playUIManager;
    public float timeElapsed = 0;

    void Awake()
    {
        if (gameStateMachine == null) gameStateMachine = FindAnyObjectByType<GameStateMachine>();
        if (playerLevelManager == null) playerLevelManager = FindAnyObjectByType<PlayerLevelManager>();
        if (motherboardConfirmButton == null) motherboardConfirmButton = FindAnyObjectByType<MotherboardConfirmButton>();
        if (playUIManager == null) playUIManager = FindAnyObjectByType<PlayUIManager>();
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

    void HandleLevelUp(float nextLevelThreshold)
    {
        gameStateMachine.ChangeGameState(new MotherboardState(gameStateMachine));
    }

    void HandleConfirmButtonClicked()
    {
        gameStateMachine.ChangeGameState(new PlayingState(gameStateMachine));
    }

}
