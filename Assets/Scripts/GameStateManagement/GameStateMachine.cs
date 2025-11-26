using Unity.VisualScripting;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    GameState currentState = null;
    public Canvas motherboardCanvas;
    public MotherboardGrid motherboardGrid;

    void Awake()
    {
        if (motherboardGrid == null) motherboardGrid = FindAnyObjectByType<MotherboardGrid>();
    }

    public void ChangeGameState(GameState state)
    {
        currentState?.ExitState();
        currentState = state;
        state?.EnterState();
    }

    void Start()
    {
        ChangeGameState(new PlayingState(this));
    }

    void Update()
    {
        currentState.Tick();
    }
}

    

