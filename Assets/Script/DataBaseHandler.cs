using FullSerializer;
using Proyecto26;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataBaseHandler : MonoBehaviour
{
    private const string databaseURL = "https://winionvirus-default-rtdb.asia-southeast1.firebasedatabase.app/";

    public delegate void PostUserCallback();
    public delegate void PutUserCallback();
    public delegate void GetUserCallback(User user);
    public delegate void GetUsersCallback(Dictionary<string, User> users);

    private static fsSerializer serializer = new fsSerializer();

    public TMP_InputField userName;
    public TMP_InputField userScore;
    public TMP_InputField userAge;

    private void Start()
    {
        // GetUserByKey("-OP6CP584TCco2LCz-25", (user) => {
        //     Debug.Log(user.age);
        // });

        // PostUser(new User("Unial1", 100, 20), "Post Player");
        // PutUser(new User("Unial1", 100, 20), "Put Player1", () => Debug.Log("PutUser"));
        // PutUser(new User("Unial2", 200, 21), "Put Player2", () => Debug.Log("PutUser"));
        // PutUser(new User("Unial3", 200, 23), "Put Player3", () => Debug.Log("PutUser"));

        // GetUsers((users) =>
        // {
        //     foreach (var user in users)
        //     {
        //         Debug.Log($"Name : {user.Value.name}\n Socre : {user.Value.score} \n Age : {user.Value.age}");
        //     }
        // });
    }

    public void PostUserButton()
    {
        PostUser(new User(userName.text, int.Parse(userScore.text), int.Parse(userAge.text)), null);
    }

    /// <summary>
    /// Adds a user to the Firebase Database
    /// </summary>
    /// <param name="user"> User object that will be uploaded </param>
    /// <param name="userId"> Id of the user that will be uploaded </param>
    public static void PostUser(User user, string userId, PostUserCallback callback = null)
    {
        RestClient.Post<User>($"{databaseURL}/users/{userId}.json", user).Then(response =>
        {
            Debug.Log("Key : " + response.name);
            callback?.Invoke();
        });
    }

    /// <summary>
    /// Adds a user to the Firebase Database
    /// </summary>
    /// <param name="user"> User object that will be uploaded </param>
    /// <param name="userId"> Id of the user that will be uploaded </param>
    public static void PutUser(User user, string userId, PutUserCallback callback = null)
    {
        RestClient.Put<User>($"{databaseURL}/users/{userId}.json", user).Then(response =>
        {
            callback?.Invoke();
        });
    }
    /// <summary>
    /// Retrieves a user from the Firebase Database, given their id
    /// </summary>
    /// <param name="userId"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    public static void GetUser(string userId, GetUserCallback callback)
    {
        RestClient.Get<User>($"{databaseURL}/users/{userId}.json").Then(user =>
        {
            callback(user);
        });
    }
    public static void GetUserByKey(string key, GetUserCallback callback)
    {
        // https://winionvirus-default-rtdb.asia-southeast1.firebasedatabase.app/users/Post%20Player/-OP6CP584TCco2LCz-25
        RestClient.Get<User>($"{databaseURL}/users/Post%20Player/{key}.json").Then(user =>
        {
            callback(user);
        });
    }

    /// <summary>
    /// Gets all users from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}/users.json").Then(response =>
        {
            var responseJson = response.Text;
            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, User>), ref deserialized);
            var users = deserialized as Dictionary<string, User>;
            callback(users);
        });
    }
}