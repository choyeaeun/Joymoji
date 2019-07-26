using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DONTdestroyKinect : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
