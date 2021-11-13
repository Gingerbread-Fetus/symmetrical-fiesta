using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
using Unity.Notifications.Android;  // from mobile notifications package

public class NotificationHandler : MonoBehaviour
{

	void Awake()
	{
	}

	// Start is called before the first frame update
	void Start()
	{
		var channel = new AndroidNotificationChannel()
		{
			Id = "channel_id",
			Name = "Default Channel",
			Importance = Importance.Default,
			Description = "Generic notifications",
		};
		AndroidNotificationCenter.RegisterNotificationChannel(channel);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void SendNotification(string title, string body)
	{
		var notification = new AndroidNotification();
		notification.Title = title;
		notification.Text = body;
		notification.FireTime = System.DateTime.Now;

		AndroidNotificationCenter.SendNotification(notification, "channel_id");
	}
}
