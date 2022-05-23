using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopitem : MonoBehaviour
{
    public bool isSelected = false;

    private void Start()
    {
        if(PlayerPrefs.GetString(transform.name, "false")=="true"||isSelected)
        {
            isSelected = true;
            foreach (Transform obj in transform)
            {
                obj.gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("charno", 0) != transform.GetSiblingIndex())
                transform.GetChild(1).gameObject.SetActive(false);
        }

       
      
    }



    public void ChooseItem()
    {
        if(isSelected)
        {
            isSelected = true;
            PlayerPrefs.SetInt("charno", transform.GetSiblingIndex());

            foreach (Transform t in transform.parent)
            {
                t.GetChild(1).gameObject.SetActive(false);
            }

            foreach (Transform obj in transform)
            {
                obj.gameObject.SetActive(true);
            }
            BallMovement.Instance.ChangeChar();
            PlayerPrefs.SetString(transform.name, "true");
            //canvasManager.Instance.SetShopChar();
        }

    }

    public void UnlockChar()
    {
        isSelected = true;
        PlayerPrefs.SetInt("charno", transform.GetSiblingIndex());

        foreach (Transform t in transform.parent)
        {
            t.GetChild(1).gameObject.SetActive(false);
        }

        foreach (Transform obj in transform)
        {
            obj.gameObject.SetActive(true);
        }
        BallMovement.Instance.ChangeChar();
        PlayerPrefs.SetString(transform.name, "true");
        //canvasManager.Instance.SetShopChar();

    }
}
