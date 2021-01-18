using System;
using UnityEngine;

public class IdleState : State
{
    private float deceleration;
    private float maxSpeed;
    public IdleState(StateMachine stateMachine) : base(stateMachine)
    {
        ;
    }
    public override void EnterState()
    {
        deceleration = MasterMachine.Data.accelerationSpeed;
        maxSpeed = MasterMachine.Data.walkingSpeed;
    }

    public override void ExitState()
    {
        
    }

    public override void LogicUpdate()
    {
        MasterMachine.Animator.SetFloat("Speed", MasterMachine.Physics.GetClampedVelocity(maxSpeed));

        float rotation;
        if (MasterMachine.Data.FacingForward)
            rotation = MasterMachine.Animator.VectorToParameterRotation(MasterMachine.Data.CurrentAxis, new Vector2(-1, 0f), MasterMachine.Data.neckRotationSpeed, "Look");
       else
            rotation = MasterMachine.Animator.VectorToParameterRotation(MasterMachine.Data.CurrentAxis, new Vector2(1, 0f), MasterMachine.Data.neckRotationSpeed, "Look");

        MasterMachine.Animator.SetFloat("Look", rotation);


        if (MasterMachine.Data.IsMoving)
            MasterMachine.ChangeState(MasterMachine.walkingState);
    }

    public override void PhysicsUpdate()
    {        
        MasterMachine.Physics.SetVelocity(MasterMachine.Data.CurrentAxis, 0f, deceleration, MasterMachine.Data.MovingForward);
        MasterMachine.Physics.ApplyForce(-MasterMachine.Data.CurrentAxis, MasterMachine.Data.groundingForce);
    }
}
