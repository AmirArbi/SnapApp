using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using SDKTemplate.Views.Espace_Prof.Update;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ProfDetail : Page
    {

        ProfGroupViewModel viewModel = new ProfGroupViewModel();
        GroupStudentViewModel viewModelGroups = new GroupStudentViewModel();
        ObservableCollection<ProfGroup> groups { get; set; }
        bool t = true;
        public static ProfAccount myData { get; set; }
        public string ProfName { get; set; }
        public ProfDetail()
        {
            this.InitializeComponent();
            groups = new ObservableCollection<ProfGroup>();
            myData = new ProfAccount();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<ProfAccount>((string)e.Parameter);
            ProfName = myData.Username;
            Prof.Text = myData.Name + " " + myData.Surname;
            char[] voyelle = { 'a', 'e', 'y', 'u', 'i', 'o', 'A', 'E', 'Y', 'U', 'I', 'O' };
            for (int i = 0; i < 12; i++)
            {
                if (myData.Subject[0] == voyelle[i])
                {
                    Subjct.Text = "Prof d'";
                    Subjct.Margin = new Thickness(0);
                    break;
                }
                else
                {
                    Subjct.Text = "Prof de ";
                    Subjct.Margin = new Thickness(0,0,10,0);
                }
            }
            Subj.Text = myData.Subject;
        }
        private async void Page_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteGetGroupByProfnameCommand(ProfName, myData.SchoolYear);
            ProfGroup AddProfAccount = new ProfGroup()
            {
                ProfName = "AddProf"
            };
            groups.Add(AddProfAccount);
            //studentsNumber = 0;
            foreach (var account in viewModel.ProfGroups)
            {
                groups.Add(account);
            }
        }

        private void panel_Loaded(object sender, RoutedEventArgs e)
        {
            if (t)
            {
                t = false;
                StackPanel FirstItem = (StackPanel)sender;
                FirstItem.Children.Clear();
                Viewbox viewbox = new Viewbox();
                viewbox.VerticalAlignment = VerticalAlignment.Center;
                viewbox.Margin = new Thickness(30);
                viewbox.MaxHeight = 50;
                viewbox.MaxWidth = 50;
                SymbolIcon icon = new SymbolIcon();
                icon.Symbol = Symbol.Add;
                viewbox.Child = icon;
                FirstItem.Children.Add(viewbox);
                TextBlock text = new TextBlock();
                text.Text = "Ajouter un nouveau groupe";
                text.VerticalAlignment = VerticalAlignment.Center;
                text.FontSize = 20;
                FirstItem.Children.Add(text);
                FirstItem.Orientation = Orientation.Horizontal;
            } 

        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = SearchControlsAsync(Level.Text, GroupName.Text);
            }
        }

        private async Task<List<ProfGroup>> SearchControlsAsync(string query, string query2)
        {
            var suggestions = new List<ProfGroup>();

            suggestions = viewModel.ProfGroups.Where(x => x.GroupName.Contains(query2)).Where(x => x.Level.Contains(query)).ToList();

            groups.Clear();
            ProfGroup AddProfAccount = new ProfGroup()
            {
                ProfName = "AddProf"
            };
            groups.Add(AddProfAccount);
            //studentsNumber = 0;
            foreach (var account in suggestions)
            {
                groups.Add(account);
            }

            return suggestions;
        }

        private void FilteredListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var FirstItem = (ProfGroup)e.ClickedItem;
            if (FirstItem.ProfName == "AddProf")
            {
                string par = JsonConvert.SerializeObject(myData);
                Frame.Navigate(typeof(AddGroup),par); 
            }
            else
            {
                string par = JsonConvert.SerializeObject(FirstItem);
                Frame.Navigate(typeof(GroupDetail), par);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool ChangeGroup = false;
            string par = JsonConvert.SerializeObject(ChangeGroup);
            Frame.Navigate(typeof(ProfSearch), par);
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Archive));
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdateProf));
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProfInfo));
        }
    }
}