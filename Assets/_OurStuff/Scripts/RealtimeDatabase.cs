using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;

public class RealtimeDatabase : MonoBehaviour
{
    DatabaseReference _reference;
    [SerializeField] InputField _username;
    [SerializeField] InputField _usernameToRead;

    // Start is called before the first frame update
    void Start()
    {
        _reference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    // Save User data to Firebase
    public void DataSave()
    {
        User user = new User();
        user.UserName = _username.text;
        string json = JsonUtility.ToJson(user);

        _reference.Child("User").Child(user.UserName).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if(task.IsCompleted)
            {
                Debug.Log("Successfully added data to Firebase.");
            }
            else
            {
                Debug.Log("Adding data failed.");
            }
        });
    }

    // Load User data from Firebase
    public void DataRead()
    {
        _reference.Child("User").Child(_usernameToRead.text).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Successfully retreived data from Firebase.");
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.Child("UserName").Value.ToString());
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
