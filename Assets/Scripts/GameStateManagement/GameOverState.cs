using UnityEngine;
public class GameOverState : GameState
{
    public GameOverState(GameStateMachine context) : base(context) {}
    public override void EnterState()
    {
        base.EnterState();
        context.player.GetComponent<Renderer>().enabled = false;
        context.gameOverMenu.gameObject.SetActive(true);
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