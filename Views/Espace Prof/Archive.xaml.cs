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
    public sealed partial class Archive : Page
    {
        ProfGroupViewModel viewModel = new ProfGroupViewModel();
        SchoolYearViewModel viewModelYear = new SchoolYearViewModel();
        ObservableCollection<Schoolyear> SchoolYears { get; set; }
        public Archive()
        {
            this.InitializeComponent();
            SchoolYears = new ObservableCollection<Schoolyear>();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Prof.Text = ProfDetail.myData.Name + " " + ProfDetail.myData.Surname;
            char[] voyelle = { 'a', 'e', 'y', 'u', 'i', 'o', 'A', 'E', 'Y', 'U', 'I', 'O' };
            for (int i = 0; i < 12; i++)
            {
                if (ProfDetail.myData.Subject[0] == voyelle[i])
                {
                    Subjct.Text = "Prof d'";
                    Subjct.Margin = new Thickness(0);
                    break;
                }
                else
                {
                    Subjct.Text = "Prof de ";
                    Subjct.Margin = new Thickness(0, 0, 10, 0);
                }
            }
            Subj.Text = ProfDetail.myData.Subject;
        }
        private async void Page_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await viewModelYear.ExecuteLoadItemsCommand();
            foreach (var account in viewModelYear.years)
            {
                SchoolYears.Add(account);
            }
        }

        private void FilteredListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var FirstItem = (Schoolyear)e.ClickedItem;
            ProfDetail.myData.SchoolYear = FirstItem.SchoolYear;
            string par = JsonConvert.SerializeObject(ProfDetail.myData);
            Frame.Navigate(typeof(ProfDetail), par);
        }
    }

}
