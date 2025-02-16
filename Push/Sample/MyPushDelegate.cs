﻿using System;
using System.Threading.Tasks;
using Shiny.Push;


namespace Sample
{
    public class MyPushDelegate : IPushDelegate
    {
        readonly SampleSqliteConnection conn;
        readonly IPushManager pushManager;


        public MyPushDelegate(SampleSqliteConnection conn, IPushManager pushManager)
        {
            this.conn = conn;
            this.pushManager = pushManager;
        }

        public Task OnEntry(PushNotificationResponse push)
            => this.Insert("PUSH ENTRY");

        public Task OnReceived(PushNotification push)
            => this.Insert("PUSH RECEIVED");

        public Task OnTokenRefreshed(string token)
            => this.Insert("PUSH TOKEN REFRESH");

        Task Insert(string info) => this.conn.InsertAsync(new ShinyEvent
        {
            Text = info,
            Detail = "Token: " + this.pushManager.CurrentRegistrationToken,
            Timestamp = DateTime.UtcNow
        });
    }
}
