using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using SDKTemplate.Views.Espace_Prof;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Views.Espace_Eléve
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class StudentSearch : Page
    {
        StudentsAccountViewModel viewModel = new StudentsAccountViewModel();
        ObservableCollection<StudentAccount> accounts = new ObservableCollection<StudentAccount>();
        bool t = true;
        public StudentSearch()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteLoadItemsCommand();
            StudentAccount AddProfAccount = new StudentAccount()
            {
                Username = "AddStudent"
            };
            accounts.Add(AddProfAccount);
            foreach (var account in viewModel.accounts)
            {
                if (account.Gender == "Homme") account.Image = new BitmapImage(new Uri("ms-appx:///Images/Boy.png"));
                else if (account.Gender == "Femme") account.Image = new BitmapImage(new Uri("ms-appx:///Images/Girl.png"));
                accounts.Add(account);
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
                text.Text = "Ajouter un nouveau élève";
                text.VerticalAlignment = VerticalAlignment.Center;
                text.FontSize = 20;
                FirstItem.Children.Add(text);
                FirstItem.Orientation = Orientation.Horizontal;
            }
        }

        private void FilteredListView_ItemClick(object sender, ItemClickEventArgs e)
        {
                var FirstItem = (StudentAccount)e.ClickedItem;
                if (FirstItem.Username == "AddStudent")
                {
                    Lastpage lastPage = new Lastpage()
                    {
                        LastPage = "StudentSearch",
                    };
                    string par = JsonConvert.SerializeObject(lastPage);
                    Frame.Navigate(typeof(AddNewStudent), par);
                }
                else
                {
                    FirstItem.Image = null;
                    string par = JsonConvert.SerializeObject(FirstItem);
                    Frame.Navigate(typeof(StudentProfs), par);
                }
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = SearchControlsAsync(Name.Text, Surname.Text, School.Text);
            }
        }
        private List<StudentAccount> SearchControlsAsync(string name,string surname, string school)
        {
            var suggestions = new List<StudentAccount>();

            suggestions = viewModel.accounts.Where(x => x.Name.Contains(name)).Where(x =>  x.Surname.Contains(surname)).Where(x => x.School.Contains(school)).ToList();

            accounts.Clear();
            StudentAccount AddProfAccount = new StudentAccount()
            {
                Username = "AddStudent"
            };
            accounts.Add(AddProfAccount);
            foreach (var account in suggestions)
            {
                accounts.Add(account);
            }

            return suggestions;
        }
    }
}
