using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
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
    public sealed partial class UpdateMonth : Page
    {
        MonthViewModel viewModel = new MonthViewModel();
        public UpdateMonth()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NumberSession.Text = MonthDetail.myData.NumberSessions.ToString();
            WantedMoney.Text = MonthDetail.myData.WantedMoney.ToString();
        }
        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            Month month = MonthDetail.myData;
            month.NumberSessions = long.Parse(NumberSession.Text);
            month.WantedMoney = float.Parse(WantedMoney.Text);
            await viewModel.ExecuteUpdateMonthCommand(month);
            string par = JsonConvert.SerializeObject(month);
            Frame.Navigate(typeof(MonthDetail), par);
        }
    }
}
