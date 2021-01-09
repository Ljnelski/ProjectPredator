using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // private inspecter var
    [SerializeField] private LayerMask ground;
    // private vars
    private Rigidbody2D rbody;
    private Animator anim;
    
    // Start is called before the first frame update
    void Awake()
    {        
        anim = transform.GetChild(0).GetComponent<Animator>(); // This is to grab the animator from the Creature
        rbody = GetComponent<Rigidbody2D>();
    }
    public void SetWalkCycleSpeed(/*Pass speed value as method*/)
    {
        anim.SetFloat("Speed", rbody.velocity.magnitude);
    }
    public void RotateHead()
    {
        Vector2 mouseDirFromCharacter = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDirFromCharacter.Normalize();

        // Get the angle of the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, ground);
        Vector2 newAxis = hit.normal;
        newAxis.Normalize();

        float dotProduct = newAxis.x * -mouseDirFromCharacter.y + newAxis.y * mouseDirFromCharacter.x;
        float dotProduct1 = newAxis.x * mouseDirFromCharacter.x + newAxis.y * mouseDirFromCharacter.y;

        if (dotProduct > 0f)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (dotProduct < 0f)
        {
            transform.localScale = new Vector2(1f, 1f);
        }

        float look = Mathf.Lerp(dotProduct1, anim.GetFloat("Look"), 0.1f);
        anim.SetFloat("Look", look);
    }
    private void ChangeDirection(bool scaleNegative)
    {
        if (scaleNegative)
            transform.localScale = new Vector2(-1f, 1f);
        else if (!scaleNegative)
            transform.localScale = new Vector2(1f, 1f);

    }
    }
    /* Note for the walk animation to make sure the feet don't slide the formula is (0.3 * Speed) */
}
