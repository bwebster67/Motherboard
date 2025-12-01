using UnityEngine;
public class PlayingState : GameState
{
    public PlayingState(GameStateMachine context) : base(context) { }
    public override void EnterState()
    {
        base.EnterState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void Tick()
    {
        base.Tick();

        // For testing only
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     context.ChangeGameState(new MotherboardState(context));
        // }
    }
}