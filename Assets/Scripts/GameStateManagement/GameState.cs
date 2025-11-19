public abstract class GameState
{
    protected readonly GameStateMachine context;
    public GameState(GameStateMachine context)
    {
        this.context = context;
    }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void Tick() { }
}