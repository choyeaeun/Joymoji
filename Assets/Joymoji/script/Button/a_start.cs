using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class a_start : MonoBehaviour
{
    private Button _button;
    public GameObject buttonObj;

    // Use this for initialization
    void Start()
    {

        _button = GetComponent<Button>();

        _button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("2_choose");
        });

    }
    void OnTriggerStay2D(Collider2D col)
    {
        buttonObj.GetComponent<Animation>().Play();
    }
    
}