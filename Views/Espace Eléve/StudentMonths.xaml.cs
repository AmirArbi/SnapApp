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

namespace SDKTemplate.Views.Espace_Eléve
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class StudentMonths : Page
    {
        MonthViewModel viewModel = new MonthViewModel();
        GroupStudentViewModel viewModelGroup = new GroupStudentViewModel();
        GroupSessionViewModel viewModelSession = new GroupSessionViewModel();
        ReceiptViewModel viewModelReceipt = new ReceiptViewModel();
        ChangeStudentViewModel viewModelChangement = new ChangeStudentViewModel();
        ObservableCollection<Month> PaymentMonths { get; set; }
        ObservableCollection<Month> PresenceMonths { get; set; }
        public static ProfAccount myData { get; set; }
        public static string groupName;
        public StudentMonths()
        {
            this.InitializeComponent();
            myData = new ProfAccount();
            PaymentMonths = new ObservableCollection<Month>();
            PresenceMonths = new ObservableCollection<Month>();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //design
            PresenceGrid.Visibility = Visibility.Collapsed;
            Receipt.Visibility = Visibility.Collapsed;
            StudentSessions.Visibility = Visibility.Collapsed;
            SessionReturn.Visibility = Visibility.Collapsed;
            //Load
            myData = JsonConvert.DeserializeObject<ProfAccount>((string)e.Parameter);
            Prof.Text = myData.Name + " " + myData.Surname;
            if (StudentProfs.myData.Gender == "Homme") StudentImage.Source = new BitmapImage(new Uri("ms-appx:///Images/Boy.png"));
            else if (StudentProfs.myData.Gender == "Femme") StudentImage.Source = new BitmapImage(new Uri("ms-appx:///Images/Girl.png"));
            Student.Text = StudentProfs.myData.Name + " " + StudentProfs.myData.Surname;
            School.Text = StudentProfs.myData.School;
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
                    Subjct.Margin = new Thickness(0, 0, 10, 0);
                }
            }
            Subj.Text = myData.Subject;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteGetGroupByProfnameCommand("CentreSnap", myData.SchoolYear);
            foreach (var month in viewModel.Months)
            {
                await viewModelSession.ExecuteGetStudentSessionsPerMonth(StudentProfs.myData.Username, "CentreSnap", "all", month.Slug);
                await viewModelReceipt.ExecuteGetStudentPaymentCommand("CentreSnap", StudentProfs.myData.Username, "all", myData.SchoolYear, "inscription");
                bool vis = true;
                float som = 0;
                foreach (var receipt in viewModelReceipt.receipts)
                {
                    som += receipt.PaidMoney;
                    if (receipt.Visibility == Visibility.Collapsed) vis = false;
                }
                if (vis)
                {
                    month.Visibilty = Visibility.Visible;
                    month.Collapsed = Visibility.Collapsed;

                    month.Montant = month.WantedMoney - som;
                    if (month.Montant == 0)
                    {
                        month.Visibilty = Visibility.Collapsed;
                        month.Collapsed = Visibility.Visible;
                    }
                    else if (month.Montant == month.WantedMoney)
                    {
                        SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
                        month.Color = redBrush;
                    }
                    else
                    {
                        SolidColorBrush orangeBrush = new SolidColorBrush(Colors.Orange);
                        month.Color = orangeBrush;
                    }
                }
                else
                {
                    month.Visibilty = Visibility.Collapsed;
                    month.Collapsed = Visibility.Visible;

                }
                PresenceMonths.Add(month);
                PaymentMonths.Add(month);
            }

            await viewModel.ExecuteGetGroupByProfnameCommand(myData.Username, myData.SchoolYear);
            foreach (var month in viewModel.Months)
            {
                await viewModelSession.ExecuteGetStudentSessionsPerMonth(StudentProfs.myData.Username, myData.Username, month.GroupName, month.Slug);
                if(viewModelSession.sessions.Count != 0)
                {
                    await viewModelReceipt.ExecuteGetStudentPaymentCommand(myData.Username, StudentProfs.myData.Username, month.GroupName, myData.SchoolYear, month.Slug);
                    bool vis = true;
                    float som = 0;
                    foreach (var receipt in viewModelReceipt.receipts)
                    {
                        som += receipt.PaidMoney;
                        if (receipt.Visibility == Visibility.Collapsed) vis = false;
                    }
                    if (vis)
                    {
                        month.Visibilty = Visibility.Visible;
                        month.Collapsed = Visibility.Collapsed;
                        int nbS = viewModelSession.sessions.Count;
                        int nbM = 0;
                        await viewModelSession.ExecuteGetGroupByProfnameCommand(myData.Username, month.GroupName, month.Slug);
                        bool[] visted = new bool[4500];
                        foreach (var session in viewModelSession.sessions)
                        {
                            if (!visted[(int)session.session_id])
                            {
                                nbM++;
                                visted[(int)session.session_id] = true;
                            }
                        }
                        float Pp = ((nbM - nbS) * month.WantedMoney) / month.NumberSessions;
                        await viewModelChangement.ExecuteGetChangementCommand(StudentProfs.myData.Username, myData.Username, month.GroupName, month.SchoolYear, month.Slug);
                        foreach (var changement in viewModelChangement.studentChangements)
                        {
                            Pp += (month.NumberSessions - changement.CurrentSession) * month.WantedMoney / month.NumberSessions;
                        }
                        month.Montant = month.WantedMoney - som - Pp;
                        if(month.Montant == 0)
                        {
                            month.Visibilty = Visibility.Collapsed;
                            month.Collapsed = Visibility.Visible;
                        } else if(month.Montant == month.WantedMoney)
                        {
                            SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
                            month.Color = redBrush;
                        } else
                        {
                            SolidColorBrush orangeBrush = new SolidColorBrush(Colors.Orange);
                            month.Color = orangeBrush;
                        }
                        month.WantedMoney -= Pp;
                    }
                    else
                    {
                        month.Visibilty = Visibility.Collapsed;
                        month.Collapsed = Visibility.Visible;

                    }
                    PresenceMonths.Add(month);
                    PaymentMonths.Add(month);
                }
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PresenceGrid.Visibility = Visibility.Collapsed;
            PaymentGrid.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PresenceGrid.Visibility = Visibility.Visible;
            PaymentGrid.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string par = JsonConvert.SerializeObject(StudentProfs.myData);
            Frame.Navigate(typeof(StudentProfs), par);
        }

        private void Name_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = SearchControlsAsync(MonthName.Text);
            }
        }
        private async Task<List<Month>> SearchControlsAsync(string MonthName)
        {
            var suggestions = new List<Month>();

            suggestions = viewModel.Months.Where(x => x.MonthName.Contains(MonthName)).ToList();

            PaymentMonths.Clear();
            foreach (var account in suggestions)
            {
                PaymentMonths.Add(account);
            }

            return suggestions;
        }

        private void HoraireListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var FirstItem = (Month)e.ClickedItem;
            if(FirstItem.MonthName != "inscription")
            {
                FirstItem.StudentName = StudentProfs.myData.Username;
                string par = JsonConvert.SerializeObject(FirstItem);
                StudentSessions.Navigate(typeof(SessionsPresence), par);
                StudentSessions.Visibility = Visibility.Visible;
                SessionReturnText.Text = FirstItem.MonthName;
                SessionReturn.Visibility = Visibility.Visible;
            }
        }

        private async void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var ClickedItem = (ToggleButton)sender;
            var t = ClickedItem.Name;
            if(t == "inscription") await viewModel.ExecuteMonthCommand("CentreSnap", ClickedItem.Tag.ToString(), t, myData.SchoolYear);
            else await viewModel.ExecuteMonthCommand(myData.Username, ClickedItem.Tag.ToString(), t, myData.SchoolYear);
            string par = JsonConvert.SerializeObject(viewModel.Months[0]);
            Add.Navigate(typeof(PaymentPage), par);
            Receipt.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Visible;
        }

        private void Exist_Click(object sender, RoutedEventArgs e)
        {
            Receipt.Visibility = Visibility.Collapsed;
        }

        private void SessionReturn_Click(object sender, RoutedEventArgs e)
        {
            StudentSessions.Visibility = Visibility.Collapsed;
            SessionReturn.Visibility = Visibility.Collapsed;
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            await viewModelGroup.ExecuteGetStudentsGroupCommand( myData.Username, StudentProfs.myData.Username, myData.SchoolYear);
            string par = JsonConvert.SerializeObject(viewModelGroup.GroupStudents[0]);
            StudentSessions.Navigate(typeof(NotePage), par);
            StudentSessions.Visibility = Visibility.Visible;
        }
    }
}
