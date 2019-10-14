using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class hatController : MonoBehaviour
{
    public GameObject BodySceneManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySceneManager _BodyManager;
    private Animator animator = null;
    private Transform pickBone = null;


    void Start()
    {
        //Debug.Log("LETS START");
        _BodyManager = FindObjectOfType<BodySceneManager>();
        pickBone = gameObject.transform;
    }

    void Update()
    {

        if (_BodyManager == null)
        {
            return;
        }

        Kinect.Body[] data = _BodyManager.GetData();

        if (data == null)
        {
            //Debug.Log("NULL DATA?");
            return;
        }

        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
                continue;
            if (body.IsTracked)
                trackedIds.Add(body.TrackingId);
            //Debug.Log("ARE YOU IN HERE");
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        foreach (ulong trackingId in knownIds)
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
                continue;
            if (body.IsTracked)
            {
                if (!_Bodies.ContainsKey(body.TrackingId))
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);

                TrackingSpecific(body);
            }
        }

    }

    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Sticker: " + id);
        return body;
    }

    private void TrackingSpecific(Kinect.Body body)
    {
        Kinect.Joint sourceJoint = body.Joints[Kinect.JointType.Head];

        pickBone.localPosition = GetVector3FromJoint(sourceJoint);
        //Debug.Log(pickBone.position);
    }

    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, (joint.Position.Y * 10), (joint.Position.Z * 10));
    }
}
