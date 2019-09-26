using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextTimer : MonoBehaviour
{
    private int time = 40;
    public Text mytext;
    private bool isDone = true;

    public Button firstBtn;
    public Button secondBtn;

    void Start()
    {
        mytext.text = "바닥에 표시된 스티커에 서서 카메라를 응시해주세요";
        //Destroy(gameObject, time);
        StartCoroutine("LoseTime");
        secondBtn.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isDone == true) {
            switch (time) {
                case 35:
                    mytext.text = "두 손을 인식할 수 있도록 가볍게 양손바닥을 올려주세요";
                    break;
                case 30:
                    mytext.text = "손바닥을 인식했다면 버튼 위에 손을 올려보세요\n(버튼이 감지된다면 버튼의 색이 진한 흰색으로 바뀝니다)";
                    break;
                case 20:
                    mytext.text = "버튼을 클릭하고 싶다면 진한 흰색 상태에서 주먹을 쥐었다 펴보세요";
                    isDone = false;
                    break;
                default:
                    break;
            }
        }
    }

    public void btnAction()
    {
        mytext.text = "잘했어요!\n이제 나만의 이모티콘을 만들러 떠나볼까요 ? ";
        firstBtn.gameObject.SetActive(false);
        secondBtn.gameObject.SetActive(true);
    }

    public void startBtnAction()
    {
        SceneManager.LoadScene("1_start");
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
    }
}