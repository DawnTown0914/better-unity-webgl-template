using System;

/// <summary>
/// The user class, which gets uploaded to the Firebase Database
/// </summary>

[Serializable] // This makes the class able to be serialized into a JSON
public class ShopType
{
    public bool Offline = false;
    public bool Online = false;
    public bool Open = false;
    public int ItemCount = 3;
    public string Context = "PlayX4 한정 특별 이벤트!\n위니언 부스에 오신 걸 환영합니다!\n​\n오프라인 판매 품목\n ​ • 띠부씰 45종\n ​ • 경단인형 6종\n ​ • 디버그 봉제인형\n​\n온라인 한정 굿즈\n ​ • 위니언 티셔츠\n ​ • 위니언 키캡\n ​ • 위니언 장패드\n​\n위니언 첫 오프라인 행사 혜택!\n ​ • 띠부씰 1,000원 할인!\n ​ • 경단인형 5개 구매 시 1개 더!\n ​ • 5만원 이상 구매 시 ‘위니언 바이러스’\n ​ ​ ​ 스팀 코드 쿠폰 무료 증정!\n​\n- 던타운 스튜디오 드림 -";
}
