using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ItemCalculator : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public CanvasGroup Quantity;
    public int Count = 0;
    public int Price = 10000;
    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI CountText;

    public GameObject CantSale;
    public void SalesOnOff(bool value)
    {
        CantSale.SetActive(!value);
    }
    void Calculate()
    {
        PriceText.text = (Count * Price).ToString("N0");
        CountText.text = Count.ToString();
    }

    public void ChangeCount(int amount)
    {
        Count = Count + amount;

        SceneController.Instance.SetPrice(SceneController.Instance.TotalPrice + (Price * amount));

        Calculate();

        if(Count <= 0)
        {
            OffQuantity();
        }
    }

    public void OnQuantity()
    {
        Quantity.alpha = 0;
        Quantity.gameObject.SetActive(true);
        Quantity.DOFade(1, 0.5f);
        
        ChangeCount(1);
    }

    public void OffQuantity()
    {
        Quantity.DOFade(0, 0.5f).OnComplete(() => Quantity.gameObject.SetActive(false));
    }

    public void SetPrice(int price)
    {
        Price = price;
        ChangeCount(0);
    }

    public void ClearData()
    {
        Count = 0;
        ChangeCount(0);
    }

    public bool ClearSelf = false;
    public static bool ClearAll = false;
    void Update()
    {
        if(ClearAll)
        {
            ClearSelf = true;
        }

        if(ClearSelf)
        {
            ClearData();
            ClearSelf = false;
        }
    }
}
