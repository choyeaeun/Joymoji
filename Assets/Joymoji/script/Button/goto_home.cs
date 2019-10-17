using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class goto_home : MonoBehaviour
{
    private Button homeBtn;
    // Use this for initialization
    void Start()
    {
        homeBtn = GetComponent<Button>();

        homeBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("1_start");

            if (GameObject.FindWithTag("test"))
            {
                GameObject.FindWithTag("test").SendMessage("TestInit"); //test nullreference 없애려고 setfalse함
                Debug.Log("test/TestInit지움");
            }
            
        });

       
    }

}
