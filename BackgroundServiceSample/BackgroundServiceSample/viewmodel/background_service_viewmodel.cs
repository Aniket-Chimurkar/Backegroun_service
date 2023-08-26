using BackgroundServiceSample.Model;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BackgroundServiceSample.viewmodel
{
    public class background_service_viewmodel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public BG_model model { get; set; }
        private DateTime _date1 { get; set; }
        private string _date { get; set; }
        private SQLiteConnection connection;
        public DateTime date1
        {
            get { return _date1; }
            set { _date1 = value; OnPropertyChanged(); }
        }
        public string date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }



        public async Task complaintslist(int id)
        {
            
            try
            {
                
                await Task.Run(async () =>
                {
                    var url = "https://fakestoreapi.com/products/" + Convert.ToString(id);
                    var baseAddress = new Uri(url);
                    var cookieContainer = new CookieContainer();
                    using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                    using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        model = new BG_model();
                        connection = DependencyService.Get<ISQLite>().GetConnection();
                        connection.CreateTable<BG_model>();
                        var result = await client.GetAsync(url);
                        var result_string = await result.Content.ReadAsStringAsync();
                        var jsonResponse = JsonConvert.DeserializeObject<Model.BG_model>(result_string);
                        
                        model.title = jsonResponse.title;
                        model.price = jsonResponse.price;
                        model.description= jsonResponse.description;
                        model.category=jsonResponse.category;
                        model.image = jsonResponse.image;
                        model.datee = DateTime.Now.ToString("dd-MMMM-yyyy: hh-mm-ss ");
                        connection.Insert(model);

                        model.title = "";
                        model.price = "";
                        model.description = "";
                        model.category = "";
                        model.image = "";
                        model.datee = "";


                        Console.WriteLine(result_string);
                       
                       

                    }
                });
              


            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");

            }



        }


    }
}
