using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetRatio : MonoBehaviour
{
    public TMP_Text tMP_Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PrintAspectRatio();
    }

    int GetGCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    void PrintAspectRatio()
    {
        int width = Screen.width;
        int height = Screen.height;
        int gcd = GetGCD(width, height);

        int aspectX = width / gcd;
        int aspectY = height / gcd;

        tMP_Text.text = ($"Aspect Ratio: {aspectX}:{aspectY}");
    }
}
