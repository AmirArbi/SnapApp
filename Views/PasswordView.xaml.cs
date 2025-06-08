using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using SDKTemplate.Views.Espace_SNAP;
using SDKTemplate.Views.Reglage;
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

namespace SDKTemplate.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class PasswordView : Page
    {
        PasswordViewModel viewModel = new PasswordViewModel();
        ExpensesViewModel viewModelExpenses = new ExpensesViewModel();
        public static Expense myData { get; set; }
        public PasswordView()
        {
            this.InitializeComponent();
            myData = new Expense();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<Expense>((string)e.Parameter);
        }

        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            Password password = new Password
            {
                AdminPassword = Password.Password,
                AdminName = myData.AdminName
            };
            await viewModel.ExecuteCheckPasswordCommand(password);
            if (viewModel.res == "1"){
                if(myData.AdminName == "Administration")
                {
                    if (myData.Username == "App")
                    {
                        Frame.Navigate(typeof(MainPage));
                    }else
                    {
                        await viewModelExpenses.ExecuteAddItemCommand(myData.Username, myData.Amount, myData.Cause);
                        Frame.Visibility = Visibility.Collapsed;
                    }  
                }
                else if(myData.AdminName == "Directeur")
                {
                    if (myData.Username == "Settings")
                    {
                        Frame.Navigate(typeof(ReglageView));
                    }
                    else
                        Frame.Navigate(typeof(SNAPmenu));
                }
                    
            }
        }
    }
}
