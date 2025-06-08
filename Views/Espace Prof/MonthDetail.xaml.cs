using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using SDKTemplate.Views.Espace_Prof.Add;
using SDKTemplate.Views.Espace_Prof.Update;
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
    public sealed partial class MonthDetail : Page
    {
        GroupSessionViewModel viewModel = new GroupSessionViewModel();
        GroupStudentViewModel viewModelGroups = new GroupStudentViewModel();
        ObservableCollection<GroupSession> sessions { get; set; }
        bool t = true;
        public static Month myData { get; set; }
        public string ProfName { get; set; }
        public MonthDetail()
        {
            this.InitializeComponent();
            sessions = new ObservableCollection<GroupSession>();
            myData = new Month();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<Month>((string)e.Parameter);
        }
        private async void Page_LoadedAsync(object sender, RoutedEventArgs e)
        {
            Exist.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Collapsed;
            await viewModel.ExecuteGetGroupByProfnameCommand(GroupDetail.myData.ProfName, GroupDetail.myData.GroupName, myData.Slug);
            GroupSession AddSession = new GroupSession()
            {
                MonthName = "AddSession"
            };
            sessions.Add(AddSession);
            bool[] vis = new bool[4500];
            foreach (var account in viewModel.sessions)
            {
                if (!vis[(int) account.session_id])
                {
                    sessions.Add(account);
                    vis[(int)account.session_id] = true;
                }
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
                text.Text = "Ajouter une session";
                text.VerticalAlignment = VerticalAlignment.Center;
                text.FontSize = 20;
                FirstItem.Children.Add(text);
                FirstItem.Orientation = Orientation.Horizontal;
            }

        }
        private void FilteredListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var FirstItem = (GroupSession)e.ClickedItem;
            if (FirstItem.MonthName == "AddSession")
            {
                Add.Navigate(typeof(AddPeriod));
                Exist.Visibility = Visibility.Visible;
                Add.Visibility = Visibility.Visible;
            }
            else
            {
                string par = JsonConvert.SerializeObject(FirstItem);
                Frame.Navigate(typeof(StudentPresence), par);
            }
        }

        private async void Exist_Click(object sender, RoutedEventArgs e)
        {
            Exist.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Collapsed;
            await viewModel.ExecuteGetGroupByProfnameCommand(GroupDetail.myData.ProfName, GroupDetail.myData.GroupName, myData.Slug);
            sessions.Clear();
            GroupSession AddSession = new GroupSession()
            {
                MonthName = "AddSession"
            };
            sessions.Add(AddSession);
            bool[] vis = new bool[4500];
            foreach (var account in viewModel.sessions)
            {
                if (!vis[(int)account.session_id])
                {
                    sessions.Add(account);
                    vis[(int)account.session_id] = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdateMonth));
        }
    }
}
