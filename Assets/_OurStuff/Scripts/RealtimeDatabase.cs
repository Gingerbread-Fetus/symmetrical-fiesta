using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;

public class RealtimeDatabase : MonoBehaviour
{
    NotificationHandler notificationHandler;
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

        // Listen for notifications
        _reference
        .Child("User")
        .Child(user.UserName)
        .ValueChanged += HandleValueChanged;
    }

	void HandleValueChanged(object sender, ValueChangedEventArgs args)
	{
		if( args.DatabaseError != null )
		{
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
		// Do something with the data in args.Snapshot
		if( notificationHandler != null )
		{
			notificationHandler.SendNotification("Title", args.Snapshot.ToString());
		}
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
