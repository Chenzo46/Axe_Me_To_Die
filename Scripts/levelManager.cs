using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class levelManager : MonoBehaviour
{
    public static levelManager instance;
    public static int deaths = 0;

    private TMP_Text timeTXT;

    private int tickTockMi = 0;
    private int seconds = 0;
    private int minutes = 0;

    private bool hasBeat = false;

    private void Awake()
    {

        instance = this;

        if(GameObject.Find("Level Manager") != null)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!hasBeat)
        {
            tickTockMi += (int)(Time.deltaTime * 1000);

            if(tickTockMi / 1000 == 1)
            {
                tickTockMi = 0;
                seconds++;
            }

            if(seconds == 60)
            {
                seconds = 0;
                minutes++;
            }


            timeTXT.text =  minutes.ToString() + ":" + seconds.ToString() + ":" + tickTockMi.ToString();

            //seconds = 
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            deaths += 1;
        }
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void incDeath()
    {
        deaths += 1;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += setText;
    }

    private void setText(Scene scene, LoadSceneMode mode)
    {
        timeTXT = GameObject.FindGameObjectWithTag("time").GetComponent<TMP_Text>();

        if(SceneManager.GetActiveScene().buildIndex == 11)
        {
            hasBeat = true;

            timeTXT.text = "FINAL TIME:" + minutes.ToString() + ":" + seconds.ToString() + ":" + tickTockMi.ToString();

            TMP_Text d_TXT = GameObject.FindGameObjectWithTag("dTXT").GetComponent<TMP_Text>();

            d_TXT.text = "DEATHS: " + deaths;
        }
        else
        {
            TMP_Text txt = GameObject.FindGameObjectWithTag("levelInd").GetComponent<TMP_Text>();
            //TMP_Text time_TXT =
            txt.text = "LEVEL: " + SceneManager.GetActiveScene().buildIndex.ToString() + "/10";
        }
    }

}
