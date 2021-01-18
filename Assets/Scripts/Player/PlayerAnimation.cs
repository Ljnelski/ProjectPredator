using UnityEngine;
public class PlayerAnimation
{
    public Animator anim;
    public PlayerAnimation(Animator anim)
    {
        this.anim = anim;
    }
    public float VectorToParameterRotation(Vector2 axis, Vector2 target, float rotSpeed, string parameter)
    {
        target.Normalize();
        axis.Normalize();

        float dotProduct = Vector2.Dot(axis, target);
        float value = Mathf.Lerp(anim.GetFloat(parameter), dotProduct, rotSpeed);
        return value;
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
    public void SetFloat(string name, float value)
    {
        anim.SetFloat(name, value);
    }
}
