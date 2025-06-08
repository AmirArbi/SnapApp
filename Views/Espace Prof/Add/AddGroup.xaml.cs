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

namespace SDKTemplate.Views.Espace_Prof
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AddGroup : Page
    {
        ProfGroupViewModel viewModel = new ProfGroupViewModel();
        ProfAccount myData = new ProfAccount();
        public AddGroup()
        {
            this.InitializeComponent();
        }

        private async void Continuer_ClickAsync(object sender, RoutedEventArgs e)
        {

            await viewModel.ExecuteAddItemCommand(myData.Username, GroupName.Text, Level.Text);
            string par = JsonConvert.SerializeObject(ProfDetail.myData);
            Frame.Navigate(typeof(ProfDetail), par);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
             myData = JsonConvert.DeserializeObject<ProfAccount>((string)e.Parameter);
        }
    }
}
