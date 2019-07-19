using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class b_back : MonoBehaviour
{
    private Button b_backBtn;
    public GameObject buttonObj;
    void Start()
    {
        b_backBtn = GetComponent<Button>();

        b_backBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("1_start");
        });
    }
    void OnTriggerStay2D(Collider2D col)
    {
        buttonObj.GetComponent<Animation>().Play();
    }
}