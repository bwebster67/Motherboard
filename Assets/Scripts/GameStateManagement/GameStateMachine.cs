using Unity.VisualScripting;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    GameState currentState = null;
    public Canvas motherboardCanvas;
    public MotherboardGrid motherboardGrid;
    public ComponentSelectionUIManager componentSelectionUIManager;
    public Canvas gameOverMenu;
    public GameObject player;
    public static bool debug = true; // when true, enables debug keybindings

    void Awake()
    {
        if (motherboardGrid == null) motherboardGrid = FindAnyObjectByType<MotherboardGrid>();
        if (componentSelectionUIManager == null) componentSelectionUIManager = FindAnyObjectByType<ComponentSelectionUIManager>();
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnEnable()
    {
        player.GetComponent<PlayerHealth>().OnPlayerDied += HandlePlayerDied;
    }

    void OnDisable()
    {
        player.GetComponent<PlayerHealth>().OnPlayerDied -= HandlePlayerDied;
    }

    public void ChangeGameState(GameState state)
    {
        currentState?.ExitState();
        currentState = state;
        state?.EnterState();
    }

    void Start()
    {
        ChangeGameState(new MotherboardState(this));
        // no component choice on start screen
        componentSelectionUIManager.ClearMenu();
    }

    void Update()
    {
        currentState.Tick();
    }

    void HandlePlayerDied()
    {
        ChangeGameState(new GameOverState(this));
    }
}


