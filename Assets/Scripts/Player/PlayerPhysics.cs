using UnityEngine;
public class PlayerPhysics
{
    Rigidbody2D RB;
    public PlayerPhysics(Rigidbody2D rbody)
    {
        RB = rbody;
    }
    public void SetVelocity(Vector2 axis, float targetVelocity, float accelerationSpeed, bool movingRight)
    {
        float currentSpeed = Mathf.Lerp(RB.velocity.magnitude, targetVelocity, accelerationSpeed * Time.deltaTime);

        if (movingRight)
        {
            Debug.Log("moving Right");
            RB.velocity = new Vector2(-axis.y, axis.x) * currentSpeed;
        }
        else
        {
            RB.velocity = new Vector2(axis.y, -axis.x) * currentSpeed;
        }
    }
    public void ApplyForce(Vector2 direction, float force)
    {
        RB.AddForce(direction * force);
    }
    public float GetClampedVelocity(float maxVelocity)
    {
        return Mathf.Clamp(RB.velocity.magnitude, 0f, maxVelocity);
    }
}