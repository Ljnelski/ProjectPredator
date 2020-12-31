using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ResetPose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteSkin sprite = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteSkin>();
        Vector3 vector = Vector3.zero;
        foreach (Transform bone in sprite.boneTransforms)
        {
            Debug.Log("The transform is: " + bone.position);
            bone.transform.localPosition = vector;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
