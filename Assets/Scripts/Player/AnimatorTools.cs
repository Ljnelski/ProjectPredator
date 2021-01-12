using UnityEngine;

public class AnimatorTools
{
    private Animator animator;
    public AnimatorTools (Animator animator)
    {
        this.animator = animator;
    }
    public void SetAnimatorFloat(string name, float value)
    {
        animator.SetFloat(name, value);
    }
    public float RotationValueToTarget(Vector2 targetDir, Vector2 upAxis, float currentValue, float rotationAmount)
    {
        targetDir.Normalize();
        upAxis.Normalize();

        float targetScewValue = upAxis.x * -targetDir.y + upAxis.y * targetDir.x;
        return Mathf.Lerp(targetScewValue, currentValue, rotationAmount);

    }
    public Vector2 ChangeCharacterDirection(Vector2 upAxis, Vector2 currentDirection, bool leftIsNegativeScale)
    {
        float dotProduct = Vector2.Dot(upAxis, currentDirection);

        if (leftIsNegativeScale)
        {
            if (dotProduct > 0f)
            {
                return new Vector2(-1f, 1f);
            }
            else if (dotProduct < 0f)
            {
                return new Vector2(1f, 1f);
            }
        }
        else
        {
            if (dotProduct > 0f)
            {
                return new Vector2(1f, 1f);
            }
            else if (dotProduct < 0f)
            {
                return new Vector2(-1f, 1f);
            }
        }

        return new Vector2(1f, 1f);
    }
}

