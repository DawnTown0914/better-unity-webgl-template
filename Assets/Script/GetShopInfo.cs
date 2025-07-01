using FullSerializer;
using Proyecto26;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetShopInfo : MonoBehaviour
{
    private const string databaseURL = "https://winionvirus-dbad5-default-rtdb.asia-southeast1.firebasedatabase.app/";

    public delegate void GetDataCallback(ShopType shopType);
    public delegate void GetItemCallback(Item item);

    public Button OfflineButton;
    public Button OnlineButton;
    public GameObject _ClosePanel;
    public static GameObject ClosePanel;

    public static void GetShopType(GetDataCallback callback)
    {
        RestClient.Get<ShopType>($"{databaseURL}/ShopType.json").Then(shopType =>
        {
            callback(shopType);
        });
    }
    public static void GetItemData(int index, GetItemCallback callback)
    {
        RestClient.Get<Item>($"{databaseURL}/Items/Item_{index + 1}.json").Then(item =>
        {
            callback(item);
        });
    }


    public TextMeshProUGUI PopUpContext;

    public List<ItemCalculator> itemList = new List<ItemCalculator>();
    void Start()
    {
        ClosePanel = _ClosePanel;

        GetShopType((shopType) =>
        {
            OnlineButton.interactable = shopType.Online;
            OfflineButton.interactable = shopType.Offline;

            if (shopType.Open)
            {
                // 상점 오픈
                ClosePanel.SetActive(false);
                SceneController.Instance.StartLoad();
            }
            else
            {
                // 상점 클로즈
                ClosePanel.SetActive(true);
            }

            PopUpContext.text = shopType.Context.Replace("\\n", "\n");
            PopUpContext.text += " ";

            Debug.Log("Item Count : " + shopType.ItemCount);

            for (int i = 0; i < shopType.ItemCount; i++)
            {
                int index = i;
                GetItemData(i, (item) =>
                {
                    itemList[index].Price = item.Price;
                    itemList[index].Title.text = item.Description;

                    itemList[index].SalesOnOff(item.Sales);
                });
            }
        });
    }

    public static void CheckShopOpen()
    {
        GetShopType((shopType) =>
        {
            if (shopType.Open)
            {
                // 상점 오픈
                ClosePanel.SetActive(false);
            }
            else
            {
                // 상점 클로즈
                ClosePanel.SetActive(true);
            }
        });
    }

    public string WebUrl = "https://x.com/WinionVirus";
    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public TextMeshProUGUI SendName;
    public TextMeshProUGUI SendNumber;
    public TextMeshProUGUI Numbers;
    public GameObject SubmitWindow;
    public void SubmitName()
    {
        Numbers.text = GetRandomFourDigitString();
        PostUser(Numbers.text);
    }
    
    public static int GetRandomFourDigitNumber()
    {
        System.Random rand = new System.Random();
        return rand.Next(0, 10000); // 0 이상 9999 이하
    }

    public static string GetRandomFourDigitString()
    {
        int number = GetRandomFourDigitNumber();
        return number.ToString("D4"); // 항상 4자리 (앞에 0 포함)
    }

    public void PostUser(string randomNumber)
    {
        DateTime utcNow = DateTime.UtcNow;
        DateTime utcPlus9 = utcNow.AddHours(9);

        string date = utcPlus9.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초");

        int a = 0;
        int b = 0;
        int c = 0;

        int a_price = 0;
        int b_price = 0;
        int c_price = 0;

        a = itemList[0].Count;
        b = itemList[1].Count;
        c = itemList[2].Count;

        a_price = itemList[0].Price;
        b_price = itemList[1].Price;
        c_price = itemList[2].Price;

        int totalPrice = (a_price * a) + (b_price * b) + (c_price * c);

        RestClient.Post<Customer>($"{databaseURL}/Customers/PlayX4/{date}.json", new Customer(a, b, c, totalPrice, randomNumber));
    }
}
