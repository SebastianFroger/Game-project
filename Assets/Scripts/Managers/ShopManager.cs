using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItem
{
    public GameObject itemSO; // change to upgradeSO?
    public TMPro.TMP_Text title;
    public TMPro.TMP_Text description;
}

public class ShopManager : Singleton<ShopManager>
{
    public ShopItem[] shopItems;

    public void DisplayItems()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {

        }
    }
}
