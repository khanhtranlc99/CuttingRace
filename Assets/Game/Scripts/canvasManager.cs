using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class canvasManager : MonoBehaviour
{
    public static canvasManager Instance;

    [Header("Panels")]
    public GameObject winPanel;
    public GameObject failPanel;
    public GameObject gameplayPanel;
    public GameObject menupanel;

    public Transform itemParent;
    public Sprite[] itemList;

    [HideInInspector]
    public int coins;

    [Header("Coins")]
    public TextMeshProUGUI shopcoinText;
    public TextMeshProUGUI maincoinText;

    [Header("Levels")]
    public TextMeshProUGUI levelText;
    public bool gamestart = false;

    //[Header("Shop")]
    //public Transform shopCharParent;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < itemParent.childCount; i++)
        {
            itemParent.GetChild(i).GetChild(2).GetComponent<Image>().sprite = itemList[i];
        }
        coins = PlayerPrefs.GetInt("coin", 0);
        DisplayInfo();
        //SetShopChar();

    }

    //public void SetShopChar()
    //{
    //    foreach (Transform sC in shopCharParent)
    //    {
    //        sC.gameObject.SetActive(false);
    //    }
    //    shopCharParent.GetChild(PlayerPrefs.GetInt("charno", 0)).gameObject.SetActive(true);
    //}

    public void DisplayInfo()
    {
        shopcoinText.text = coins.ToString();
        maincoinText.text = coins.ToString();
        levelText.text = "Level " + PlayerPrefs.GetInt("levelshow", 1);

    }

    public void NextLevel()
    {
        coins += 50;
        PlayerPrefs.SetInt("coin", coins);
        coins = PlayerPrefs.GetInt("coin", 0);
        DisplayInfo();
        winPanel.SetActive(false);
        menupanel.SetActive(true);
        gameplayPanel.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        gamestart = true; 
    }
    public void WinMethod(float sec)
    {
        StartCoroutine(win(sec));
    }
    IEnumerator win(float sec)
    {
        yield return new WaitForSeconds(sec);
        winPanel.SetActive(true);
        gameplayPanel.SetActive(false);
            
    }

    public void AddCoins()
    {
        if (UnityAdsManager.instance)
        {
            UnityAdsManager.instance.forcontine = true;
            UnityAdsManager.instance.ShowRewardedVideo();

        }
    }

    public void RewardCoins()
    {
        coins = coins + 150;
        PlayerPrefs.SetInt("coin", coins);
        DisplayInfo();
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://ravestudioprivacypolicy.blogspot.com/2021/02/rave-studio-february-042021-rave-studio.html");
    }
    public void UnlockChar()
    {
        if (coins>=150)
        {
           
            int unlocked = 0;
            for (int i = 0; i < itemParent.childCount; i++)
            {
                shopitem s1 = itemParent.GetChild(i).GetComponent<shopitem>();
                if (s1.isSelected == true)
                    unlocked++;

            }

            if (unlocked > itemParent.childCount - 1)
            {

            }
            else
            {
               
                int rn = Random.Range(1, itemParent.childCount);
                shopitem s = itemParent.GetChild(rn).GetComponent<shopitem>();
                if (s.isSelected == false)
                {
                    print("Unlock____Character");
                    coins = coins - 150;
                    PlayerPrefs.SetInt("coin", coins);
                    s.UnlockChar();
                }
                else
                    UnlockChar();
               
            }

             DisplayInfo();
        }

    }
}
