using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PoseController))]
public class PoseControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //PoseController controller = (PoseController)target;

        //controller.parentBoneChildIndex = EditorGUILayout.IntField("Index of child that has bones:", controller.parentBoneChildIndex);


        //if (GUILayout.Button("Get BoneData"))
        //{
        //    controller.GetPose();
        //}

        //foreach (BoneTransform boneTransform in controller.boneTransforms)
        //{
        //    EditorGUILayout.LabelField(boneTransform.BoneName);
        //}

    }
}
