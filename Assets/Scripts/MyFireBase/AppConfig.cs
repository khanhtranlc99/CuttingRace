using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AppConfig : MonoBehaviour
{
    #region Instance
    private static AppConfig instance;
    public static AppConfig Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AppConfig>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.AddComponent<AppConfig>();
                    go.name = "AppConfig";
                    instance = go.GetComponent<AppConfig>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (FindObjectsOfType<AppConfig>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        ReadSaveConfig();
    }
    #endregion

    #region Private Methods
    private void ReadSaveConfig()
    {
        churn = PlayerPrefs.GetInt("churn_config", 0) == 1;
        spamAds = PlayerPrefs.GetInt("spam_ads_config", 0) == 1;
        drawControl = PlayerPrefs.GetInt("draw_control", 0) == 1;
        timeShowSpin = PlayerPrefs.GetInt("time_show_spin", 300);
        activeShuffle = PlayerPrefs.GetInt("active_shuffle", 0) == 1;
        colorBackGround = PlayerPrefs.GetInt("colorBackGround", 0) == 1;
    }
    #endregion

    #region Config Value

    private bool churn;
    public bool CHURN
    {
        get
        {
            return churn;
        }

        set
        {
            churn = value;
            PlayerPrefs.SetInt("churn_config", churn ? 1 : 0);
        }
    }

    private bool spamAds;
    public bool SPAM_ADS
    {
        get
        {
            return spamAds;
        }
        set
        {
            spamAds = value;
            PlayerPrefs.SetInt("spam_ads_config", spamAds ? 1 : 0);
        }
    }

    private bool drawControl;

    public bool DRAW_CONTROL
    {
        get
        {
            return drawControl;
        }
        set
        {
            drawControl = value;
            PlayerPrefs.SetInt("draw_control", drawControl ? 1 : 0);
        }
    }

    private int timeShowSpin;
    public int TIME_SHOW_SPIN
    {
        get
        {
            return timeShowSpin;
        }
        set
        {
            timeShowSpin = value;
            PlayerPrefs.SetInt("time_show_spin", timeShowSpin);
        }
    }

    private bool activeShuffle = false;

    public bool ACTIVE_SHUFFLE
    {
        get { return activeShuffle; }
        set
        {
            activeShuffle = value;
            PlayerPrefs.SetInt("active_shuffle", activeShuffle ? 1 : 0);
        }
    }
    private bool colorBackGround;
    public bool BACK_GROUND 
    {
          get { return colorBackGround; }
        set
        {
            colorBackGround = value;
            PlayerPrefs.SetInt("colorBackGround", colorBackGround? 1 : 0);
        }
    }
    #endregion

}
