using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Values")]
    public LayerMask groundLayer;
    public float walkingSpeed;
    public float accelerationSpeed;
    public float groundingForce;
    public float neckRotationSpeed;

    [Header("Controllers")]
    public bool MovingForward;
    public bool FacingForward;
    public bool IsGrounded;
    public bool IsMoving;
    public bool IsFocused;
    public bool DoingJump;
    public bool DoingEcolocate;

    public Vector2 DirectionToMouse {
        get { return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; }
        private set { }        
    }
    public Vector2 CurrentAxis
    {
        get
        {
            float angle = transform.rotation.eulerAngles.z - 90;
            Vector3 angleDown = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, angleDown, 2f, groundLayer);

            return hit.normal;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (IsGrounded)
            {
                IsFocused = true;
            }
        }
        else if (Input.GetMouseButton(1))
        {
            IsFocused = true;
        }
        else
        {
            // DO the jump
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            IsMoving = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MovingForward = true;
            IsMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MovingForward = false;
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }
}


