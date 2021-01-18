using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // private vars
    private Rigidbody2D rbody;

    // gizmos vars
    private Vector3 walkDirection;
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    public Vector2 GetVelocity()
    {
        return rbody.velocity;
    }
    public float GetVelocityX()
    {
        return rbody.velocity.x;
    }
    public float GetVelocityY()
    {
        return rbody.velocity.y;
    }

    public void SetVelocity(Vector2 forward, float speed)
    {
        Vector2 direction;
        direction = new Vector2(speed * forward.x, speed * forward.y);

        rbody.velocity = direction;

        walkDirection = direction.normalized;
    }
    
    public void SetGravityScale(float scale)
    {
        rbody.gravityScale = scale;
    }
    public void AddForce(Vector2 direction, float force)
    {
        rbody.AddForce(direction * force);
    }    
    private void OnDrawGizmos()
    {
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //mousePosition.Normalize();
        ////mousePosition = mousePosition.normalized;
        ////Debug.Log(mousePosition.magnitude);
        //Gizmos.DrawWireSphere(mousePosition + new Vector2(transform.position.x , transform.position.y), 1f);

        

        //Gizmos.color = Color.magenta;
        //Gizmos.DrawRay(transform.position, walkDirection);

    }
}