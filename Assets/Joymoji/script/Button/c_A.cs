using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class c_A : MonoBehaviour
{
    private Button c_ABtn;
    // Use this for initialization
    void Start()
    {
        c_ABtn = GetComponent<Button>();

        c_ABtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("4_B_background");
        });
    }
}
