using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using SDKTemplate.Views.Espace_Prof.Info;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Views.Espace_Eléve
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class StudentProfs : Page
    {
        GroupStudentViewModel viewModel = new GroupStudentViewModel();
        ProfAccountViewModel viewModelProf = new ProfAccountViewModel();
        ObservableCollection<ProfAccount> profs { get; set; }
        ObservableCollection<ProfAccount> ConstProfs { get; set; }
        bool t = true;
        public static StudentAccount myData { get; set; }
        public StudentProfs()
        {
            this.InitializeComponent();
            profs = new ObservableCollection<ProfAccount>();
            ConstProfs = new ObservableCollection<ProfAccount>();
            myData = new StudentAccount();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<StudentAccount>((string)e.Parameter);
            if (myData.Gender == "Homme") StudentImage.Source = new BitmapImage(new Uri("ms-appx:///Images/Boy.png"));
            else if (myData.Gender == "Femme") StudentImage.Source = new BitmapImage(new Uri("ms-appx:///Images/Girl.png"));
            Student.Text = myData.Name + " " + myData.Surname;
            School.Text = myData.School;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteGetStudentsGroupCommand(myData.Username, "2020-2021");
            foreach (var group in viewModel.GroupStudents)
            {
                await viewModelProf.ExecuteGetProfAccountCommand(group.ProfName);
                foreach (var account in viewModelProf.ProfAccounts)
                {
                    profs.Add(account);
                    ConstProfs.Add(account);
                }
            }
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            myData.Image = null;
            myData.Page = "StudentProfs";
            string par = JsonConvert.SerializeObject(myData);
            Frame.Navigate(typeof(UpdateStudent), par);
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {
            myData.Image = null;
            myData.Page = "StudentProfs";
            string par = JsonConvert.SerializeObject(myData);
            Frame.Navigate(typeof(StudentInfo), par);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StudentSearch));
        }

        private void FilteredListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var FirstItem = (ProfAccount)e.ClickedItem;
            FirstItem.SchoolYear = "2020-2021";
            string par = JsonConvert.SerializeObject(FirstItem);
            Frame.Navigate(typeof(StudentMonths), par);
        }

        private void Level_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = SearchControlsAsync(ProfName.Text, Subject.Text);
            }
        }
        private async Task<List<ProfAccount>> SearchControlsAsync(string ProfName, string Subject)
        {
            var suggestions = new List<ProfAccount>();

            suggestions = ConstProfs.Where(x => x.Username.Contains(ProfName)).Where(x => x.Subject.Contains(Subject)).ToList();

            profs.Clear();
            foreach (var account in suggestions)
            {
                profs.Add(account);
            }

            return suggestions;
        }
    }
}
