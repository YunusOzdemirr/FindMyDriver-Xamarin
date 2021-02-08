using LojistikProject.Models;
using LojistikProject.Services;
using LojistikProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LojistikProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnaSayfa : TabbedPage
    {
        // private string UserrId;
        LoginViewModel login;
        public AnaSayfa()
        {

            InitializeComponent();

            imageUserGiris.Source = ImageSource.FromResource("LojistikProject.Image.cmnsoftlogo.png");
            imageUserKayit.Source = ImageSource.FromResource("LojistikProject.Image.cmnsoftlogo.png");
            // Kayit.Icon = ImageSource.FromResource("LojistikProject.Image.useradd.png");
            // Giris.Icon = ImageSource.FromResource("LojistikProject.Image.userEnter.png");
            // imageAdminKayit.Source = ImageSource.FromResource("LojistikProject.Image.cmnsoft logo beyazlı.png");
            login = new LoginViewModel();
            BindingContext = login;
            //BtnAdminKayitOl.Clicked += BtnAdminKayitOl_Clicked;
            BtnUserKayitOl.Clicked += BtnUserKayitOl_Clicked1;
            BtnUserGirisBizeUlasın.Clicked += BtnUserGirisBizeUlasın_Clicked;
            BtnUserKayitBizeUlasın.Clicked += BtnUserKayitBizeUlasın_Clicked;

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                // Connection to internet is available
            }
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                DisplayAlert("İnternet Bağlantısı Sorunu", "İnternet bağlantınızı kontrol edin", "Tamam");
            }

        }

        //user signIn
        private async void BtnUserKayitOl_Clicked1(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("İnternet Bağlantısı Sorunu", "İnternet bağlantınızı kontrol edin", "Tamam");
            }
            else
            {

                var response = await ApiServices.Admin(EntUserSirketKoduKayit.Text);

                if (string.IsNullOrEmpty(EntUserAdSoyadKayit.Text) || string.IsNullOrEmpty(EntUserTelNoKayit.Text) || string.IsNullOrEmpty(EntUserSifreKayit.Text) || string.IsNullOrEmpty(EntUserSirketKoduKayit.Text) || response == null)
                {
                    await App.Current.MainPage.DisplayAlert("Hatalı kayit", "Lütfen bilgileri eksiksiz ve doğru giriniz", "tamam");
                    EntUserAdSoyadKayit.Text = string.Empty;
                    EntUserTelNoKayit.Text = string.Empty;
                    EntUserSifreKayit.Text = string.Empty;
                    EntUserSirketKoduKayit.Text = string.Empty;
                }
                else
                {
                    try
                    {
                        var location = await Geolocation.GetLastKnownLocationAsync();
                        if (location == null)
                        {
                            location = await Geolocation.GetLocationAsync(new GeolocationRequest
                            {
                                DesiredAccuracy = GeolocationAccuracy.High,
                                Timeout = TimeSpan.FromSeconds(10)
                            }); ;
                            //var isupdate = await ApiServices.AddGps(GPS);
                            GpsLatiude.Text = location.Latitude.ToString();
                            GpsAltitudee.Text = location.Altitude.ToString();
                        }
                        if (location == null)
                        {
                            await App.Current.MainPage.DisplayAlert("Gps Bulunamadı", "Gpsinizin açık olduğundan emin olun", "Tamam");
                        }
                        else
                        {
                            GpsLatiude.Text = location.Latitude.ToString();
                            GpsAltitudee.Text = location.Altitude.ToString();
                        }
                    }
                    catch (Exception)
                    {
                    }
                    var check = await ApiServices.GetPerson(EntUserTelNoKayit.Text,EntUserAdSoyadKayit.Text);
                    if (check != null)
                    {
                        await App.Current.MainPage.DisplayAlert("Hatalı kayit", "Böyle bir kullanıcı mevcut", "tamam");

                    }
                    else
                    {
                        var response1 = await ApiServices.AddPerson(EntUserAdSoyadKayit.Text, EntUserTelNoKayit.Text, EntUserSifreKayit.Text, EntUserSirketKoduKayit.Text, double.Parse(GpsLatiude.Text), double.Parse(GpsAltitudee.Text));
                        if (response1 == false)
                        {
                            await App.Current.MainPage.DisplayAlert("Başarılı", "Kullanıcı Başarı İle Oluşturuldu", "Tamam");
                            EntUserAdSoyadKayit.Text = string.Empty;
                            EntUserTelNoKayit.Text = string.Empty;
                            EntUserSifreKayit.Text = string.Empty;
                            EntUserSirketKoduKayit.Text = string.Empty;


                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Uyarı", "Bilgileri Kontrol Ediniz", "Tamam");
                        }
                    }
                }

            }


        }

        private async void BtnUserGirisBizeUlasın_Clicked(object sender, EventArgs e)
        {
            await Email.ComposeAsync("", "", "info@cmnsoftware.com");
        }

        private async void BtnUserKayitBizeUlasın_Clicked(object sender, EventArgs e)
        {
            await Email.ComposeAsync("", "", "info@cmnsoftware.com");

        }

      

    }
}