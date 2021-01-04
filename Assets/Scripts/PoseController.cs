using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseController : MonoBehaviour
{
    public List<Transform> bones;
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Hey buddy");
        bones = new List<Transform>();
        foreach (Transform bone in transform.GetChild(1).GetComponentsInChildren<Transform>())
        {
            bones.Add(bone);
            Debug.Log(bone.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
