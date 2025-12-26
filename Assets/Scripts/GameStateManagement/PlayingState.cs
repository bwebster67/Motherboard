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
        context.gameManager.timeElapsed += Time.deltaTime;
        float time = context.gameManager.timeElapsed;

        int minutes = Mathf.FloorToInt(time / 60F); 
        int seconds = Mathf.FloorToInt(time % 60F); 

        // "{0:00}:{1:00}" formats minutes (index 0) and seconds (index 1) to always have two digits, adding a leading zero if necessary.
        context.gameManager.playUIManager.timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}