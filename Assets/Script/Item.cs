using System;

/// <summary>
/// The user class, which gets uploaded to the Firebase Database
/// </summary>

[Serializable] // This makes the class able to be serialized into a JSON
public class Item
{
    public string Description;
    public int Price;
    public bool Sales = false;
}