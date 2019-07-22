using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(GameObject.Find("DefaultThing"));
        //GameObject.Destroy(GameObject.Find("Kinect"));
    }

    // Update is called once per frame
    void OnDestroy()
    {
        GameObject.Destroy(GameObject.Find("Kinect"));
    }
}
