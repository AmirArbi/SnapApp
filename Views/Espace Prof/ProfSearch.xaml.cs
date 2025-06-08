using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.Services;
using SDKTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Views.Espace_Prof
{
    
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ProfSearch : Page
    {
        ProfAccountViewModel viewModel = new ProfAccountViewModel();
        ObservableCollection<ProfAccount> accounts = new ObservableCollection<ProfAccount>();
        ObservableCollection<ProfAccount> AddAccount = new ObservableCollection<ProfAccount>();
        bool t = true;
        public static bool myData { get; set; }

        public ProfSearch()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<bool>((string)e.Parameter);
        }
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = SearchControlsAsync(sender.Text);
                ObservableCollection<String> result = new ObservableCollection<string>();
                if (suggestions.Count > 0)
                {
                    for (int i = 0; i < suggestions.Count; i++)
                    {
                        result.Add(suggestions[i].Username);
                    }
                    sender.ItemsSource = result;
                } else
                    sender.ItemsSource = new string[] { "No results found" };
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
        }

        private  List<ProfAccount> SearchControlsAsync(string query)
        {
            var suggestions = new List<ProfAccount>();
             
            suggestions = viewModel.ProfAccounts.Where(x => x.Username.Contains(query)).ToList();

            accounts.Clear();
            ProfAccount AddProfAccount = new ProfAccount()
            {
                Username = "AddProf"
            };
            accounts.Add(AddProfAccount);
            foreach (var account in suggestions)
            {
                accounts.Add(account);
            }

            return suggestions;
        }


        private async void Page_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteLoadItemsCommand();
            ProfAccount AddProfAccount = new ProfAccount()
            {
                Username = "AddProf"
            };
            accounts.Add(AddProfAccount);
            foreach (var account in viewModel.ProfAccounts)
            {
                accounts.Add(account);
            }
            
        }

        

        private void FilteredListView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void panel_Loaded(object sender, RoutedEventArgs e)
        {

            if (t)
            {
                t = false;
                StackPanel FirstItem = (StackPanel) sender;
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
                text.Text = "Ajouter un nouveau Prof";
                text.VerticalAlignment = VerticalAlignment.Center;
                text.FontSize = 20;
                FirstItem.Children.Add(text);
                FirstItem.Orientation = Orientation.Horizontal;
            }
        }

        private void FilteredListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var FirstItem = (ProfAccount)e.ClickedItem;
            if (FirstItem.Username == "AddProf")
            {
                Frame.Navigate(typeof(AddProf));
            }
            else
            {
                if (!myData)
                {
                    FirstItem.SchoolYear = "2020-2021";
                    string par = JsonConvert.SerializeObject(FirstItem);
                    Frame.Navigate(typeof(ProfDetail), par);
                } else
                {
                    string par = JsonConvert.SerializeObject(FirstItem);
                    Frame.Navigate(typeof(ChangeGroup), par);
                }
            }
        }
    }
}
