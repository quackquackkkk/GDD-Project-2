using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowChomp : MonoBehaviour
{
    private Vector3 offset;
    Transform targetChild;
    void Start()
    {
        // Calculate the initial offset between this object and its parent
        Transform parentTransform = transform.parent;
        targetChild = parentTransform.Find("Chomper");
        if (transform.parent != null)
        {
            offset = transform.position - targetChild.position;
        }
        else
        {
            Debug.LogWarning("This object has no parent!");
        }
    }

    void LateUpdate()
    {
        // Follow the parent's position while maintaining the initial offset
        if (transform.parent != null)
        {
            transform.position = targetChild.position + offset;
        }
    }
}
