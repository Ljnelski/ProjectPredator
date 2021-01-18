//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    //inspector vars
//    public float baseSpeed;
//    public float focusSpeed;
//    public float accelerationSpeed;
//    public float groundingForce;
//    public float jumpForce;

//    [Header("Animations")]
//    public float neckRotationSpeed;

//    [Header("References")]
//    public LayerMask groundLayer;

//    // private vars
//   // public State currentState;
//   // public State idleState = new IdleState();
//    //public State focusedState = new FocusedState();
//    //public State walkingState = new WalkingState();
//    //public State focusedWalkingState = new FocusedWalkingState();
//    //public State fallingState = new FallingState();
//    //public State jumpingState = new JumpingState();
    

//    public PlayerMovement playerMovement;
//    public PlayerAnimation playerAnimation;

//    public Vector2 currentAxis;
//    public Vector2 directionToMouse;

//    public float currentSpeed;

//    public bool isFocused;
//    public bool isPreparedJump;
//    public bool isGrounded;
//    public bool isMoving;
//    public bool movingForward; // FORWARD is Scale = (-1,1,1)
//    public bool facingForward;  // BACKWARD is Scale = (1,1,1)

//    //trigger
//    public bool doJump;

//    private void Awake()
//    {
//        playerMovement = GetComponent<PlayerMovement>();
//        playerAnimation = GetComponent<PlayerAnimation>();

//        //currentState = idleState;
//    }
//    private void Update()
//    {        
//        if (Input.GetMouseButton(0))
//        {
//            if (isGrounded)
//            {
//                isPreparedJump = true;
//                isFocused = true;
//            }
//        }
//        else if(Input.GetMouseButton(1))
//        {
//            isFocused = true;
//        }
//        else
//        {
//            if (isPreparedJump && isGrounded)
//            {
//                doJump = true;
//            }
//            isFocused = false;
//        }

//        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
//        {
//            isMoving = false;
//        }
//        else if (Input.GetKey(KeyCode.A))
//        {
//            movingForward = true;
//            isMoving = true;
//        }
//        else if (Input.GetKey(KeyCode.D))
//        {
//            movingForward = false;
//            isMoving = true;
//        }
//        else
//        {
//            isMoving = false;
//        }
//        //currentState = currentState.ExecuteState(this);
        
//    }

//    private void FixedUpdate()
//    {        
//        //currentState.ExecuteStatePhysics(this);
//    }

//    #region MultiStateFunctions
//    public void RotateHeadToMouse()
//    {
//        float rotationAmount = neckRotationSpeed * Time.deltaTime;
        
//        facingForward = playerAnimation.VectorRightOfAxis(currentAxis, directionToMouse);
//        playerAnimation.VectorToParameterRotation(currentAxis, directionToMouse, rotationAmount, "Look");        
//    }
//    public void RotateHeadToRest()
//    {
//        float rotationAmount = neckRotationSpeed * Time.deltaTime;

//        if (movingForward)
//            playerAnimation.VectorToParameterRotation(currentAxis, new Vector2(-currentAxis.y, currentAxis.x), rotationAmount, "Look");
//        else
//            playerAnimation.VectorToParameterRotation(currentAxis, new Vector2(currentAxis.y, -currentAxis.x), rotationAmount, "Look");
//    }
//    public void SetWalkPlaySpeed(float maxSpeed)
//    {
//        maxSpeed = maxSpeed - groundingForce / 100;

//        if (facingForward)
//        {
//            float dotProduct = currentAxis.x * -playerMovement.GetVelocityY() / maxSpeed + currentAxis.y * playerMovement.GetVelocityX() / maxSpeed;
//            playerAnimation.SetAnimatorFloat("Speed", dotProduct * -1);
//        }
//        else
//        {
//            float dotProduct = currentAxis.x * -playerMovement.GetVelocityY() / maxSpeed + currentAxis.y * playerMovement.GetVelocityX() / maxSpeed;
//            playerAnimation.SetAnimatorFloat("Speed", dotProduct);
//        }
//    }
//    public void MovePlayer(float targetVelocity)
//    {
//        currentSpeed = Mathf.Lerp(currentSpeed, targetVelocity, accelerationSpeed * Time.deltaTime);

//        if (movingForward)
//        {
//            playerMovement.SetVelocity(new Vector2(-currentAxis.y, currentAxis.x), currentSpeed);
//            facingForward = true;
//        }
//        else
//        {
//            playerMovement.SetVelocity(new Vector2(currentAxis.y, -currentAxis.x), currentSpeed);
//            facingForward = false;
//        }        
//    }   
//    public void SetDirectionToMouse()
//    {
//        directionToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
//    }
//    public void AddForce(Vector2 direction, float force)
//    {
//        playerMovement.AddForce(direction, force);
//    }
//    #endregion

//    public bool GetAxisOfGround()
//    {
//        float angle = transform.rotation.eulerAngles.z - 90;
//        Vector3 angleDown = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);

//        RaycastHit2D hit = Physics2D.Raycast(transform.position, angleDown, 2f, groundLayer);
//        if (hit)
//        {
//            currentAxis = hit.normal;
//            return true;
//        }
//        else
//        {
//            return false;
//        }        
//    }
//    private void OnDrawGizmos()
//    {
//        //Vector2 playerPosition = transform.position;

//        //Vector2 groundForce = -currentAxis;
//        //Gizmos.color = Color.green;
//        //Gizmos.DrawRay(playerPosition, groundForce);

//        //Gizmos.color = Color.red;
//        //Vector2 cornerDetectorPos = playerPosition - new Vector2(-currentAxis.y, currentAxis.x) + groundForce;
//        //Gizmos.DrawRay(cornerDetectorPos, -new Vector2(currentAxis.y, -currentAxis.x));

        
//    }
//}
