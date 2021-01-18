
public abstract class State
{
    public StateMachine MasterMachine { get; private set;}   
    public State(StateMachine stateMachine)
    {
        MasterMachine = stateMachine;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();   
}
