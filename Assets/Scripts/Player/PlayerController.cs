using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //inspector vars
    public float baseSpeed;
    public float focusSpeed;
    public float accelerationSpeed;
    public float groundingForce;

    [Header("Animations")]
    public float neckRotationSpeed;

    [Header("References")]
    public LayerMask groundLayer;

    // private vars
    public State currentState;
    public State idleState = new IdleState();
    public State focusedState = new FocusedState();
    public State walkingState = new WalkingState();
    public State focusedWalkingState = new FocusedWalkingState();
    

    public PlayerMovement playerMovement;
    public PlayerAnimation playerAnimation;

    public Vector2 currentAxis;

    public float currentSpeed;

    public bool isFocused;
    public bool isMoving;
    public bool movingForward; // FORWARD is Scale = (-1,1,1)
    public bool facingForward;  // BACKWARD is Scale = (1,1,1)

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();

        currentState = idleState;
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetMouseButton(0))
        {
            isFocused = true;
        }
        else if(Input.GetMouseButton(1))
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
        currentState = currentState.ExecuteState(this);
        Debug.Log(currentState);
    }
    private void FixedUpdate()
    {
        currentAxis = GetUpAxis();
        currentState.ExecuteStatePhysics(this);
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
