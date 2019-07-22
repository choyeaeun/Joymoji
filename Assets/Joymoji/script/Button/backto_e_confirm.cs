using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class backto_e_confirm : MonoBehaviour
{
    private Button c_backBtn;
    // Use this for initialization
    void Start()
    {
        c_backBtn = GetComponent<Button>();

        c_backBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("5_confirm");
        });
    }
}
