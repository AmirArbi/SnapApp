 using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.Services;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Views.Espace_Prof
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class GroupDetail : Page
    {
        ObservableCollection<GroupStudents> Students = new ObservableCollection<GroupStudents>();
        ObservableCollection<GroupStudents> ConstStudents = new ObservableCollection<GroupStudents>();
        ObservableCollection<Month> Months = new ObservableCollection<Month>();
        GroupStudentViewModel viewModel = new GroupStudentViewModel();
        GroupSessionViewModel viewModelSession = new GroupSessionViewModel();
        StudentsAccountViewModel viewModelStudent = new StudentsAccountViewModel();
        ReceiptViewModel viewModelReceipt = new ReceiptViewModel();
        MonthViewModel viewModelMonth= new MonthViewModel();
        bool tStudent = true;
        bool tTimeLine = true;
        public static ProfGroup myData { get; set; }
        public GroupDetail()
        {
            this.InitializeComponent();
            myData = new ProfGroup();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<ProfGroup>((string)e.Parameter);
            grpName.Text = myData.GroupName;
            nivaeu.Text = myData.Level; 
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Design
            MonthDetailReturn.Visibility = Visibility.Collapsed;
            Student.Visibility = Visibility.Collapsed;
            TimeLine.Visibility = Visibility.Collapsed;
            Default.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Collapsed;
            Exist.Visibility = Visibility.Collapsed;
            //Student Load
            await viewModel.ExecuteGetGroupCommand(ProfDetail.myData.Username, myData.GroupName, GroupDetail.myData.SchoolYear);
            Students.Clear();
            GroupStudents AddProfAccount = new GroupStudents()
            {
                StudentName = "AddStudent"
            };
            Students.Add(AddProfAccount);
            await viewModel.ExecuteDetailCommand(myData.ProfName, myData.GroupName, myData.SchoolYear);
            foreach(var account in viewModel.GroupStudents)
            {
                
                if (account.Gender == "Homme") account.Image = new BitmapImage(new Uri("ms-appx:///Images/Boy.png"));
                else if (account.Gender == "Femme") account.Image = new BitmapImage(new Uri("ms-appx:///Images/Girl.png"));
                if (account.IsStudying == false)
                {
                    if (account.Payment != "0")
                    {
                        account.Surname += " (batal)";
                        Students.Add(account);
                        ConstStudents.Add(account);
                    }
                }
                else
                {
                    Students.Add(account);
                    ConstStudents.Add(account);
                }  
            }
            //Month Load
            await viewModelMonth.ExecuteGetGroupByProfnameCommand(ProfDetail.myData.Username, myData.GroupName, ProfDetail.myData.SchoolYear);
            Months.Clear();
            Month AddMonth = new Month()
            {
                Slug = "AddMonth"
            };
            Months.Add(AddMonth);
            foreach (var month in viewModelMonth.Months)
            {
                bool payer = true;
                foreach (var student in viewModel.GroupStudents)
                {
                    await viewModelSession.ExecuteGetStudentSessionsPerMonth(student.StudentName, myData.ProfName, myData.GroupName, month.Slug);
                    if(viewModelSession.sessions.Count != 0)
                    {
                        await viewModelReceipt.ExecuteGetStudentPaymentCommand(myData.ProfName, student.StudentName,student.GroupName, myData.SchoolYear, month.Slug);
                        float som = 0 ;
                        foreach(var receipt in viewModelReceipt.receipts)
                        {
                            som += receipt.PaidMoney;
                        }
                        if (viewModelReceipt.receipts.Count == 0)
                        {
                            payer = false;
                            break;
                        }
                        else if (som < (month.WantedMoney - ((month.CurrentSessions - viewModelSession.sessions.Count) * month.WantedMoney / month.NumberSessions)))
                        {
                            payer = false;
                            break;
                        }
                    }
                    
                }
                if(payer) month.Payement = "Payé";
                else month.Payement = "Non Payé";
                Months.Add(month);
            }
        }

        private void panel_Loaded(object sender, RoutedEventArgs e)
        {
            if (tStudent)
            {
                tStudent = false;
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

        private void TimeLinePanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (tTimeLine)
            {
                tTimeLine = false;
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
                text.Text = "Ajouter une nouvelle session";
                text.VerticalAlignment = VerticalAlignment.Center;
                text.FontSize = 20;
                FirstItem.Children.Add(text);
                FirstItem.Orientation = Orientation.Horizontal;
            }
        }

        private async void FilteredListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var FirstItem = (GroupStudents)e.ClickedItem;
            if (FirstItem.StudentName== "AddStudent")
            {
                ListView.Visibility = Visibility.Visible;
            }
            else
            {
                ListView.Visibility = Visibility.Collapsed;
                StudentAccount student = await viewModelStudent.ExecuteGetStudentAccountCommand(FirstItem.StudentName);
                string par = JsonConvert.SerializeObject(student);
                Frame.Navigate(typeof(StudentDetail), par);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListView.SelectedIndex == 1)
            {
                Frame.Navigate(typeof(AddCurrentStudent));
            } else
            {
                Lastpage lastPage = new Lastpage()
                {
                    LastPage = "GroupDetail",
                };
                string par = JsonConvert.SerializeObject(lastPage);
                Frame.Navigate(typeof(AddNewStudent), par) ;
            }
        }

        private void Name_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = SearchControlsAsync(Name.Text, Surname.Text, School.Text);
            }
        }

        private List<GroupStudents> SearchControlsAsync(string name, string surname,string school)
        {
            var suggestions = new List<GroupStudents>();

            suggestions = ConstStudents.Where(x => x.Name.Contains(name)).Where(x => x.Surname.Contains(surname)).Where(x => x.School.Contains(school)).ToList();

            Students.Clear();
            GroupStudents AddProfAccount = new GroupStudents()
            {
                StudentName = "AddStudent"
            };
            Students.Add(AddProfAccount);
            foreach (var account in suggestions)
            {
                Students.Add(account);
            }

            return suggestions;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Student.Visibility = Visibility.Visible;
            TimeLine.Visibility = Visibility.Collapsed;
            Default.Visibility = Visibility.Collapsed;
            Session.Visibility = Visibility.Collapsed;
            MonthDetailReturn.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Collapsed;
            Exist.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Student.Visibility = Visibility.Collapsed;
            TimeLine.Visibility = Visibility.Visible;
            Default.Visibility = Visibility.Collapsed;
            Session.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Collapsed;
            MonthDetailReturn.Visibility = Visibility.Collapsed;
            Exist.Visibility = Visibility.Collapsed;
        }

        private void HoraireListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var FirstItem = (Month)e.ClickedItem;
            if (FirstItem.Slug == "AddMonth")
            {
                Add.Navigate(typeof(AddSession));
                Add.Visibility = Visibility.Visible;
                Exist.Visibility = Visibility.Visible;
            } else
            { 
                ListView.Visibility = Visibility.Collapsed;
                string par = JsonConvert.SerializeObject(FirstItem);
                Session.Navigate(typeof(MonthDetail), par);
                Session.Visibility = Visibility.Visible;
                ReturnText.Text = FirstItem.MonthName;
                MonthDetailReturn.Visibility = Visibility.Visible;
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MonthDetailReturn.Visibility = Visibility.Collapsed;
            Session.Visibility = Visibility.Collapsed;
        }

        private async void Exist_Click(object sender, RoutedEventArgs e)
        {
            Exist.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Collapsed;
            //Month Load
            await viewModelMonth.ExecuteGetGroupByProfnameCommand(ProfDetail.myData.Username, myData.GroupName, ProfDetail.myData.SchoolYear);
            Months.Clear();
            Month AddMonth = new Month()
            {
                Slug = "AddMonth"
            };
            Months.Add(AddMonth);
            foreach (var month in viewModelMonth.Months)
            {
                bool payer = true;
                foreach (var student in viewModel.GroupStudents)
                {
                    await viewModelSession.ExecuteGetStudentSessionsPerMonth(student.StudentName, myData.ProfName, myData.GroupName, month.Slug);
                    if (viewModelSession.sessions.Count != 0)
                    {
                        await viewModelReceipt.ExecuteGetStudentPaymentCommand(myData.ProfName, student.StudentName, student.GroupName, myData.SchoolYear, month.Slug);
                        float som = 0;
                        foreach (var receipt in viewModelReceipt.receipts)
                        {
                            som += receipt.PaidMoney;
                        }
                        if (viewModelReceipt.receipts.Count == 0)
                        {
                            payer = false;
                            break;
                        }
                        else if (som < (month.WantedMoney - ((month.CurrentSessions - viewModelSession.sessions.Count) * month.WantedMoney / month.NumberSessions)))
                        {
                            payer = false;
                            break;
                        }
                    }

                }
                if (payer) month.Payement = "Payé";
                else month.Payement = "Non Payé";
                Months.Add(month);
            }
        }

        private void Session_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string par = JsonConvert.SerializeObject(ProfDetail.myData);
            Frame.Navigate(typeof(ProfDetail), par);
        }
    }
}
