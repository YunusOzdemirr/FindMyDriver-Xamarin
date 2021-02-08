using LojistikProject.Admin.Views;
using LojistikProject.Models;
using LojistikProject.Services;
using LojistikProject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LojistikProject.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel(Guid UserIdd, string userName)
        {
            UserId = UserIdd;
            UserName = userName;
        }

        public LoginViewModel()
        {

        }

        public Guid id;
        public Guid UserId
        {
            get { return id; }
            set { id = value; }
        }

        public string username;
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }



        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set
            {
                _Phone = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Phone"));

            }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));

            }
        }

        public Command LoginDoubleCommandUser
        {
            get
            {
                return new Command(PerformLoginDoubleLogin);
            }
        }


        //log in
        private async void PerformLoginDoubleLogin()
        {
             if (string.IsNullOrEmpty(Phone) && string.IsNullOrEmpty(Password))
                await App.Current.MainPage.DisplayAlert("Hatalı giriş", "Lütfen bilgileri kontrol ediniz", "tamam");
            else
            {
                var userAdmin = await ApiServices.LoginAdminUser(Phone, Password);
                var user = await ApiServices.LoginUser(Phone, Password);
               
                if (userAdmin != null)
                    if (Phone == userAdmin.UserPhone && Password == userAdmin.UserPass)
                    {
                        await App.Current.MainPage.DisplayAlert("Hoşgeldiniz", "Giriş Başarılı", "tamam");
                        //Navigate to Wellcom page after successfuly login
                        //pass user email to welcom page
                        Phone = string.Empty;
                        Password = string.Empty;
                        await App.Current.MainPage.Navigation.PushAsync(new AdminDashBoard());
                        //user.UserId, user.UserName, user.UserPhone,user.UserGps
                       
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Giriş Hatalı", "Lütfen bilgileri kontrol ediniz", "tamam");
                else if (user != null)
                {
                    if (Phone == user.UserPhone && Password == user.UserPass)
                    {
                        await App.Current.MainPage.DisplayAlert("Hoşgeldiniz", "Giriş Başarılı", "tamam");
                        //Navigate to Wellcom page after successfuly login
                        //pass user email to welcom page
                        // await App.Current.MainPage.Navigation.PushAsync(new AdminDashBoard());
                        Phone = string.Empty;
                        Password = string.Empty;
                        await App.Current.MainPage.Navigation.PushAsync(new KullaniciGps(user.UserId, user.UserName));
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Giriş Hatalı", "Lütfen bilgileri kontrol ediniz", "tamam");
                }
                else                   
                    await App.Current.MainPage.DisplayAlert("Giriş Hatalı", "Böyle bir kullanıcı yok", "tamam");

            }

        }
    }
}
