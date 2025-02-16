﻿using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Shiny;
using Shiny.Notifications;
using Xamarin.Forms;


namespace Sample
{
    public class OtherViewModel : SampleViewModel
    {
        readonly INotificationManager notifications;
        IDisposable? sub;


        public OtherViewModel()
        {
            this.notifications = ShinyHost.Resolve<INotificationManager>();
            this.ClearBadge = new Command(() => this.Badge = 0);

            this.PermissionCheck = new Command(async () =>
            {
                var result = await this.notifications.RequestAccess(AccessRequestFlags.All);
                await this.Alert("Permission Check Result: " + result);
            });

            this.StartChat = new Command(async () =>
                await this.notifications.Send(
                    "Shiny Chat",
                    "Hi, What's your name?",
                    "ChatName",
                    DateTime.Now.AddSeconds(10)
                )
            );

            this.QuickSend = new Command(async () =>
            {
                await this.notifications.Send("QUICK SEND TITLE", "This is a quick message");
            });
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.Badge = this.notifications.Badge;

            this.sub = this.WhenAnyProperty(x => x.Badge)
                .Skip(1)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .DistinctUntilChanged()
                .Subscribe(badge => this.notifications.Badge = badge);
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.sub?.Dispose();
        }


        public ICommand QuickSend { get; }
        public ICommand StartChat { get; }
        public ICommand PermissionCheck { get; }
        public ICommand ClearBadge { get; }


        int badge;
        public int Badge
        {
            get => this.badge;
            set => this.Set(ref this.badge, value);
        }
    }
}

