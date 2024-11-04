using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Baker")]
    public ShopButton bakerButton;
    public float bakerPrice = 10;
    public int bakerCount = 0;
    public int cpb = 1; // Clicks per baker
    public float cookTime = 1; // Time to cook 1 click

    [Header("Clicker upgrade")]
    public ShopButton clickerUpgradeButton;
    public float clickerUpgradePrice = 100;
    public int clickerUpgradeMultiplier = 2;
    public GameObject newClickerModel;

    private Clicker clicker;

    private void Start() 
    {
        clicker = FindObjectOfType<Clicker>();
        InvokeRepeating("Cook", 0, cookTime);
    }

    public void BuyBaker()
    {
        var realPrice = (int)Mathf.Ceil(bakerPrice);
        if (clicker.clicks >= realPrice)
        {
            clicker.clicks -= realPrice;
            UiManager.instance.UpdateClicks(clicker.clicks, clicker.totalCPS);

            bakerPrice *= 1.15f; // 15% increase
            realPrice = (int)Mathf.Ceil(bakerPrice);
            bakerButton.UpdateText(realPrice, ++bakerCount);

            clicker.totalCPS += cpb;
        }
    }

    public void BuyClickerUpgrade()
    {
        var realPrice = (int)Mathf.Ceil(clickerUpgradePrice);
        if (clicker.clicks >= realPrice)
        {
            clicker.clicks -= realPrice;
            UiManager.instance.UpdateClicks(clicker.clicks, clicker.totalCPS);

            clickerUpgradeButton.Destroy();
        }
    }

    public void Cook()
    {
        var particleCount = Mathf.Min(bakerCount * cpb, 100);
        clicker.clickParticles.Emit(particleCount);
        
        clicker.clicks += bakerCount * cpb;
        UiManager.instance.UpdateClicks(clicker.clicks, clicker.totalCPS);
    }
}
