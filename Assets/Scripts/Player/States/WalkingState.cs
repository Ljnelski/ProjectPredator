using UnityEngine;

public class WalkingState : State
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

        if (player.isFocused)
            return player.focusedWalkingState;
        else if (!player.isMoving)
            return player.idleState;
        else
            return player.walkingState;
    }
    public override void ExecuteStatePhysics(PlayerController player)
    {
        player.currentSpeed = Mathf.Lerp(player.currentSpeed, player.baseSpeed, player.accelerationSpeed);

        if (player.movingForward)
        {
            player.playerMovement.SetVelocity(new Vector2(-player.currentAxis.y, player.currentAxis.x), player.currentSpeed);
            player.facingForward = true;
        }
        else
        {
            player.playerMovement.SetVelocity(new Vector2(player.currentAxis.y, -player.currentAxis.x), player.currentSpeed);
            player.facingForward = false;
        }
        player.playerMovement.AddForce(player.currentAxis, -player.groundingForce);
    }
}

