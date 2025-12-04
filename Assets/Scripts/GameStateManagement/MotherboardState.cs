using UnityEngine;
public class MotherboardState : GameState
{
    public MotherboardState(GameStateMachine context) : base(context) { }
    public override void EnterState()
    {
        base.EnterState();
        context.motherboardCanvas.gameObject.SetActive(true);
        context.componentSelectionUIManager.PopulateMenuWithComponents();
    }
    public override void ExitState()
    {
        base.ExitState();
        context.motherboardGrid.ReloadMotherboard();
        context.motherboardCanvas.gameObject.SetActive(false);
        context.componentSelectionUIManager.ClearMenu();
    }
    public override void Tick()
    {
        base.Tick();

    }

}