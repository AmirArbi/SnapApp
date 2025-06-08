using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using SDKTemplate.Views.Espace_Prof.Add;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Views.Espace_Prof.Update
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class UpdateSeance : Page
    {
        GroupSessionViewModel viewModel = new GroupSessionViewModel();
        public UpdateSeance()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SessionId.Text = StudentPresence.myData.session_id.ToString();
            Date.Date = StudentPresence.myData.Date;
            Time.Time = StudentPresence.myData.Date.TimeOfDay;
        }

        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            DateTime HollDate = new DateTime(Date.Date.Year, Date.Date.Month, Date.Date.Day, Time.Time.Hours, Time.Time.Minutes, Time.Time.Seconds);
            GroupSession session = StudentPresence.myData;
            session.Date = HollDate;
            await viewModel.ExecuteUpdateSessionCommand(session);
            string par = JsonConvert.SerializeObject(session);
            Frame.Navigate(typeof(StudentPresence), par);
        }
    }
}
