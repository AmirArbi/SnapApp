using Newtonsoft.Json;
using SDKTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace SDKTemplate.Views.Espace_Prof
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AddProf : Page
    {
        ProfAccountViewModel viewModel = new ProfAccountViewModel();
        public AddProf()
        {
            this.InitializeComponent();
        }

        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            string Password = "admin";
            string Username = Name.Text + Surname.Text;
            bool t = true;
            while (t)
            {
                await viewModel.ExecuteGetProfAccountCommand(Username);
                if (viewModel.ProfAccounts.Count == 0) t = false;
                else Username += "1";
            } 
            await viewModel.ExecuteAddItemCommand(Username, Name.Text, Surname.Text, Password, Formule.SelectedItem.ToString(), Subject.Text, Email.Text);
            bool ChangeGroup = false;
            string par = JsonConvert.SerializeObject(ChangeGroup);
            Frame.Navigate(typeof(ProfSearch), par);
        }
    }
}
