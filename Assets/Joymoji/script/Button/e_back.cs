using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class e_back : MonoBehaviour
{
    private Button c_backBtn;
    // Use this for initialization
    void Start()
    {
        c_backBtn = GetComponent<Button>();

        c_backBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("4_B_background");
        });
    }
}