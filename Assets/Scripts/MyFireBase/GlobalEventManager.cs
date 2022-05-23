using System;
using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    #region Instance

    private static GlobalEventManager instance;

    public static GlobalEventManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GlobalEventManager>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    public static bool Exist => instance;

    #endregion

    #region Events
    public Action<string, Parameter[]> EvtSendEvent;
    public Action EvtDelayJump;
    public Action EvtDisableMagnet;
    public Action EvtUpdateUserProperties;
    public Action EvtOnEnemyKill;
    public Action EvtTutorialStart;

    public Action<Sprite> EvtOnSetLetter;
    public Action<float> EvtClaimBossPosition;
    public Action<bool> EvtPickupChestKey;
    public Action<bool> EvtOnSwitch;

    

    #endregion

    #region Tracking Level
    public void OnLevelWin(int level)
    {
        Parameter[] pa = new Parameter[]
        {
            new Parameter("level", level),          
        };
        EvtSendEvent?.Invoke("level_complete", pa);
    }
    public void OnLevelWinzzzzz(int level)
    {
        EvtSendEvent?.Invoke($"z_win_level_{level}", null);
    }
    public void OnLevelLose(int level)
    {
        Parameter[] pa = new Parameter[]
        {
            new Parameter("level", level),
     
        };

        EvtSendEvent?.Invoke("level_fail", pa);
    }
    public void OnLevelLosezzzzz(int level)
    {
        Parameter[] pa = new Parameter[]
        {
            new Parameter("replay_times", PlayerPrefs.GetInt($"REPLAY_TIMES_LEVEL_{level}", 0)),
        };
        EvtSendEvent?.Invoke($"z_lose_level_{level}", pa);
    }
    public void OnLevelSkip(int level)
    {
        EvtSendEvent?.Invoke($"z_skip_level_{level}", null);
    }
    //public void OnLevelPlay(int level)
    //{
    //    Parameter[] pa = new Parameter[]
    //    {
    //        new Parameter("level", level),
    //        new Parameter("current_gold", PlayerData.Instance.COIN.ToString())
    //    };

    //    EvtSendEvent?.Invoke("level_start", pa);
    //}
    public void OnLevelPlayzzzzz(int level)
    {
        EvtSendEvent?.Invoke($"z_play_level_{level}", null);
    }
    #endregion Tracking Level

    #region Tracking Loading
    public void OnLoadingStart()
    {
        EvtSendEvent?.Invoke("Loading_Start", null);
    }

    public void OnLoadingEnd()
    {
        EvtSendEvent?.Invoke("Loading_End", null);
    }
    #endregion Tracking Loading

    #region Tracking ADS
    public void OnInterLoad()
    {
        EvtSendEvent?.Invoke("ad_inter_load", null);
    }
    public void OnInterShow()
    {
        EvtSendEvent?.Invoke("ad_inter_show", null);
    }
    public void OnInterClick()
    {
        EvtSendEvent?.Invoke("ad_inter_click", null);
    }
    public void OnInterFail(string msg)
    {

        Parameter[] pa = new Parameter[]
        {
            new Parameter("message", msg),
        };

        EvtSendEvent?.Invoke("ad_inter_fail", pa);
    }

    public void OnRewardOffer(string placement)
    {
        Parameter[] pa = new Parameter[]
        {
            new Parameter("placement", placement),
        };

        EvtSendEvent?.Invoke("ads_reward_offer", pa);
    }
    public void OnRewardClick(string placement)
    {
        Parameter[] pa = new Parameter[]
        {
            new Parameter("placement", placement),
        };

        EvtSendEvent?.Invoke("ads_reward_click", pa);
    }
    public void OnRewardShow(string placement)
    {
        Parameter[] pa = new Parameter[]
        {
            new Parameter("placement", placement),
        };

        EvtSendEvent?.Invoke("ads_reward_show", pa);
    }
    public void OnRewardFail(string placement, string msg)
    {
        Parameter[] pa = new Parameter[]
        {
            new Parameter("placement", placement),
            new Parameter("message", msg),
        };

        EvtSendEvent?.Invoke("ads_reward_fail", pa);
    }
    public void OnRewardComplete(string placement)
    {
        Parameter[] pa = new Parameter[]
        {
            new Parameter("placement", placement),
        };

        EvtSendEvent?.Invoke("ads_reward_complete", pa);
    }
    #endregion

    #region Tracking Currency
    public void OnSpendCurrency(string curName, long value, string itemName)
    {
        Parameter[] pa = new Parameter[]
        {
            new Parameter("virtual_currency_name", curName),
            new Parameter("value", value),
            new Parameter("item_name", itemName),
        };

        EvtSendEvent?.Invoke("spend_virtual_currency", pa);
    }

    public void OnEarnCurrency(string curName, long value, string source)
    {
        Parameter[] pa = new Parameter[]
        {
            new Parameter("virtual_currency_name", curName),
            new Parameter("value", value),
            new Parameter("source", source),
        };

        EvtSendEvent?.Invoke("earn_virtual_currency", pa);
    }
    #endregion
}
