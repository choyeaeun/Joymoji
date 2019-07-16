using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigBone : MonoBehaviour
{
    public GameObject gameObject;
    public HumanBodyBones bone;
    public bool isValid;
    public Transform transform
    {
        get { return animator.GetBoneTransform(bone); }
    }
    Animator animator;
    Quaternion savedValue;

    public RigBone(GameObject g, HumanBodyBones b)
    {
        gameObject = g;
        bone = b;
        isValid = false;
        animator = gameObject.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("no Animator Component");
            return;
        }
        Avatar avatar = animator.avatar;
        if (avatar == null || !avatar.isHuman || !avatar.isValid)
        {
            Debug.Log("Avatar is not Humanoid or it is not valid");
            return;
        }
        isValid = true;
        savedValue = animator.GetBoneTransform(bone).localRotation;
    }
}