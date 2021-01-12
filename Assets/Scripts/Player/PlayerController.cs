using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // private inspector vars
    [SerializeField] float baseSpeed;
    [SerializeField] float focusSpeed;
    [SerializeField] float accelerationSpeed;
    [SerializeField] float groundingForce;

    [Header("References")]
    [SerializeField] LayerMask ground;

    // private vars
    private Rigidbody2D rbody;
    private Animator anim;


    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;

    private Vector2 currentAxis;

    private float currentSpeed;

    private bool isFocused;
    private bool isMoving;
    private bool movingForward; // FORWARD is Scale = (-1,1,1)
    private bool facingForward;  // BACKWARD is Scale = (1,1,1)



    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();

        rbody = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>(); // This is to grab the animator from the Creature
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isFocused = true;
        }
        else
        {
            isFocused = false;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            isMoving = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movingForward = true;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movingForward = false;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isFocused)
        {
            facingForward = playerAnimation.RotateHead(currentAxis, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            playerAnimation.ChangeDirection(!facingForward);
        }
        else
        {
            playerAnimation.ChangeDirection(!movingForward);
        }

        if (facingForward)
        {
            float dotProduct = currentAxis.x * -rbody.velocity.y / baseSpeed + currentAxis.y * rbody.velocity.x / baseSpeed;
            playerAnimation.SetAnimatorFloat("Speed", dotProduct *-1);
        }
        else
        {
            float dotProduct = currentAxis.x * -rbody.velocity.y / baseSpeed + currentAxis.y * rbody.velocity.x / baseSpeed;
            playerAnimation.SetAnimatorFloat("Speed", dotProduct);
        }
        
       

       


    }
    private void FixedUpdate()
    {
        currentAxis = GetUpAxis();

        if (isMoving)
        {
            if (isFocused)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, focusSpeed, accelerationSpeed * Time.deltaTime);
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, baseSpeed, accelerationSpeed * Time.deltaTime);
            }
            if (movingForward)
            {
                playerMovement.DoWalk(new Vector2(-currentAxis.y, currentAxis.x), currentSpeed);
                facingForward = true;
            }
            else
            {
                playerMovement.DoWalk(new Vector2(currentAxis.y, -currentAxis.x), currentSpeed);
                facingForward = false;
            }
        }
        else 
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, accelerationSpeed * Time.deltaTime);
        }
        rbody.AddForce(-groundingForce * currentAxis);
    }
    private Vector2 GetUpAxis()
    {
        float angle = transform.rotation.eulerAngles.z - 90;
        Vector3 down = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, down, 2f, ground);
        Vector2 newAxis = hit.normal;
        newAxis.Normalize();
        return newAxis;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        float angle = transform.rotation.eulerAngles.z - 90;

        Vector2 down = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector2 newTransform = transform.position;
        Debug.Log(down);
        Gizmos.DrawRay(newTransform + rbody.centerOfMass, rbody.centerOfMass + down);
    }
}
