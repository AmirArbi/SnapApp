using Newtonsoft.Json;
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
    public sealed partial class AddSession : Page
    {
        MonthViewModel viewModel = new MonthViewModel();
        public AddSession()
        {
            this.InitializeComponent();
        }

        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            string slug = "";
            for(int i = 0; i < MonthName.Text.Length; i++)
            {
                if(MonthName.Text[i] != ' ' && MonthName.Text[i] != '/')
                {
                    slug += MonthName.Text[i];
                }
            }
            await viewModel.ExecuteAddItemCommand(GroupDetail.myData.GroupName, slug, GroupDetail.myData.ProfName, MonthName.Text, long.Parse(NumberSession.Text), float.Parse(WantedMoney.Text));
            Frame.Visibility = Visibility.Collapsed;
        }
    }
}
