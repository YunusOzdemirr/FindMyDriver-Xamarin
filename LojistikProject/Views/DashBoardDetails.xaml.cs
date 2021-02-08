using LojistikProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LojistikProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashBoardDetails : ContentPage
    {
        public DashBoardDetails(Kullanici tasksModel)
        {
            InitializeComponent();

            //TaskId.Text = tasksModel.TaskId.ToString();
            //TaskTitle.Text = tasksModel.TaskTitle.ToString();
            //ClientID.Text = tasksModel.ClientID.ToString();
            //MyList.ItemsSource = tasksModel.clientTasks;
        }
    }
}