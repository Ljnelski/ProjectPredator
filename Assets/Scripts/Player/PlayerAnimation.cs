using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // private vars
    private Rigidbody2D rbody;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rbody.velocity.x));
    }
    /* Note for the walk animation to make sure the feet don't slide the formula is (0.3 * Speed) */
}
