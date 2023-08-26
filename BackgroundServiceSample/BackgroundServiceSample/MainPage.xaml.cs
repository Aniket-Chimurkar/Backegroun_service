using BackgroundServiceSample.Model;
using BackgroundServiceSample.viewmodel;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BackgroundServiceSample
{
    public partial class MainPage : ContentPage
    {
        private SQLiteConnection connection;
        public BG_model model;
        public MainPage()
        {
            InitializeComponent();
            connection = DependencyService.Get<ISQLite>().GetConnection();
            connection.CreateTable<BG_model>();
             

            BindingContext = new background_service_viewmodel();
            var vm = BindingContext as background_service_viewmodel;
            int id = 1;
            MessagingCenter.Unsubscribe<string>(this, "countervalue");
            MessagingCenter.Subscribe<string>(this, "countervalue", (value) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    counter.Text = value;
                    if (Convert.ToInt32(counter.Text) / 10 == 0)
                    {
                        

                       await vm.complaintslist(id);
                        id++;
                    }
                });
            });
        }

        private void Start_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<string>("1", "myService");
        }

        private void Stop_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<string>("0", "myService");
        }

        private void Show_Clicked(object sender, EventArgs e)
        {
            var data = (from bg in connection.Table<BG_model>() select bg);
            datalist.ItemsSource = data;
        }
    }
}
