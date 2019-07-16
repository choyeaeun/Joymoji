using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using Kinect = Windows.Kinect;

public class AvatarController : MonoBehaviour
{
    protected int moveRate = 1;

    //선형보간법? Slerp smooth Factor
    public float smoothFactor = 5f;

    //offset 노드가 사용자의 위치로 위치가 다시 설정되는지 아닌지
    public bool offsetRelativeToSensor = false;

    //몸의 root node
    protected Transform bodyRoot;

    //모델의 회전에 필요한 변수
    protected GameObject offsetNode;

    //모든 뼈들 담고 있는 변수 
    protected Transform[] bones;

    //kinect가 track하기 시작할 때 뼈들의 회전들
    protected Quaternion[] initialRotations;
    protected Quaternion[] initialLocalRotations;

    //위치와 회전 초기화
    protected Vector3 initialPosition;
    protected Quaternion initialRotation;

    //캐릭터 위치를 위한 구경측정 offset
    protected bool offsetCalibrated = false;
    protected float xOffset, yOffset, zOffset;

    public GameObject BodySourceManager;
    private BodySourceManager _BodyManager;

    protected KinectManager kinectManager;

    private Transform _transformCache;
    public new Transform transform
    {
        get
        {
            if (!_transformCache)
                _transformCache = base.transform;
            return _transformCache; 
        }
    }

    public void Awake()
    {
        if (bones != null)
            return;

        bones = new Transform[22];

        initialRotations = new Quaternion[bones.Length];
        initialLocalRotations = new Quaternion[bones.Length];

        //Map bonew to the points the Kinect tracks
        MapBones();

        //Get initial bone rotations
        GetInitialRotations();

        
    }
    public void Update()
    {
        if (BodySourceManager == null)
        {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }

        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }



        List<uint> trackedIds = new List<uint>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                trackedIds.Add((uint)(body.TrackingId));
            }
        }

        List<uint> knownIds = new List<uint>(_Bodies.Keys);

        // First delete untracked bodies
        foreach (uint trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
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
                if (!_Bodies.ContainsKey((uint)(body.TrackingId)))
                {
                    //_Bodies[body.TrackingId] = UpdateAvatar(body.TrackingId);
                    UpdateAvatar((uint)(body.TrackingId));
                    Debug.Log("body.TrackingId : " + body.TrackingId);
                }
                //새로 계속 그려주기
                //RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
    }
    public void UpdateAvatar(uint UserID)
    {
        if (!transform.gameObject.activeInHierarchy)
            return;

        //if (kinectManager == null)
            //kinectManager = kinectManager.instance;

        //avatar를 kinect위치로 움직이기
        MoveAvatar(UserID);

        for (var boneIndex = 0; boneIndex < bones.Length; boneIndex++)
        {
            if (!bones[boneIndex])
                continue;

            if (kinectBones.ContainsKey(boneIndex))
            {
                Kinect.JointType joint = kinectBones[boneIndex];
                TransformBone(UserID, joint, boneIndex);
            }
        }

        Debug.Log("Model의 bones : " + modelBones[3]);
    }
    
    public void ResetToInitialPosition()
    {
        if (bones == null)
            return;

        if(offsetNode != null)
        {
            offsetNode.transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

        for(int i =0; i < bones.Length; i++)
        {
            if (bones[i] != null)
            {
                bones[i].rotation = initialRotations[i];
            }
        }

        if(bodyRoot != null)
        {
            bodyRoot.localPosition = Vector3.zero;
            bodyRoot.localRotation = Quaternion.identity;
        }

        if(offsetNode != null)
        {
            offsetNode.transform.position = initialPosition;
            offsetNode.transform.rotation = initialRotation;
        }
        else
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }
    }

    public void SuccessfulCalibration(uint userId)
    {
        if(offsetNode != null)
        {
            offsetNode.transform.rotation = initialRotation;
        }

        offsetCalibrated = false;
    }

    protected void TransformBone(uint userId, Kinect.JointType joint, int boneIndex)
    {
        bool flip = false;

        Transform boneTransform = bones[boneIndex];
        if (boneTransform == null || kinectManager == null)
            return;

        int iJoint = (int)joint;
        if (iJoint < 0)
            return;

        Quaternion jointRotation = kinectManager.GetJointOrientation(userId, iJoint, flip);
        if (jointRotation == Quaternion.identity)
            return;

        Quaternion newRotation = Kinect2AvatarRot(jointRotation, boneIndex);

        if (smoothFactor != 0f)
            boneTransform.rotation = Quaternion.Slerp(boneTransform.rotation, newRotation, smoothFactor * Time.deltaTime);
        else
            boneTransform.rotation = newRotation;
    }

    protected void TransformSpecialBone(uint userId, Kinect.JointType joint, Kinect.JointType jointParent, int boneIndex, Vector2 baseDir, bool flip)
    {
        Transform boneTransform = bones[boneIndex];
        if (boneTransform == null || kinectManager == null)
            return;

        if (!kinectManager.IsJointTracked(userId, (int)joint) || !kinectManager.IsJointTracked(userId, (int)jointParent))
            return;

        Vector3 jointDir = kinectManager.GetDirectionBetweenJoints(userId, (int)jointParent, (int)joint, false, true);
        Quaternion jointRotation = jointDir != Vector3.zero ? Quaternion.FromToRotation(baseDir, jointDir) : Quaternion.identity;

        if(jointRotation != Quaternion.identity)
        {
            Quaternion newRotation = Kinect2AvatarRot(jointRotation, boneIndex);

            if (smoothFactor != 0f)
                boneTransform.rotation = Quaternion.Slerp(boneTransform.rotation, newRotation, smoothFactor * Time.deltaTime);
            else
                boneTransform.rotation = newRotation;
        }
    }

    protected void MoveAvatar(uint UserID)
    {
        if (bodyRoot == null || _BodyManager == null)
            return;
        if (!kinectManager.IsJointTracked(UserID, (int)Kinect.JointType.SpineBase))
            return;

        Vector3 trans = kinectManager.GetUserPosition(UserID);

        // If this is the first time we're moving the avatar, set the offset. Otherwise ignore it.
        if (!offsetCalibrated)
        {
            offsetCalibrated = true;

            xOffset = trans.x * moveRate;
            yOffset = trans.y * moveRate;
            zOffset = -trans.z * moveRate;

            if (offsetRelativeToSensor)
            {
                Vector3 cameraPos = Camera.main.transform.position;

                float yRelToAvatar = (offsetNode != null ? offsetNode.transform.position.y : transform.position.y) - cameraPos.y;
                Vector3 relativePos = new Vector3(trans.x * moveRate, yRelToAvatar, trans.z * moveRate);
                Vector3 offsetPos = cameraPos + relativePos;

                if (offsetNode != null)
                {
                    offsetNode.transform.position = offsetPos;
                }
                else
                {
                    transform.position = offsetPos;
                }
            }
        }
        // Smoothly transition to the new position
        Vector3 targetPos = Kinect2AvatarPos(trans);

        if (smoothFactor != 0f)
            bodyRoot.localPosition = Vector3.Lerp(bodyRoot.localPosition, targetPos, smoothFactor * Time.deltaTime);
        else
            bodyRoot.localPosition = targetPos;
    }
    //
    protected virtual void MapBones()
    {
        offsetNode = new GameObject(name + "Ctrl") { layer = transform.gameObject.layer, tag = transform.gameObject.tag };
        offsetNode.transform.position = transform.position;
        offsetNode.transform.rotation = transform.rotation;
        offsetNode.transform.parent = transform.parent;

        transform.parent = offsetNode.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // transform정보를 model에 넣기
        bodyRoot = transform;

        // animator 컴포넌트에서 bone transform 정보 가져오기
        var animatorComponent = GetComponent<Animator>();

        for (int boneIndex = 0; boneIndex < bones.Length;boneIndex++)
        {
            if (!modelBones.ContainsKey(boneIndex))
                continue;

            bones[boneIndex] = animatorComponent.GetBoneTransform(modelBones[boneIndex]);
        }
    }

    protected void GetInitialRotations()
    {
        if(offsetNode != null)
        {
            initialPosition = offsetNode.transform.position;
            initialRotation = offsetNode.transform.rotation;

            transform.rotation = Quaternion.identity;
        }
        else
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }

        for (int i = 0; i < bones.Length; i++)
        {
            if (bones[i] != null)
            {
                initialRotations[i] = bones[i].rotation;
                initialLocalRotations[i] = bones[i].localRotation;
            }
        }

        if(offsetNode != null)
        {
            offsetNode.transform.rotation = initialRotation;
        }
        else
        {
            transform.rotation = initialRotation;
        }
    }

    protected Quaternion Kinect2AvatarRot(Quaternion jointRotation, int boneIndex)
    {
        Quaternion newRotation = jointRotation * initialRotations[boneIndex];

        if(offsetNode != null)
        {
            Vector3 totalRotation = newRotation.eulerAngles + offsetNode.transform.rotation.eulerAngles;
            newRotation = Quaternion.Euler(totalRotation);
        }
        return newRotation;
    }

    protected Vector3 Kinect2AvatarPos(Vector3 jointPosition)
    {
        float xPos;
        float yPos;
        float zPos;

        xPos = jointPosition.x * moveRate - xOffset;
        yPos = jointPosition.y * moveRate - yOffset;
        zPos = -jointPosition.z * moveRate - zOffset;

        // If we are tracking vertical movement, update the y. Otherwise leave it alone.
        Vector3 avatarJointPos = new Vector3(xPos, yPos, zPos);

        return avatarJointPos;
    }
    
    private Dictionary<uint, GameObject> _Bodies = new Dictionary<uint, GameObject>();

    private Dictionary<int, HumanBodyBones> modelBones = new Dictionary<int, HumanBodyBones>()
    {
        {0, HumanBodyBones.Hips},
        {1, HumanBodyBones.Spine},
        {2, HumanBodyBones.Neck},
        {3, HumanBodyBones.Head},

        {4, HumanBodyBones.LeftShoulder},
        {5, HumanBodyBones.LeftUpperArm},
        {6, HumanBodyBones.LeftLowerArm},
        {7, HumanBodyBones.LeftHand},
        {8, HumanBodyBones.LeftIndexProximal},

        {9, HumanBodyBones.RightShoulder},
        {10, HumanBodyBones.RightUpperArm},
        {11, HumanBodyBones.RightLowerArm},
        {12, HumanBodyBones.RightHand},
        {13, HumanBodyBones.RightIndexProximal},

        {14, HumanBodyBones.LeftUpperLeg},
        {15, HumanBodyBones.LeftLowerLeg},
        {16, HumanBodyBones.LeftFoot},
        {17, HumanBodyBones.LeftToes},

        {18, HumanBodyBones.RightUpperLeg},
        {19, HumanBodyBones.RightLowerLeg},
        {20, HumanBodyBones.RightFoot},
        {21, HumanBodyBones.RightToes},
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
        { 8, Kinect.JointType.HandTipLeft },

        { 9, Kinect.JointType.ShoulderRight },
        { 10, Kinect.JointType.ElbowRight },
        { 11, Kinect.JointType.WristRight },
        { 12, Kinect.JointType.HandRight },
        { 13, Kinect.JointType.HandTipRight },

        { 14, Kinect.JointType.HipLeft },
        { 15, Kinect.JointType.KneeLeft },
        { 16, Kinect.JointType.AnkleLeft },
        { 17, Kinect.JointType.FootLeft },

        { 18, Kinect.JointType.HipRight },
        { 19, Kinect.JointType.KneeRight },
        { 20, Kinect.JointType.AnkleRight },
        { 21, Kinect.JointType.FootRight }
    };
/*
    public class FindMissingScripts : EditorWindow
    {
        [MenuItem("Window/FindMissingScripts")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(FindMissingScripts));
        }

        public void OnGUI()
        {
            if (GUILayout.Button("Find Missing Scripts in selected prefabs"))
            {
                FindInSelected();
            }
        }
        private static void FindInSelected()
        {
            GameObject[] go = Selection.gameObjects;
            int go_count = 0, components_count = 0, missing_count = 0;
            foreach (GameObject g in go)
            {
                go_count++;
                Component[] components = g.GetComponents<Component>();
                for (int i = 0; i < components.Length; i++)
                {
                    components_count++;
                    if (components[i] == null)
                    {
                        missing_count++;
                        string s = g.name;
                        Transform t = g.transform;
                        while (t.parent != null)
                        {
                            s = t.parent.name + "/" + s;
                            t = t.parent;
                        }
                        Debug.Log(s + " has an empty script attached in position: " + i, g);
                    }
                }
            }

            Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
        }
    }*/
}
