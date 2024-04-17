using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance ;
    public GameObject square ;
    public GameObject endPanel;
    public Text timeTxt ;
    public Text nowScore ;
    public Text BestScore ;
    public Animator anim;
    bool isPlay = true;
    float time = 0.0f;
    string key = "bestScore";

private void Awake()
{
    if(Instance == null)
    {
        Instance = this;
    }
}

    void Start()
    {
        Time.timeScale = 1.0f;
        InvokeRepeating("MakeSquare", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlay)
        {
            time +=Time.deltaTime;
            timeTxt.text = time.ToString("N2"); 
        }
        
    }
    void MakeSquare()
    {
        Instantiate(square);
    }
    public void GameOver()
    {
        isPlay = false;
        anim.SetBool("isDie",true);
        Invoke("TimeStop", 0.5f); 
        nowScore.text = time.ToString("N2");

        // 최고점수가 있다면
        if(PlayerPrefs.HasKey("bestScore"))
        {
            float best = PlayerPrefs.GetFloat("bestScore");
            if(best < time)
            {
                PlayerPrefs.SetFloat("bestScore",time);
                BestScore.text = time.ToString("N2");
            }
            else
            {
                BestScore.text = best.ToString("N2");
            }
        }
        else
        {
            PlayerPrefs.SetFloat("bestScore",time);
            BestScore.text = time.ToString("N2");
        }
        
            // 최고점수 < 현재 점수
            // 현재 점수를 최고 점수에 저장한다.
        // 최고점수가 없다면
            // 현재 점수를 저장한다
        endPanel.SetActive(true);
    }
    void TimeStop()
    {
        Time.timeScale = 0.0f ;
    }
}