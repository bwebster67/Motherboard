using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    GameState currentState = null;
    public GameStateMachine gameStateMachine;

    void Start()
    {
        gameStateMachine.ChangeGameState(new PlayingState(gameStateMachine));
    }

    void Update()
    {
        
    }
}
