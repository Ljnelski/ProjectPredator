using UnityEngine;

public class RotationToParameter
{
    public string targetParameter { get; private set; }
    public Animator targetAnimator { get; private set; }
    public RotationToParameter(Animator targetAnimator, string targetParameter)
    {
        this.targetAnimator = targetAnimator;
        this.targetParameter = targetParameter;
    }
    public void SetTargetParameter(string targetParameter)
    {
        this.targetParameter = targetParameter;
    }
    public void SetTargetAnimator(Animator targetAnimator)
    {
        this.targetAnimator = targetAnimator;
    }
    public float RotationValueToTarget(Vector2 targetDir, Vector2 upAxis, float rotationAmount)
    {
        targetDir.Normalize();
        upAxis.Normalize();
        float targetScewValue = upAxis.x * -targetDir.y + upAxis.y * targetDir.x;
        return Mathf.Lerp(targetScewValue, targetAnimator.GetFloat(targetParameter), rotationAmount);
    }
    public void SkewTransform()
    {

    }
}
