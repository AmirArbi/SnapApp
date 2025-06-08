using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using SDKTemplate.Views.Espace_Prof.Update;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Views.Espace_Prof.Add
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class StudentPresence : Page
    {
        ObservableCollection<GroupSession> StudentPresences = new ObservableCollection<GroupSession>();
        StudentsAccountViewModel viewModelStudent = new StudentsAccountViewModel();
        GroupStudentViewModel viewModel = new GroupStudentViewModel();
        GroupSessionViewModel viewModelSession = new GroupSessionViewModel();
        PresenceViewModel viewModelPresence = new PresenceViewModel();
        public static GroupSession myData { get; set; }
        public StudentPresence()
        {
            this.InitializeComponent();
            myData = new GroupSession();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<GroupSession>((string)e.Parameter);

        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModelSession.ExecuteLoadPresence(myData.ProfName, myData.GroupName, myData.SchoolYear, myData.MonthName, myData.session_id);
            foreach (var session in viewModelSession.sessions)
            {
                if (session.Gender == "Homme") session.Image = new BitmapImage(new Uri("ms-appx:///Images/Boy.png"));
                else if (session.Gender == "Femme") session.Image = new BitmapImage(new Uri("ms-appx:///Images/Girl.png"));
                StudentPresences.Add(session);
            }
        }

        private async void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var ClickedItem = (ToggleButton)sender;
            var t = ClickedItem.Name;
            await viewModelSession.ExecuteGetSession(myData.ProfName, myData.GroupName, t, myData.SchoolYear, myData.MonthName, myData.session_id);
            if (ClickedItem.IsChecked.Value)
            {
                viewModelSession.sessions[0].IsPresent = true;
            }
            else if (!ClickedItem.IsChecked.Value)
            {
                viewModelSession.sessions[0].IsPresent = false;
            }
            await viewModelSession.ExecuteUpdateSessionCommand(viewModelSession.sessions[0]);
        }

        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            await viewModelSession.ExecuteUpdateAllCommand(myData.ProfName, myData.GroupName, myData.SchoolYear, myData.MonthName, myData.session_id);
            StudentPresences.Clear();
            await viewModelSession.ExecuteLoadPresence(myData.ProfName, myData.GroupName, myData.SchoolYear, myData.MonthName, myData.session_id);
            foreach (var session in viewModelSession.sessions)
            {
                if (session.Gender == "Homme") session.Image = new BitmapImage(new Uri("ms-appx:///Images/Boy.png"));
                else if (session.Gender == "Femme") session.Image = new BitmapImage(new Uri("ms-appx:///Images/Girl.png"));
                StudentPresences.Add(session);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string par = JsonConvert.SerializeObject(MonthDetail.myData);
            Frame.Navigate(typeof(MonthDetail), par);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdateSeance));
        }
    }
}
