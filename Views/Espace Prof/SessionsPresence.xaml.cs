using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Views.Espace_Prof
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class SessionsPresence : Page
    {
        ObservableCollection<GroupSession> sessions;
        GroupSessionViewModel viewModel= new GroupSessionViewModel();
        GroupStudentViewModel viewModelStudent = new GroupStudentViewModel();

        public static Month myData { get; set; }
        public string ProfName { get; set; }
        public SessionsPresence()
        {
            this.InitializeComponent();
            sessions = new ObservableCollection<GroupSession>();
            myData = new Month();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<Month>((string)e.Parameter);
        }
        private async void Page_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteGetStudentSessionsPerMonth(myData.StudentName ,myData.ProfName, myData.GroupName, myData.Slug);
            sessions.Clear();
            foreach (var session in viewModel.sessions)
            {
                if (session.IsPresent)
                {
                    session.Presence = "Present";
                    session.IsAbsent = false;
                } else
                {
                    session.Presence = "Absent";
                    session.IsAbsent = true;
                }
                if (session.IsCounting)
                    session.IsDeleted = false;
                else
                {
                    session.IsDeleted = true;
                    session.IsAbsent = false;
                }
                sessions.Add(session);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var ClickedItem = (Button)sender;
            var t = ClickedItem.Name;
            await viewModel.ExecuteGetSession(myData.ProfName, myData.GroupName, myData.StudentName, myData.SchoolYear, myData.MonthName, long.Parse(t));
            await viewModelStudent.ExecuteDeleteSessionCommand(viewModel.sessions[0]);
        }
    } 
}
