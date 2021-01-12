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
    public void SetAnimatorFloat(string parameterName, float value)
    {
        anim.SetFloat(parameterName, value);
    }
    public bool RotateHead(Vector2 newAxis, Vector2 target)
    {
       
        target.Normalize();
       
        newAxis.Normalize();

        
        float dotProduct = newAxis.x * -target.y + newAxis.y * target.x;
        float dotProduct1 = Vector2.Dot(newAxis, target);  
   
        float look = Mathf.Lerp(dotProduct1, anim.GetFloat("Look"), 0.1f);
        anim.SetFloat("Look", look);


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

