using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class testController : MonoBehaviour
{
    public GameObject BodySceneManager;
    private HumanBodyBones bone;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
    private Animator animator = null;
    private Transform savedTrans = null;

    private Transform speBone;
    private Transform modelJoint;

    // Start is called before the first frame update
    void Start()
    {
        
        animator = gameObject.GetComponent<Animator>();
        Avatar avatar = animator.avatar;
        speBone = animator.GetBoneTransform(modelBones[19]);
     }

    // Update is called once per frame
    void Update()
    {
        if (BodySceneManager == null)
        {
            return;
        }

        _BodyManager = BodySceneManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }

        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }



        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        // First delete untracked bodies
        foreach (ulong trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
                //Debug.Log("untracked bodies delete!!");
            }
        }

        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {

                //처음 그리기
                if (!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                //새로 계속 그려주기
                //RefreshBodyObject(body, _Bodies[body.TrackingId]);

                trackingRoot(body);
                
            }
        }
    }
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        return body;
    }

    private void trackingRoot(Kinect.Body body)
    {
        for (int boneCount=0; boneCount <= kinectBones.Count; boneCount++)
        {
            if (!modelBones.ContainsKey(boneCount))
            {
                continue;
            }
            else
            {
                //키넥트의 조인트 가져오기
                Kinect.Joint sourceJoint = body.Joints[kinectBones[boneCount]];

                //모델의 조인트 Transform 정보
                modelJoint = animator.GetBoneTransform(modelBones[boneCount]);
                modelJoint.position = GetVector3FromJoint(sourceJoint);
            }
        }
        
   
    }

    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 2, joint.Position.Y * 2, (joint.Position.Z * 2)-10);
    }

    
    private Dictionary<int, HumanBodyBones> modelBones = new Dictionary<int, HumanBodyBones>()
    {
        { 0, HumanBodyBones.Hips },
        { 1, HumanBodyBones.Spine },
        { 2, HumanBodyBones.Neck },
        { 3, HumanBodyBones.Head },

        { 4, HumanBodyBones.RightShoulder },
        { 5, HumanBodyBones.RightUpperArm },
        { 6, HumanBodyBones.RightLowerArm },
        { 7, HumanBodyBones.RightHand },
        
        { 8, HumanBodyBones.LeftShoulder },
        { 9, HumanBodyBones.LeftUpperArm },
        { 10, HumanBodyBones.LeftLowerArm },
        { 11, HumanBodyBones.LeftHand },

        { 12, HumanBodyBones.RightUpperLeg },
        { 13, HumanBodyBones.RightLowerLeg },
        { 14, HumanBodyBones.RightFoot },
        { 15, HumanBodyBones.RightToes },

        { 16, HumanBodyBones.LeftUpperLeg },
        { 17, HumanBodyBones.LeftLowerLeg },
        { 18, HumanBodyBones.LeftFoot },
        { 19, HumanBodyBones.LeftToes },
        
        { 20, HumanBodyBones.Chest }
        //{ 21, HumanBodyBones.Head }
    };
    private Dictionary<int, Kinect.JointType> kinectBones = new Dictionary<int, Kinect.JointType>()
    {
        { 0, Kinect.JointType.SpineBase },
        { 1, Kinect.JointType.SpineMid },
        { 2, Kinect.JointType.Neck },
        { 3, Kinect.JointType.Head },

        { 4, Kinect.JointType.ShoulderLeft },
        { 5, Kinect.JointType.ElbowLeft },
        { 6, Kinect.JointType.WristLeft },
        { 7, Kinect.JointType.HandLeft },

        { 8, Kinect.JointType.ShoulderRight },
        { 9, Kinect.JointType.ElbowRight },
        { 10, Kinect.JointType.WristRight },
        { 11, Kinect.JointType.HandRight },

        { 12, Kinect.JointType.HipLeft },
        { 13, Kinect.JointType.KneeLeft },
        { 14, Kinect.JointType.AnkleLeft },
        { 15, Kinect.JointType.FootLeft },

        { 16, Kinect.JointType.HipRight },
        { 17, Kinect.JointType.KneeRight },
        { 18, Kinect.JointType.AnkleRight },
        { 19, Kinect.JointType.FootRight },

        { 20, Kinect.JointType.SpineShoulder },
        { 21, Kinect.JointType.HandTipLeft },
        { 22, Kinect.JointType.ThumbLeft },
        { 23, Kinect.JointType.HandTipRight },
        { 24, Kinect.JointType.ThumbRight }
    };
}
