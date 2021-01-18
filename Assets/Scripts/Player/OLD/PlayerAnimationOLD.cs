using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationOLD : MonoBehaviour
{
    // private inspecter var
    [SerializeField] private LayerMask ground;
    // private vars
    private Animator anim;
    
    // Start is called before the first frame update
    void Awake()
    {        
        anim = transform.GetChild(0).GetComponent<Animator>(); // This is to grab the animator from the Creature
    }
    public void SetAnimatorFloat(string parameterName, float value)
    {
        anim.SetFloat(parameterName, value);
    }
    public void VectorToParameterRotation(Vector2 axis, Vector2 target, float rotSpeed, string parameter)
    {       
        target.Normalize();       
        axis.Normalize();        
       
        float dotProduct1 = Vector2.Dot(axis, target);  
        float look = Mathf.Lerp(anim.GetFloat(parameter), dotProduct1, rotSpeed);
        anim.SetFloat(parameter, look);       
    }
    public bool VectorRightOfAxis(Vector2 axis, Vector2 target)
    {
        float dotProduct = axis.x * -target.y + axis.y * target.x;

        if (dotProduct > 0f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void ChangeDirection(bool isForward)
    {
        if (isForward)
            transform.localScale = new Vector2(-1f, 1f);
        else
            transform.localScale = new Vector2(1f, 1f);
    }
}
    /* Note for the walk animation to make sure the feet don't slide the formula is (0.3 * Speed) */

