using System;
using UnityEngine;
public class IdleState : State
{
    public override State ExecuteState(PlayerController player)
    {
        #region HeadRotation
        player.playerAnimation.ChangeDirection(!player.movingForward);
        float rotationAmount = player.neckRotationSpeed * Time.deltaTime;

        if (player.movingForward)
            player.playerAnimation.VectorToParameterRotation(player.currentAxis, new Vector2(-player.currentAxis.y, player.currentAxis.x), rotationAmount, "Look");
        else
            player.playerAnimation.VectorToParameterRotation(player.currentAxis, new Vector2(player.currentAxis.y, -player.currentAxis.x), rotationAmount, "Look");
        #endregion

        #region SetWalkAnimationSpeed
        if (player.facingForward)
        {
            float dotProduct = player.currentAxis.x * -player.playerMovement.GetVelocityY() / player.baseSpeed + player.currentAxis.y * player.playerMovement.GetVelocityX() / player.baseSpeed;
            player.playerAnimation.SetAnimatorFloat("Speed", dotProduct * -1);
        }
        else
        {
            float dotProduct = player.currentAxis.x * -player.playerMovement.GetVelocityY() / player.baseSpeed + player.currentAxis.y * player.playerMovement.GetVelocityX() / player.baseSpeed;
            player.playerAnimation.SetAnimatorFloat("Speed", dotProduct);
        }
        #endregion

        if (player.isMoving)
            return player.walkingState;
        else if (player.isFocused)
            return player.focusedState;
        else
            return player.idleState;
    }
    public override void ExecuteStatePhysics(PlayerController player)
    {
        player.currentSpeed = Mathf.Lerp(player.currentSpeed, 0f, player.accelerationSpeed);
        player.playerMovement.AddForce(player.currentAxis, -player.groundingForce);
    }
}
