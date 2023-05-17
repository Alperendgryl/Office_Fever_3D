using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BuyArea : MonoBehaviour
{
    public GameObject desk, buyArea;
    public float cost, currentPayment, progress;
    public TMP_Text progressTxt;
    public void Buy(int moneyAmount)
    {
        currentPayment += moneyAmount;
        progress = currentPayment / cost;
        progressTxt.text = currentPayment + " / " + cost;

        if (progress >= 1)
        {
            buyArea.SetActive(false);
            desk.SetActive(true);
            this.enabled = false;
        }
    }
}
