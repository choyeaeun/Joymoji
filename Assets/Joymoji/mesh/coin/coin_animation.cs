using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_animation : MonoBehaviour
{
    public GameObject buttonObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D col)
    {
        buttonObj.GetComponent<Animation>().Play();
    }
}
