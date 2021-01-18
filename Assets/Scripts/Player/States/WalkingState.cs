using System;
using UnityEngine;

public class WalkingState : State
{    
    public WalkingState(StateMachine stateMachine) : base(stateMachine)
    {
        ;
    }
    public override void EnterState()
    {
        Debug.Log("EnteredwalkingState");
    }

    public override void ExitState()
    {
        ;
    }

    public override void LogicUpdate()
    {        
        MasterMachine.Animator.SetFloat("Speed", MasterMachine.Physics.GetClampedVelocity(MasterMachine.Data.walkingSpeed));      

        if (!MasterMachine.Data.IsMoving)
            MasterMachine.ChangeState(MasterMachine.idleState);
    }

    public override void PhysicsUpdate()
    {
        MasterMachine.Physics.SetVelocity(MasterMachine.Data.CurrentAxis, MasterMachine.Data.walkingSpeed, MasterMachine.Data.accelerationSpeed, MasterMachine.Data.MovingForward);
        MasterMachine.Physics.ApplyForce(-MasterMachine.Data.CurrentAxis, MasterMachine.Data.groundingForce);
    }
}
