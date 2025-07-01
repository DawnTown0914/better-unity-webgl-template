using System;

/// <summary>
/// The user class, which gets uploaded to the Firebase Database
/// </summary>

[Serializable] // This makes the class able to be serialized into a JSON
public class Customer
{
    public int _1_Seal;
    public int _2_Doll;
    public int _3_Debug;
    public int _4_totalPrice;
    public string _0_OrderNumber;

    public Customer(int a, int b, int c, int d, string e)
    {
        _0_OrderNumber = e;
        _1_Seal = a;
        _2_Doll = b;
        _3_Debug = c;
        _4_totalPrice = d;
    }
}