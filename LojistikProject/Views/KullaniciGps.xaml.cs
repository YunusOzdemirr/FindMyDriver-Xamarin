using LojistikProject.Models;
using LojistikProject.Services;
using LojistikProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LojistikProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class KullaniciGps : ContentPage
    {
        LoginViewModel lg;
        bool olay;

        private Guid id;
        private string userName;

        public KullaniciGps(Guid id, string userName)
        {

            InitializeComponent();
            this.id = id;
            this.userName = userName;
            lg = new LoginViewModel(id, userName);
            BindingContext = lg;
            //KullaniciGpsBackground.BackgroundImageSource = ImageSource.FromResource("LojistikProject.Image.kırmızıbackground.jpg");
            Salim.Clicked += Salim_Clicked;
            LblSwtchDeger.Text = "Konum Bilginiz Gönderilmiyor";

        }


        private async void Salim_Clicked(object sender, EventArgs e)
        {
            if (olay)
            {
                olay = false;
                LblSwtchDeger.Text = "Konum Bilginiz Gönderilmiyor";
                LblSwtchDeger.TextColor = Color.White;

            }
            else
            {
                olay = true;
                LblSwtchDeger.Text = "Konum Bilginiz Alınıyor";
                LblSwtchDeger.TextColor = Color.Red;

                try
                {
                    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        await App.Current.MainPage.DisplayAlert("İnternet Bağlantısı Sorunu", "İnternet bağlantınızı kontrol edin", "Tamam");
                    }
                    else
                    {
                        do
                        {
                            try
                            {
                                var location = await Geolocation.GetLastKnownLocationAsync();
                                if (location != null)
                                {
                                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                                    {
                                        DesiredAccuracy = GeolocationAccuracy.High,
                                        Timeout = TimeSpan.FromSeconds(10)
                                    }); ;
                                    lblLongtitude.Text = location.Latitude.ToString();
                                    lblAltitude.Text = location.Longitude.ToString();
                                }
                                if (location == null)
                                {
                                    await App.Current.MainPage.DisplayAlert("Gps Bulunamadı", "Gpsinizin açık olduğundan emin olun", "Tamam");
                                }
                                else
                                {
                                    lblLongtitude.Text = location.Latitude.ToString();
                                    lblAltitude.Text = location.Longitude.ToString();
                                }
                                await ApiServices.UpdatePerson(Guid.Parse(LabelUserId.Text), double.Parse(lblLongtitude.Text), double.Parse(lblAltitude.Text));
                            }
                            catch (Exception ex)
                            {
                                await DisplayAlert($"Hata", $"Konum alınamadı geliştirici ile iletişime geçin {ex.Message}", "Tamam");

                            }
                        } while (olay == true);
                    }
                }
                catch (Exception exx)
                {
                    await DisplayAlert($"Hata", $"Konum alınamadı geliştirici ile iletişime geçin {exx.Message}", "Tamam");
                    throw;
                }


            }
        }

    }
}