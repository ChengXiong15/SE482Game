using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public GameObject textDisplay;
    [SerializeField] public int secondsLeft;
    public Scene scene;

    private bool takingAway = false;
    public Text fastestTimerText;
    private int check;
    private int check1;
    private int moduleCheck;
    private int moduleCheck1;
    private int checkScore1;
    private int checkScore2;


    private void Start()
    {
        check = PlayerPrefs.GetInt("FastestTime" + gameObject.scene.name);
        moduleCheck = check % 60;
        if (check >179)
        {
            fastestTimerText.text = "03:" + moduleCheck.ToString();
        }
        else if(check > 119)
        {
            fastestTimerText.text = "02:" + moduleCheck.ToString();
        }
        else if(check > 59)
        {
            fastestTimerText.text = "01:" + moduleCheck.ToString();
        }
        else if(check < 10)
        {
            fastestTimerText.text = "00:0" + PlayerPrefs.GetInt("FastestTime" + gameObject.scene.name, 0).ToString();
        }
        else
        {
            fastestTimerText.text = "00:" + PlayerPrefs.GetInt("FastestTime" + gameObject.scene.name, 0).ToString();
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }
    }

    private void Update()
    {
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            checkTime();
        }
    }

    public void checkTime()
    {
        checkScore1 = Score.score;
        checkScore2 = Score.highScore;
        if (checkScore1 > checkScore2)
        {
            PlayerPrefs.SetInt("FastestTime" + gameObject.scene.name, secondsLeft);
            check1 = PlayerPrefs.GetInt("FastestTime" + gameObject.scene.name);
            moduleCheck1 = check1 % 60;
            if (check1 < 190 && check1 > 179)
            {
                fastestTimerText.text = "03:0" + moduleCheck1.ToString();
            }
            else if(check1 > 179)
            {
                fastestTimerText.text = "03:" + moduleCheck1.ToString();
            }
            else if (check1 < 130 && check1 > 119)
            {
                fastestTimerText.text = "02:0" + moduleCheck1.ToString();
            }
            else if (check1 > 119)
            {
                fastestTimerText.text = "02:" + moduleCheck1.ToString();
            }
            else if (check1 < 70 && check1 > 59)
            {
                fastestTimerText.text = "01:0" + moduleCheck1.ToString();
            }
            else if (check1 > 59)
            {
                fastestTimerText.text = "01:" + moduleCheck1.ToString();
            }
            else if (check1 < 10)
            {
                fastestTimerText.text = "00:0" + secondsLeft.ToString();
            }
            else
            {
                fastestTimerText.text = "00:" + secondsLeft.ToString();
            }
        }
        else if (checkScore1 == checkScore2)
        {
            if (secondsLeft > PlayerPrefs.GetInt("FastestTime" + gameObject.scene.name))
            {
                PlayerPrefs.SetInt("FastestTime" + gameObject.scene.name, secondsLeft);
                check1 = PlayerPrefs.GetInt("FastestTime" + gameObject.scene.name);
                moduleCheck1 = check1 % 60;
                if (check1 < 190 && check1 >179)
                {
                    fastestTimerText.text = "03:0" + moduleCheck1.ToString();
                }
                else if (check1 > 179)
                {
                    fastestTimerText.text = "03:" + moduleCheck1.ToString();
                }
                else if ((check1 < 130) && (check1 > 119))
                {
                    fastestTimerText.text = "02:0" + moduleCheck1.ToString();
                }
                else if (check1 > 119)
                {
                    fastestTimerText.text = "02:" + moduleCheck1.ToString();
                }
                else if (check1 < 70 && check1 > 59)
                {
                    fastestTimerText.text = "01:0" + moduleCheck1.ToString();
                }
                else if (check1 > 59)
                {
                    fastestTimerText.text = "01:" + moduleCheck1.ToString();
                }
                else if(check1 < 10)
                {
                    fastestTimerText.text = "00:0" + secondsLeft.ToString();
                }
                else
                {
                    fastestTimerText.text = "00:" + secondsLeft.ToString();
                }
            }
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        moduleCheck = secondsLeft % 60;
        if (secondsLeft < 190 && secondsLeft > 179)
        {
            textDisplay.GetComponent<Text>().text = "03:0" + moduleCheck.ToString();
        }
        else if (secondsLeft > 179)
        {
            textDisplay.GetComponent<Text>().text = "03:" + moduleCheck.ToString();
        }
        else if (secondsLeft < 130 && secondsLeft > 119)
        {
            textDisplay.GetComponent<Text>().text = "02:0" + moduleCheck.ToString();
        }
        else if(secondsLeft > 119)
        {
            textDisplay.GetComponent<Text>().text = "02:" + moduleCheck.ToString();
        }
        else if (secondsLeft < 70 && secondsLeft > 59)
        {
            textDisplay.GetComponent<Text>().text = "01:0" + moduleCheck.ToString();
        }
        else if(secondsLeft > 59)
        {
            textDisplay.GetComponent<Text>().text = "01:" + moduleCheck.ToString();
        }
        else if(secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + moduleCheck.ToString();
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + moduleCheck.ToString();
        }
        takingAway = false;
    }

}
