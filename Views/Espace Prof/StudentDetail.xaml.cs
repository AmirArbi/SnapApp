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
using Windows.UI;
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
    public sealed partial class StudentDetail : Page
    {
        MonthViewModel viewModel = new MonthViewModel();
        GroupStudentViewModel viewModelGroups = new GroupStudentViewModel();
        ReceiptViewModel viewModelReceipts = new ReceiptViewModel();
        GroupSessionViewModel viewModelSession = new GroupSessionViewModel();
        ChangeStudentViewModel viewModelChangement = new ChangeStudentViewModel();
        ProfGroupViewModel viewModelProfGroup = new ProfGroupViewModel();
        ObservableCollection<Month> periods { get; set; }
        bool t = true;
        public static StudentAccount myData { get; set; }
        public string ProfName { get; set; }
        public StudentDetail()
        {
            this.InitializeComponent();
            periods = new ObservableCollection<Month>();
            myData = new StudentAccount();

        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            SessionReturn.Visibility = Visibility.Collapsed;
            StudentSessions.Visibility = Visibility.Collapsed;
            myData = JsonConvert.DeserializeObject<StudentAccount>((string)e.Parameter);
            await viewModelGroups.ExecuteGetStudentsGroupCommand(GroupDetail.myData.ProfName, myData.Username, GroupDetail.myData.SchoolYear);
            GroupStudents student = viewModelGroups.GroupStudents[0];
            if (student.IsStudying)
                Rja.IsEnabled = false;
            else
                Batal.IsEnabled = false;
            ProfName = myData.Username;
            if (myData.Gender == "Homme") StudentImage.Source = new BitmapImage(new Uri("ms-appx:///Images/Boy.png"));
            else if (myData.Gender == "Femme") StudentImage.Source = new BitmapImage(new Uri("ms-appx:///Images/Girl.png"));
            Student.Text = myData.Name + " " + myData.Surname;
            School.Text = myData.School;
        }
        private async void Page_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteDetailCommand(ProfDetail.myData.Username, myData.Username, GroupDetail.myData.GroupName, GroupDetail.myData.SchoolYear);
            foreach (var month in viewModel.Months)
            {
                if (month.Payement == month.Montant.ToString())
                {
                    month.Payement = "Non";
                    SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
                    month.Color = redBrush;
                }
                else if (month.Payement == "0")
                {
                    month.Payement = "Oui";
                    SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
                    month.Color = greenBrush;
                }
                else
                {
                    month.Payement = month.Montant.ToString() + " DT";
                    SolidColorBrush orangeBrush = new SolidColorBrush(Colors.Orange);
                    month.Color = orangeBrush;
                }
                periods.Add(month);
            }
            /*await viewModel.ExecuteGetGroupByProfnameCommand("CentreSnap", "all", GroupDetail.myData.SchoolYear);
            inscri = viewModel.Months[0];
            float InscriMontant = 30;
            inscri.Montant = InscriMontant;
            string InscriRep = await viewModelGroups.ExecuteGetResteCommand(inscri.ProfName, myData.Username, inscri.Slug, inscri.GroupName, inscri.SchoolYear, 30);
            if (InscriRep == "0" || float.Parse(InscriRep) == 30)
            {
                inscri.Payement = "Non";
                SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
                inscri.Color = redBrush;
            }
            else if (InscriRep == "1")
            {
                inscri.Payement = "Oui";
                SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
                inscri.Color = greenBrush;
            }
            else
            {
                inscri.Payement = InscriRep + " DT";
                SolidColorBrush orangeBrush = new SolidColorBrush(Colors.Orange);
                inscri.Color = orangeBrush;
            }
            if (InscriMontant != 0) periods.Add(inscri);
            await viewModel.ExecuteGetGroupByProfnameCommand(ProfDetail.myData.Username, ProfDetail.myData.SchoolYear);
            foreach (var account in viewModel.Months)
            {
                float montant = await viewModelGroups.ExecuteGetAmountCommand(account.ProfName, myData.Username, account.Slug, account.GroupName, account.SchoolYear);
                account.Montant = montant;
                string rep = await viewModelGroups.ExecuteGetResteCommand(account.ProfName, myData.Username, account.Slug, account.GroupName, account.SchoolYear, montant);
                if (rep == "0" || float.Parse(rep) == account.WantedMoney)
                {
                    account.Payement = "Non";
                    SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
                    account.Color = redBrush;
                }
                else if (rep == "1")
                {
                    account.Payement = "Oui";
                    SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
                    account.Color = greenBrush;
                } else
                {
                    account.Payement = rep + " DT";
                    SolidColorBrush orangeBrush = new SolidColorBrush(Colors.Orange);
                    account.Color = orangeBrush;
                }
                if(montant!=0) periods.Add(account);
            }*/
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = SearchControlsAsync(Name.Text, Payement.SelectedIndex);
            }
        }

        private async Task<List<Month>> SearchControlsAsync(string query, int query2)
        {
            var suggestions = new List<Month>();
            periods.Clear();
            if (query2 == 1)
            {
                suggestions = viewModel.Months.Where(x => x.MonthName.Contains(query)).Where(x => x.Payement.Contains("Oui")).ToList();
            }
            else if (query2 == 2)
            {
                suggestions = viewModel.Months.Where(x => x.MonthName.Contains(query)).Where(x => x.Payement.Contains("Non")).ToList();
            }
            else if (query2 == 3)
            {
                suggestions = viewModel.Months.Where(x => x.MonthName.Contains(query)).Where(x => x.Payement.Contains("DT")).ToList();
            }
            else
            {
                suggestions = viewModel.Months.Where(x => x.MonthName.Contains(query)).ToList();
            }
           
            
            
            foreach (var account in suggestions)
            {
                    //Months
                    if (account.Montant != 0) periods.Add(account);  
            }

            return suggestions;
        }

        private void panel_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var suggestions = SearchControlsAsync(Name.Text, Payement.SelectedIndex);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string par = JsonConvert.SerializeObject(GroupDetail.myData);
            Frame.Navigate(typeof(GroupDetail), par);
        }

        private void FilteredListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var FirstItem = (Month)e.ClickedItem;
            FirstItem.StudentName = myData.Username;
            FirstItem.Color = null;
            string par = JsonConvert.SerializeObject(FirstItem);
            StudentSessions.Navigate(typeof(SessionsPresence), par);
            StudentSessions.Visibility = Visibility.Visible;
            SessionReturn.Visibility = Visibility.Visible;
            SessionReturnText.Text = FirstItem.MonthName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StudentSessions.Visibility = Visibility.Collapsed;
            SessionReturn.Visibility = Visibility.Collapsed;
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            string par = JsonConvert.SerializeObject(ProfDetail.myData);
            Frame.Navigate(typeof(ChangeGroup), par);
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            myData.Image = null;
            myData.Page = "StudentDetail";
            string par = JsonConvert.SerializeObject(myData);
            Frame.Navigate(typeof(UpdateStudent), par);
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {
            myData.Image = null;
            myData.Page = "StudentDetail";
            string par = JsonConvert.SerializeObject(myData);
            Frame.Navigate(typeof(StudentInfo), par);
        }

        private void MenuFlyoutItem_Click_3(object sender, RoutedEventArgs e)
        {
            bool ChangeProf = true;
            string par = JsonConvert.SerializeObject(ChangeProf);
            Frame.Navigate(typeof(ProfSearch), par);
        }

        private async void MenuFlyoutItem_Click_4(object sender, RoutedEventArgs e)
        {
            await viewModelGroups.ExecuteGetStudentsGroupCommand(GroupDetail.myData.ProfName, myData.Username, GroupDetail.myData.SchoolYear);
            GroupStudents student = viewModelGroups.GroupStudents[0];
            student.IsStudying = false;
            await viewModelGroups.ExecuteUpdateItemCommand(student, GroupDetail.myData.ProfName, student.SchoolYear, GroupDetail.myData.GroupName, student.student_id);
            //Modify the number of student in the old group
            await viewModelProfGroup.ExecuteUpdateMinus(GroupDetail.myData);
            Rja.IsEnabled = true;
            Batal.IsEnabled = false;
        }

        private async void MenuFlyoutItem_Click_5(object sender, RoutedEventArgs e)
        {
            await viewModelGroups.ExecuteGetStudentsGroupCommand(GroupDetail.myData.ProfName, myData.Username, GroupDetail.myData.SchoolYear);
            GroupStudents student = viewModelGroups.GroupStudents[0];
            student.IsStudying = true;
            await viewModelGroups.ExecuteUpdateItemCommand(student, GroupDetail.myData.ProfName, student.SchoolYear, GroupDetail.myData.GroupName, student.student_id);
            //Modify the number of student in the old group
            await viewModelProfGroup.ExecuteUpdateNumberOfStudents(GroupDetail.myData);
            Rja.IsEnabled = false;
            Batal.IsEnabled = true;
        }

        private async void MenuFlyoutItem_Click_6(object sender, RoutedEventArgs e)
        {
            await viewModelGroups.ExecuteGetStudentsGroupCommand(GroupDetail.myData.ProfName, myData.Username, GroupDetail.myData.SchoolYear);
            string par = JsonConvert.SerializeObject(viewModelGroups.GroupStudents[0]);
            StudentSessions.Navigate(typeof(NotePage), par);
            StudentSessions.Visibility = Visibility.Visible;
        }

        private void Reduction_Click(object sender, RoutedEventArgs e)
        {
            StudentSessions.Navigate(typeof(ReductionPage));
            StudentSessions.Visibility = Visibility.Visible;
        }
    }
}
