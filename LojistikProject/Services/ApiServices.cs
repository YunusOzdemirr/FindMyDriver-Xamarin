using Firebase.Database;
using Firebase.Database.Query;
using LiteDB;
using LojistikProject.Models;
using LojistikProject.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojistikProject.Services
{
    public static class ApiServices
    {
        //database
        public static FirebaseClient firebase = new FirebaseClient("https://mirportdatabaseandroid.firebaseio.com/");

        //üye getirme
        public static async Task<List<Kullanici>> GetRegisteredUsers()
        {
            var GetClients = (await firebase
               .Child("Kullanicilar")
               .OnceAsync<Kullanici>()).Select(item => new Kullanici
               {
                   UserId = item.Object.UserId,
                   UserName = item.Object.UserName,
                   UserPhone = item.Object.UserPhone,
                   UserPass = item.Object.UserPass,
                   UserSirketKod = item.Object.UserSirketKod,
                   UserLatitude = item.Object.UserLatitude,
                   UserAltitude = item.Object.UserAltitude,
                   AdminKod=item.Object.AdminKod,                   
               }).ToList();
            return GetClients;
        }
        //admin getirme

        public static async Task<List<Kullanici>> GetAdmin()
        {
            var GetClients = (await firebase
               .Child("Admin")
               .OnceAsync<Kullanici>()).Select(item => new Kullanici
               {
                   UserName = item.Object.UserName,
                   UserPhone = item.Object.UserPhone,
                   UserPass = item.Object.UserPass,
                   AdminKod = item.Object.AdminKod,
               }).ToList();
            return GetClients;
        }
        //admin koda ulaşma
        public static async Task<Kullanici> Admin(string AdminKodd)
        {

            //var GetClientss = (await firebase
            //   .Child("Admin")
            //   .OnceAsync<Kullanici>()).Select(item => new Kullanici
            //   {
            //       AdminKod = item.Object.AdminKod,
            //   }).ToList();
            //return GetClientss;
            var allPersons = await GetAdmin();
            await firebase
              .Child("Admin")
              .OnceAsync<Kullanici>();
            return allPersons.Where(a => a.AdminKod == AdminKodd).FirstOrDefault();

        }
        //AddPerson
        public static async Task<bool> AddPerson(string AdSoyad, string TelefonNo, string Sifre, string SirketKodu, double Latitude, double Altitude)
        {
            var resultt = await firebase
            .Child("Kullanicilar")
            .PostAsync(new Kullanici() { UserId = Guid.NewGuid(), UserName = AdSoyad, UserPhone = TelefonNo, UserPass = Sifre, UserSirketKod = SirketKodu, UserLatitude = Latitude, UserAltitude = Altitude });
            if (resultt.Object == null)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        //login user
        public static async Task<Kullanici> LoginUser(string phone, string password)
        {
            var GetPerson = (await firebase
              .Child("Kullanicilar")
              .OnceAsync<Kullanici>()).Where(a => a.Object.UserPhone == phone).Where(b => b.Object.UserPass == password).FirstOrDefault();

            if (GetPerson != null)
            {
                var content = GetPerson.Object as Kullanici;
                return content;
            }
            else
            {
                return null;
            }
        }
        //login admin user
        public static async Task<Kullanici> LoginAdminUser(string phone, string password)
        {
            var GetPerson = (await firebase
              .Child("Admin")
              .OnceAsync<Kullanici>()).Where(a => a.Object.UserPhone == phone).Where(b => b.Object.UserPass == password).FirstOrDefault();
            if (GetPerson != null)
            {
                var content = GetPerson.Object as Kullanici;
                return content;
            }
            else
            {
                return null;
            }
        }
        //update
        public static async Task UpdatePerson(Guid id, double Latitude, double Altitude)
        {
            try
            {
                //a.Object.UserId.toString() yapıp parametredeki id yi stringe dönüştürdüm olmadı hata vermedi ama bir işe de yaramadı
                var toUpdateUser = (await firebase
                .Child("Kullanicilar")
                .OnceAsync<Kullanici>()).Where(a => a.Object.UserId == id).FirstOrDefault();

                await firebase
               .Child("Kullanicilar")
               .Child(toUpdateUser.Key)
               .PatchAsync(new GPS() { UserLatitude = Latitude, UserAltitude = Altitude });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lütfen geliştirici ile iletişime geçin. {ex.Message}");

            }
        }
        //getPerson
        public static async Task<Kullanici> GetPerson(string phone, string adsoyad)
        {
            var allPersons = await GetRegisteredUsers();
            await firebase
              .Child("Kullanicilar")
              .OnceAsync<Kullanici>();
            return allPersons.Where(a => a.UserPhone == phone|| a.UserName == adsoyad).FirstOrDefault();

        }
       


    }
}
