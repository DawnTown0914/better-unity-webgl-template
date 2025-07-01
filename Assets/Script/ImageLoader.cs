using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class ImageLoader : MonoBehaviour
{
    public string imageUrl = "https://github.com/user-attachments/assets/acc51146-6ef6-4e2b-8700-49fa80b7e45f";
    public RawImage rawImage;   // 또는

    void Start()
    {
        // StartCoroutine(DownloadImage());
        Debug.Log(Texture2DToBase64(rawImage.mainTexture as Texture2D));
    }

    public static string Texture2DToBase64(Texture2D texture)
    {
        byte[] imageData = texture.EncodeToPNG();
        return Convert.ToBase64String(imageData);
    }
    

    IEnumerator DownloadImage()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("이미지 다운로드 실패: " + request.error);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);

            // RawImage에 적용 (직접 Texture 사용)
            if (rawImage != null)
            {
                rawImage.texture = texture;
            }
        }
    }
}