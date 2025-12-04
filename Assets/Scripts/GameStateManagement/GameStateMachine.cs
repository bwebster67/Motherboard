using Unity.VisualScripting;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    GameState currentState = null;
    public Canvas motherboardCanvas;
    public MotherboardGrid motherboardGrid;
    public ComponentSelectionUIManager componentSelectionUIManager;

    void Awake()
    {
        if (motherboardGrid == null) motherboardGrid = FindAnyObjectByType<MotherboardGrid>();
        if (componentSelectionUIManager == null) componentSelectionUIManager = FindAnyObjectByType<ComponentSelectionUIManager>();
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
    }

    void Update()
    {
        currentState.Tick();
    }
}

    

