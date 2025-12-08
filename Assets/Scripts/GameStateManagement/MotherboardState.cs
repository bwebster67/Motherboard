using UnityEngine;
public class MotherboardState : GameState
{
    public MotherboardState(GameStateMachine context) : base(context) { }
    public override void EnterState()
    {
        base.EnterState();
        context.motherboardCanvas.gameObject.SetActive(true);
        context.componentSelectionUIManager.PopulateMenuWithComponents();
        Time.timeScale = 0;
    }
    public override void ExitState()
    {
        base.ExitState();
        context.motherboardGrid.ReloadMotherboard();
        context.motherboardCanvas.gameObject.SetActive(false);
        context.componentSelectionUIManager.ClearMenu();
        Time.timeScale = 1;
    }
    public override void Tick()
    {
        base.Tick();

    }

}