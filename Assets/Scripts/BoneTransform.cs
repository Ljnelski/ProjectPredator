using UnityEngine;
public class BoneTransform
{
    public string BoneName { get; private set; }
    public Transform BoneTransformations { get; private set; }

    public BoneTransform(string BoneName, Transform BoneTransformations)
    {
        this.BoneName = BoneName;
        this.BoneTransformations = BoneTransformations;
    }
    public void SetBoneName(string BoneName)
    {
        this.BoneName = BoneName;
    }
    public void SetBoneTransform(Transform BoneTransformations)
    {
        this.BoneTransformations = BoneTransformations;
    }
}

