using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // private inspector vars
    [SerializeField] float baseSpeed;
    [SerializeField] float focusSpeed;

    // private vars
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;

    private bool dirNegative;

    void Start()
    {        
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0.01f)
        {
            playerAnimation.RotateHead();
        }
        playerAnimation.SetWalkCycleSpeed();
    }
    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Fire1") > 0.01f)
        {
            playerMovement.DoWalk(horiz, focusSpeed);
        }
        else
        {
            playerMovement.DoWalk(horiz, baseSpeed);
        }

    }
}
