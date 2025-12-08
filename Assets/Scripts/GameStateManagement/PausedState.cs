using UnityEngine;
public class PausedState : GameState
{
    public PausedState(GameStateMachine context) : base(context) {}
    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0;
    }
    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1;
    }
    public override void Tick()
    {
        base.Tick();
    }
    
}