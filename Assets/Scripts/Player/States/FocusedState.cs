using UnityEngine;

public class FocusedState : State
{
    public override State ExecuteState(PlayerController player)
    {
        #region HeadRotation
        Vector2 targetRotation = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        player.facingForward = player.playerAnimation.VectorRightOfAxis(player.currentAxis, targetRotation);
        player.playerAnimation.VectorToParameterRotation(player.currentAxis, targetRotation, player.neckRotationSpeed * Time.deltaTime, "Look");
        player.playerAnimation.ChangeDirection(!player.facingForward);
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
            return player.focusedWalkingState;
        else if (!player.isFocused && !player.isMoving)
            return player.idleState;
        else
            return player.focusedState;        
    }

    public override void ExecuteStatePhysics(PlayerController player)
    {
        player.currentSpeed = Mathf.Lerp(player.currentSpeed, 0f, player.accelerationSpeed);
        player.playerMovement.AddForce(player.currentAxis, -player.groundingForce);
    }
}