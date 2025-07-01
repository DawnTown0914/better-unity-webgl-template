using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SceneController : SingletoneBehaviour<SceneController>
{
    public CanvasGroup Logo;
    public CanvasGroup LogoImage;
    public CanvasGroup FirstDepth;
    public CanvasGroup SecondDepth;
    public CanvasGroup ThirdDepth;

    public CanvasGroup LobbyButton;
    public TextMeshProUGUI TotalPriceText;
    public int TotalPrice = 0;

    public void SetPrice(int price)
    {
        TotalPrice = price;
        TotalPriceText.text = "<size=65>총</size> " + price.ToString("N0") + "<size=65>원";
    }

    public void GoToDepth(int depth)
    {
        switch(depth)
        {
            case 1:
                FirstDepth.gameObject.SetActive(true);
                FirstDepth.DOFade(1, 0.5f);
                SecondDepth.DOFade(0, 0.5f).OnComplete(() => { SecondDepth.gameObject.SetActive(false); });
                ThirdDepth.DOFade(0, 0.5f).OnComplete(() => { ThirdDepth.gameObject.SetActive(false); });
                LobbyButton.DOFade(0,0.5f).OnComplete(() => LobbyButton.blocksRaycasts = false );

                SetPrice(0);
                BroadcastMessage("ClearData");
                break;
            case 2:
                SecondDepth.gameObject.SetActive(true);
                SecondDepth.DOFade(1, 0.5f);
                FirstDepth.DOFade(0, 0.5f).OnComplete(() => { FirstDepth.gameObject.SetActive(false); });
                ThirdDepth.DOFade(0, 0.5f).OnComplete(() => { ThirdDepth.gameObject.SetActive(false); });
                LobbyButton.DOFade(1,0.5f).OnComplete(() => LobbyButton.blocksRaycasts = true );

                ItemCalculator.ClearAll = false;
                break;
            case 3:
                ThirdDepth.gameObject.SetActive(true);
                ThirdDepth.DOFade(1, 0.5f);
                FirstDepth.DOFade(0, 0.5f).OnComplete(() => { FirstDepth.gameObject.SetActive(false); });
                SecondDepth.DOFade(0, 0.5f).OnComplete(() => { SecondDepth.gameObject.SetActive(false); }); 
                LobbyButton.DOFade(1,0.5f);

                ItemCalculator.ClearAll = false;
                break;

        }
    }

    public void StartLoad()
    {
        Logo.alpha = 1;
        LogoImage.alpha = 0;
        

        LogoImage.DOFade(1, 1f).OnComplete(() => {

            GoToDepth(1);

            LogoImage.DOFade(0, 1f).OnComplete(() => {
                Logo.DOFade(0, 1f).OnComplete(() => Logo.gameObject.SetActive(false));
            });
        });
    }

    public void CopyNumber()
    {
        GUIUtility.systemCopyBuffer = "국민은행 801301-01-781221";
    }
}
