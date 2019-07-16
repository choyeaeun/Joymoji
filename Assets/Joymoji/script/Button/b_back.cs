using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class b_back : MonoBehaviour
{
    private Button b_backBtn;
    // Use this for initialization
    void Start()
    {
        b_backBtn = GetComponent<Button>();

        b_backBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("1_start");
        });
    }
}