using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class UnityAdsManager : MonoBehaviour, IUnityAdsListener
{
    public static UnityAdsManager instance;
    string gameId = "4322699";
    bool testMode = false;
    public bool forcontine;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        // showbanner();
         Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        StartCoroutine(ShowBannerWhenInitialized());
    }
    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show("Banner_Android");
    }
    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("Interstitial_Android");
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    public void ShowRewardedVideo()
    {
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
           if(forcontine)
            {
                forcontine = false;
                canvasManager.Instance.RewardCoins();
            }
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == "Rewarded_Android")
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
