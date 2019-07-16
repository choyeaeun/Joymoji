using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class d_A : MonoBehaviour
{
    private Button c_ABtn;
    // Use this for initialization
    void Start()
    {
        c_ABtn = GetComponent<Button>();

        c_ABtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("5_confirm");
        });
    }
}
