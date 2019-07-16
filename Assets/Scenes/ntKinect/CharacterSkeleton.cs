using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


class CharacterSkeleton
{
    public const int
      // JointType
      JointType_SpineBase = 0,
      JointType_SpineMid = 1,
      JointType_Neck = 2,
      JointType_Head = 3,
      JointType_ShoulderLeft = 4,
      JointType_ElbowLeft = 5,
      JointType_WristLeft = 6,
      JointType_HandLeft = 7,
      JointType_ShoulderRight = 8,
      JointType_ElbowRight = 9,
      JointType_WristRight = 10,
      JointType_HandRight = 11,
      JointType_HipLeft = 12,
      JointType_KneeLeft = 13,
      JointType_AnkleLeft = 14,
      JointType_FootLeft = 15,
      JointType_HipRight = 16,
      JointType_KneeRight = 17,
      JointType_AnkleRight = 18,
      JointType_FootRight = 19,
      JointType_SpineShoulder = 20,
      JointType_HandTipLeft = 21,
      JointType_ThumbLeft = 22,
      JointType_HandTipRight = 23,
      JointType_ThumbRight = 24,
      // TrackingState
      TrackingState_NotTracked = 0,
      TrackingState_Inferred = 1,
      TrackingState_Tracked = 2,
      // Number
      bodyCount = 6,
      jointCount = 25;

    private static int[] jointSegment = new int[] {
    JointType_SpineBase, JointType_SpineMid,             // Spine
    JointType_Neck, JointType_Head,                      // Neck
    // left
    JointType_ShoulderLeft, JointType_ElbowLeft,         // LeftUpperArm
    JointType_ElbowLeft, JointType_WristLeft,            // LeftLowerArm
    JointType_WristLeft, JointType_HandLeft,             // LeftHand
    JointType_HipLeft, JointType_KneeLeft,               // LeftUpperLeg
    JointType_KneeLeft, JointType_AnkleLeft,             // LeftLowerLeg6
    JointType_AnkleLeft, JointType_FootLeft,             // LeftFoot
    // right
    JointType_ShoulderRight, JointType_ElbowRight,       // RightUpperArm
    JointType_ElbowRight, JointType_WristRight,          // RightLowerArm
    JointType_WristRight, JointType_HandRight,           // RightHand
    JointType_HipRight, JointType_KneeRight,             // RightUpperLeg
    JointType_KneeRight, JointType_AnkleRight,           // RightLowerLeg
    JointType_AnkleRight, JointType_FootRight,           // RightFoot
  };
    public Vector3[] joint = new Vector3[jointCount];
    public int[] jointState = new int[jointCount];

    Dictionary<HumanBodyBones, Vector3> trackingSegment = null;
    Dictionary<HumanBodyBones, int> trackingState = null;

    private static HumanBodyBones[] humanBone = new HumanBodyBones[] {
    HumanBodyBones.Hips,
    HumanBodyBones.Spine,
    HumanBodyBones.UpperChest,
    HumanBodyBones.Neck,
    HumanBodyBones.Head,
    HumanBodyBones.LeftUpperArm,
    HumanBodyBones.LeftLowerArm,
    HumanBodyBones.LeftHand,
    HumanBodyBones.LeftUpperLeg,
    HumanBodyBones.LeftLowerLeg,
    HumanBodyBones.LeftFoot,
    HumanBodyBones.RightUpperArm,
    HumanBodyBones.RightLowerArm,
    HumanBodyBones.RightHand,
    HumanBodyBones.RightUpperLeg,
    HumanBodyBones.RightLowerLeg,
    HumanBodyBones.RightFoot,
  };

    private static HumanBodyBones[] targetBone = new HumanBodyBones[] {
    HumanBodyBones.Spine,
    HumanBodyBones.Neck,
    HumanBodyBones.LeftUpperArm,
    HumanBodyBones.LeftLowerArm,
    HumanBodyBones.LeftHand,
    HumanBodyBones.LeftUpperLeg,
    HumanBodyBones.LeftLowerLeg,
    HumanBodyBones.LeftFoot,
    HumanBodyBones.RightUpperArm,
    HumanBodyBones.RightLowerArm,
    HumanBodyBones.RightHand,
    HumanBodyBones.RightUpperLeg,
    HumanBodyBones.RightLowerLeg,
    HumanBodyBones.RightFoot,
  };

    public GameObject humanoid;
    private Dictionary<HumanBodyBones, RigBone> rigBone = null;
    private bool isSavedPosition = false;
    private Vector3 savedPosition;
    private Quaternion savedHumanoidRotation;
    public CharacterSkeleton(GameObject h)
    {
        humanoid = h;
        rigBone = new Dictionary<HumanBodyBones, RigBone>();
        foreach (HumanBodyBones bone in humanBone)
        {
            rigBone[bone] = new RigBone(humanoid, bone);
        }
        savedHumanoidRotation = humanoid.transform.rotation;
        trackingSegment = new Dictionary<HumanBodyBones, Vector3>(targetBone.Length);
        trackingState = new Dictionary<HumanBodyBones, int>(targetBone.Length);
    }
}
