using LojistikProject.Models;
using LojistikProject.Services;
using LojistikProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LojistikProject.Admin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminDashBoard : ContentPage
    {
        public AdminDashBoard()
        {
            InitializeComponent();
            GetListofClients();
            btn.ImageSource = ImageSource.FromResource("LojistikProject.Image.rightIcon.png");
            MyList.RefreshCommand = new Command(() => { RefreshData(); MyList.IsRefreshing = false; });
            BindingContext = ApiServices.GetRegisteredUsers();
        }
        public async void RefreshData()
        {
            var response = await ApiServices.GetRegisteredUsers();
            MyList.ItemsSource = null;
            MyList.ItemsSource = response;

        }


        //async void Button_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    var CellContents = ((Button)sender).BindingContext as Kullanici;

        //    #region eskiler
        //    //var CellContents = ((Button)sender).BindingContext as Kullanici;

        //    //Step1 : Add task title
        //    //var addtasktitle = await DisplayPromptAsync("Question", "What is the task name for  " + CellContents.UserName + " User ");

        //    //Step 2 : Add total no of tasks
        //    // var addtotaltasks = await DisplayPromptAsync("Question", "How many task assign to  " + CellContents.UserName, keyboard: Keyboard.Numeric);
        //    //int totaltasks = Convert.ToInt32(addtotaltasks);

        //    #endregion

        //}

        void MyList_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var content = e.SelectedItem as Kullanici;
            MyList.SelectedItem = content;
            Navigation.PushAsync(new AdminDashBoardDetails());

        }
        private async void GetListofClients()
        {
            var response = await ApiServices.GetRegisteredUsers();
            MyList.ItemsSource =response;
        }

        private void ImageClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdminDashBoardDetails());
        }

      
    }
}