
public class StateMachine
{
    public PlayerData Data;
    public PlayerPhysics Physics;
    public PlayerAnimation Animator;

    public State CurrentState { get; set; }

    public IdleState idleState;
    public WalkingState walkingState;

    public StateMachine(PlayerData data, PlayerPhysics physics, PlayerAnimation animator)
    {
        Data = data;
        Physics = physics;
        Animator = animator;

        idleState = new IdleState(this);
        walkingState = new WalkingState(this);
    }
    public void ChangeState(State newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
   
