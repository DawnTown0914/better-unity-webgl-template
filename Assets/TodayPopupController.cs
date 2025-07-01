using UnityEngine;
using UnityEngine.UI;
using System;

public class TodayPopupController : MonoBehaviour
{
    public GameObject popupUI;         // 보여줄 팝업 오브젝트
    public Button closeButton;         // 그냥 닫기 버튼
    public Button dontShowTodayButton; // 오늘 하루 보지 않기 버튼

    private const string PREF_KEY = "PopupHideDate";

    void Start()
    {
        if (ShouldShowPopup())
        {
            popupUI.SetActive(true);
        }
        else
        {
            popupUI.SetActive(false);
        }

        closeButton.onClick.AddListener(() => popupUI.SetActive(false));
        dontShowTodayButton.onClick.AddListener(DontShowToday);
    }

    bool ShouldShowPopup()
    {
        string savedDate = PlayerPrefs.GetString(PREF_KEY, "");
        string today = DateTime.Now.ToString("yyyyMMdd");
        return savedDate != today;
    }

    void DontShowToday()
    {
        string today = DateTime.Now.ToString("yyyyMMdd");
        PlayerPrefs.SetString(PREF_KEY, today);
        PlayerPrefs.Save();
        popupUI.SetActive(false);
        
    }
}