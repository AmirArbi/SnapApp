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

namespace SDKTemplate.Views.Espace_SNAP
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AddEmployer : Page
    {
        EmployerViewModel viewModel = new EmployerViewModel();
        public AddEmployer()
        {
            this.InitializeComponent();
        }

        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            string Username = "";
            string FullName = Name.Text + Surname.Text;
            for (int i = 0; i < FullName.Length; i++)
            {
                if (FullName[i] != ' ' && FullName[i] != '/')
                {
                    Username += FullName[i];
                }
            }
            await viewModel.ExecuteAddItemCommand(Username, Name.Text, Surname.Text, Role.Text);
            Frame.Visibility = Visibility.Collapsed;
        }
    }
}
