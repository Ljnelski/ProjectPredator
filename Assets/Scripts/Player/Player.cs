using UnityEngine;

public class Player : MonoBehaviour
{
    StateMachine stateMachine;
    PlayerData Data;

    IdleState IdleState;

    void Start()
    {
        Rigidbody2D targetRigidBody2D = GetComponent<Rigidbody2D>();
        Animator targetAnimator = transform.GetChild(0).GetComponent<Animator>();

        stateMachine = new StateMachine(GetComponent<PlayerData>(), new PlayerPhysics(targetRigidBody2D), new PlayerAnimation(targetAnimator));
        
        IdleState = new IdleState(stateMachine);
        stateMachine.CurrentState = IdleState;
        stateMachine.CurrentState.EnterState();
    }
    // Update is called once per frame
    private void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
}
