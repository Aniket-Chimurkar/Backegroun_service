using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BackgroundServiceSample.Droid
{
    [Service(Label ="BackgroundService")]
    public class BackgroundService : Service
    {
        int counter = 0;
        bool isRunningTimer = true;
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                MessagingCenter.Send<string>(counter.ToString(), "countervalue");
                counter += 1;
                return isRunningTimer;
            });
            return StartCommandResult.Sticky;
        }





        public override void OnDestroy()
        {
            StopSelf();
            counter = 0;
            isRunningTimer = false;
            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}