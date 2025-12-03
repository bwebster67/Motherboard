using UnityEngine;
public class MotherboardState : GameState
{
    public MotherboardState(GameStateMachine context) : base(context) { }
    public override void EnterState()
    {
        base.EnterState();
        context.motherboardCanvas.gameObject.SetActive(true);
    }
    public override void ExitState()
    {
        base.ExitState();
        context.motherboardGrid.ReloadMotherboard();
        context.motherboardCanvas.gameObject.SetActive(false);
    }
    public override void Tick()
    {
        base.Tick();

    }

}