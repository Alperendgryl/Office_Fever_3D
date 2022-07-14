using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    public int money = 0;
    public TMP_Text moneyTXT;
    private void Update()
    {
        moneyTXT.text = money.ToString() + "$";
    }
    private void OnEnable()
    {
        EventManager.onMoneyCollected += MoneyCollected;
        EventManager.onBuyDesk += buyArea;
    }

    private void OnDisable()
    {
        EventManager.onMoneyCollected -= MoneyCollected;
        EventManager.onBuyDesk -= buyArea;
    }

    void buyArea()
    {
        if(EventManager.areaToBuy != null && money > 0)
        {
            EventManager.areaToBuy.Buy(1);
            money -= 1;
        }
    }
    void MoneyCollected()
    {
        money++;
    }
}
