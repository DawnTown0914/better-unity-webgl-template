using System;

/// <summary>
/// The user class, which gets uploaded to the Firebase Database
/// </summary>

[Serializable] // This makes the class able to be serialized into a JSON
public class User
{
    public string name;
    public int score;
    public int age;

    public User(string name, int score, int age)
    {
        this.name = name;
        this.score = score;
        this.age = age;
    }
}