using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class backto_2_choose : MonoBehaviour
{
    private Button c_backBtn;
    // Use this for initialization
    void Start()
    {
        c_backBtn = GetComponent<Button>();

        c_backBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("2_choose");
        });
    }
}