using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // private vars
    private Rigidbody2D rbody;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>(); // This is to grab the animator from the Creature
    }
    public void DoWalk(float input, float speed)
    {
        rbody.velocity = new Vector2(input * speed, rbody.velocity.y);
    }    
    private void OnDrawGizmos()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mousePosition.Normalize();
        //mousePosition = mousePosition.normalized;
        //Debug.Log(mousePosition.magnitude);
        Gizmos.DrawWireSphere(mousePosition + new Vector2(transform.position.x , transform.position.y), 1f);

    }
}
