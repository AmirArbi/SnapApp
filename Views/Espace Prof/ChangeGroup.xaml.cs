using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
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
    public sealed partial class ChangeGroup : Page
    {

        ProfGroupViewModel viewModel = new ProfGroupViewModel();
        GroupStudentViewModel viewModelStudent = new GroupStudentViewModel();
        ChangeStudentViewModel viewModelChangement = new ChangeStudentViewModel();
        MonthViewModel viewModelMonth= new MonthViewModel();
        ObservableCollection<ProfGroup> groups { get; set; }
        public static ProfAccount myData { get; set; }
        public ChangeGroup()
        {
            this.InitializeComponent();
            groups = new ObservableCollection<ProfGroup>();
            myData = new ProfAccount();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<ProfAccount>((string)e.Parameter);
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
                    Subjct.Margin = new Thickness(0, 0, 10, 0);
                }
            }
            Subj.Text = myData.Subject;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteGetGroupByProfnameCommand(myData.Username, ProfDetail.myData.SchoolYear);
            foreach (var account in viewModel.ProfGroups)
            {
                groups.Add(account);
            }
        }

        private async void FilteredListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var FirstItem = (ProfGroup)e.ClickedItem;
            //find the last Month and the current session
            await viewModelMonth.ExecuteGetGroupByProfnameCommand(GroupDetail.myData.ProfName, GroupDetail.myData.GroupName, GroupDetail.myData.SchoolYear);
            Month CurrentMonth = new Month();
            bool Changement = false;
            foreach (var month in viewModelMonth.Months)
            {
                if (month.CurrentSessions != month.NumberSessions && month.CurrentSessions != 0)
                {
                    CurrentMonth = month;
                    Changement = true;
                    break;
                }
            }
            //Add Changement
            if (Changement)
                await viewModelChangement.ExecuteAddItemCommand(GroupDetail.myData.GroupName, GroupDetail.myData.SchoolYear, StudentDetail.myData.Username, GroupDetail.myData.ProfName, CurrentMonth.Slug, (int)CurrentMonth.CurrentSessions);
            if (GroupDetail.myData.ProfName == myData.Username)
            {
                //Get the Item to Delete
                await viewModelStudent.ExecuteGetStudentsGroupCommand(GroupDetail.myData.ProfName, StudentDetail.myData.Username, GroupDetail.myData.SchoolYear);
                //Delete Item
                await viewModelStudent.ExecuteDeleteItemCommand(viewModelStudent.GroupStudents[0]);
                //Modify the number of student in the old group
                await viewModel.ExecuteUpdateMinus(GroupDetail.myData);
                //Get the Student Id in the new Group
                long max = 0;
                await viewModelStudent.ExecuteGetGroupCommand(GroupDetail.myData.ProfName, GroupDetail.myData.GroupName, GroupDetail.myData.SchoolYear);
                foreach (var student in viewModelStudent.GroupStudents)
                {

                    if (max < student.student_id) max = student.student_id;
                }
                //Add the student to the new group
                await viewModelStudent.ExecuteAddItemCommand(FirstItem.GroupName, StudentDetail.myData.Username, GroupDetail.myData.ProfName, max + 1);
                //Modify the number of student in the new group
                await viewModel.ExecuteUpdateNumberOfStudents(FirstItem);
            }
            else 
            {
                await viewModelStudent.ExecuteGetStudentsGroupCommand(GroupDetail.myData.ProfName, StudentDetail.myData.Username, GroupDetail.myData.SchoolYear);
                GroupStudents student = viewModelStudent.GroupStudents[0];
                if (student.IsStudying)
                {
                    //9ayed el telmidh batal 
                    student.IsStudying = false;
                    await viewModelStudent.ExecuteUpdateItemCommand(student, GroupDetail.myData.ProfName, student.SchoolYear, GroupDetail.myData.GroupName, student.student_id);
                    //Modify the number of student in the old group
                    await viewModel.ExecuteUpdateMinus(GroupDetail.myData);
                    //Get the Student Id in the new Group
                    long max = 0;
                    await viewModelStudent.ExecuteGetGroupCommand(myData.Username, GroupDetail.myData.GroupName, GroupDetail.myData.SchoolYear);
                    foreach (var studentGroup in viewModelStudent.GroupStudents)
                    {
                        if (max < studentGroup.student_id) max = studentGroup.student_id;
                    }
                    //Add the student to the new prof
                    await viewModelStudent.ExecuteAddItemCommand(FirstItem.GroupName, StudentDetail.myData.Username, myData.Username, max + 1);
                    //Modify the number of student in the new group
                    await viewModel.ExecuteUpdateNumberOfStudents(FirstItem);
                }
            }
            bool ChangeGroup = false;
            string par = JsonConvert.SerializeObject(ChangeGroup);
            Frame.Navigate(typeof(ProfSearch), par);
        }

        private void Level_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
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

            //studentsNumber = 0;
            foreach (var account in suggestions)
            {
                groups.Add(account);
            }

            return suggestions;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string par = JsonConvert.SerializeObject(myData);
            Frame.Navigate(typeof(StudentDetail), par);
        }
    }
}
