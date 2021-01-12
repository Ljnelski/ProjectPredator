using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // private vars
    private Rigidbody2D rbody;
    private Animator anim;

    // gizmos vars
    Vector3 walkDirection;
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>(); // This is to grab the animator from the Creature
    }
    public void DoWalk(Vector2 forward, float speed)
    {
        Vector2 direction;
        direction = new Vector2(speed * forward.x, speed * forward.y);       

        rbody.velocity = direction;
       
        walkDirection = direction.normalized;        
    }    
    private void OnDrawGizmos()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mousePosition.Normalize();
        //mousePosition = mousePosition.normalized;
        //Debug.Log(mousePosition.magnitude);
        Gizmos.DrawWireSphere(mousePosition + new Vector2(transform.position.x , transform.position.y), 1f);

        

        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, walkDirection);

    }
}