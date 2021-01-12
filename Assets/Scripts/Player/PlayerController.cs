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

    [Header("Animations")]
    [SerializeField] float neckRotationSpeed;

    [Header("References")]
    [SerializeField] LayerMask groundLayer;

    // private vars    
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;

    private Vector2 currentAxis;

    private float currentSpeed;

    private bool isFocused;
    private bool isMoving;
    private bool movingForward; // FORWARD is Scale = (-1,1,1)
    private bool facingForward;  // BACKWARD is Scale = (1,1,1)

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
        //anim = transform.GetChild(0).GetComponent<Animator>(); // This is to grab the animator from the Creature
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
            Vector2 targetRotation = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            facingForward = playerAnimation.VectorRightOfAxis(currentAxis, targetRotation);
            playerAnimation.VectorToParameterRotation(currentAxis, targetRotation, neckRotationSpeed * Time.deltaTime, "Look");
            playerAnimation.ChangeDirection(!facingForward);
        }
        else
        {
            playerAnimation.ChangeDirection(!movingForward);
            float rotationAmount = neckRotationSpeed * Time.deltaTime;

            if (movingForward)
            {
                playerAnimation.VectorToParameterRotation(currentAxis, new Vector2(-currentAxis.y, currentAxis.x), rotationAmount, "Look");
            }
            else
            {
                playerAnimation.VectorToParameterRotation(currentAxis, new Vector2(currentAxis.y, -currentAxis.x), rotationAmount, "Look");
            }            
        }

        if (facingForward)
        {
            float dotProduct = currentAxis.x * -playerMovement.GetVelocityY() / baseSpeed + currentAxis.y * playerMovement.GetVelocityX() / baseSpeed;
            playerAnimation.SetAnimatorFloat("Speed", dotProduct *-1);
        }
        else
        {
            float dotProduct = currentAxis.x * -playerMovement.GetVelocityY() / baseSpeed + currentAxis.y * playerMovement.GetVelocityX() / baseSpeed;
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
                playerMovement.SetVelocity(new Vector2(-currentAxis.y, currentAxis.x), currentSpeed);
                facingForward = true;
            }
            else
            {
                playerMovement.SetVelocity(new Vector2(currentAxis.y, -currentAxis.x), currentSpeed);
                facingForward = false;
            }
        }
        else 
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, accelerationSpeed * Time.deltaTime);
        }
        playerMovement.AddForce( currentAxis, -groundingForce);
    }
    private Vector2 GetUpAxis()
    {
        float angle = transform.rotation.eulerAngles.z - 90;
        Vector3 angleDown = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, angleDown, 2f, groundLayer);
        Vector2 newAxis = hit.normal;
        newAxis.Normalize();

        return newAxis;
    }
    private void OnDrawGizmos()
    {
        Vector2 playerPosition = transform.position;

        Vector2 groundForce = -GetUpAxis();
        Gizmos.color = Color.green;
        Gizmos.DrawRay(playerPosition, groundForce);

        Gizmos.color = Color.red;
        Vector2 cornerDetectorPos = playerPosition - new Vector2(-currentAxis.y, currentAxis.x) + groundForce;
        Gizmos.DrawRay(cornerDetectorPos, -new Vector2(currentAxis.y, -currentAxis.x));

        
    }
}
