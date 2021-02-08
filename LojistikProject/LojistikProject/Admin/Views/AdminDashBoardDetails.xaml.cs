using LojistikProject.Models;
using LojistikProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace LojistikProject.Admin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminDashBoardDetails : ContentPage
    {
        //Map map = new Map()
        //{

        //};
        public AdminDashBoardDetails()
        {
            //double latitude,double altitude
            InitializeComponent();
            search.Source = ImageSource.FromResource("LojistikProject.Image.searchicon.png");

            GetListofClients();

        }
   
        private async void GetListofClients()
        {
            var response = await ApiServices.GetRegisteredUsers();
            foreach (Kullanici user in response)
            {
                Pin pin = new Pin
                {
                    Label = user.UserName,
                    Type = PinType.SavedPin,
                    Position = new Position(user.UserLatitude, user.UserAltitude)                    
                };
                Position position = new Position(user.UserLatitude, user.UserAltitude);
                MapSpan mapSpan = new MapSpan(position, user.UserLatitude, user.UserAltitude);
                Map map = new Map(mapSpan);
                MyMap.Pins.Add(pin);
            }
            //MyMap.ItemsSource = response;
        }
        //void TapGestureRecognizer_Tapped(Object sender, EventArgs e)
        //{
        //    Label lblClicked = (Label)sender;
        //    var item = (TapGestureRecognizer)lblClicked.GestureRecognizers[0];
        //    var tappedItem = item.CommandParameter as Kullanici;
        //}

    }
}