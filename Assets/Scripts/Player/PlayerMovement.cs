using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // inspecter vars
    [SerializeField] private float speed;
    // private vars
    private Rigidbody2D rbody;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horiz) > 0.01f)
        {
            rbody.velocity = new Vector2(speed * horiz, rbody.velocity.y);
        }
        ChangeDirection(false);
    }
    private void ChangeDirection(bool flip)
    {
        if (flip)
            transform.parent.localScale *= -1f;
        else
        {
            if (rbody.velocity.x > 0.1f)
                transform.parent.localScale = new Vector2(-1f, 1f);
            else if (rbody.velocity.x < -0.1f)
                transform.parent.localScale = new Vector2(1f, 1f);
        }
    }
}
