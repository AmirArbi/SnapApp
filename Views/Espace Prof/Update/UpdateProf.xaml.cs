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
    public sealed partial class UpdateProf : Page
    {
        ProfAccountViewModel viewModel = new ProfAccountViewModel();

        public UpdateProf()
        {
            this.InitializeComponent();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Name.Text = ProfDetail.myData.Name;
            Surname.Text = ProfDetail.myData.Surname;
            Email.Text = ProfDetail.myData.Email;
            Subject.Text = ProfDetail.myData.Subject;
            if (ProfDetail.myData.Formule == "Scientifique")
                Formule.SelectedIndex = 0;
            else
                Formule.SelectedIndex = 1;
        }

        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteUpdateItemCommand(ProfDetail.myData.Username, Name.Text, Surname.Text, ProfDetail.myData.Password, Formule.SelectedItem.ToString(), Subject.Text, Email.Text);
            await viewModel.ExecuteGetProfAccountCommand(ProfDetail.myData.Username);
            ProfAccount CurrentProfAccount = viewModel.ProfAccounts[0];
            CurrentProfAccount.SchoolYear = "2020-2021";
            string par = JsonConvert.SerializeObject(CurrentProfAccount);
            Frame.Navigate(typeof(ProfDetail), par);
        }
    }
}
