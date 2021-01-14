using UnityEngine;

public class FocusedWalkingState : State
{
    public override State ExecuteState(PlayerController player)
    {
        #region headRotation
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

        if (!player.isFocused)
            return player.walkingState;
        else if (!player.isMoving)                 
            return player.focusedState;        
        else
            return player.focusedWalkingState;        
    }

    public override void ExecuteStatePhysics(PlayerController player)
    {
        player.currentSpeed = Mathf.Lerp(player.currentSpeed, player.focusSpeed, player.accelerationSpeed);

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
