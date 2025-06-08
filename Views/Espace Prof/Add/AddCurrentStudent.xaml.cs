using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class AddCurrentStudent : Page
    {
        ObservableCollection<StudentAccount> Students = new ObservableCollection<StudentAccount>();
        StudentsAccountViewModel viewModel = new StudentsAccountViewModel();
        GroupStudentViewModel viewModelGroup = new GroupStudentViewModel();
        ProfGroupViewModel profGroupViewModel = new ProfGroupViewModel();
        public AddCurrentStudent()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteLoadItemsCommand();
            foreach (var account in viewModel.accounts)
            {
                Students.Add(account);
            }
        }

        private void Name_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = SearchControlsAsync(Name.Text, Surname.Text, School.Text);
            }
        }

        private List<StudentAccount> SearchControlsAsync(string name, string surname, string school)
        {
            var suggestions = new List<StudentAccount>();

            suggestions = viewModel.accounts.Where(x => x.Name.Contains(name)).Where(x => x.Surname.Contains(surname)).Where(x => x.School.Contains(school)).ToList();

            Students.Clear();
            StudentAccount AddStudentAccount = new StudentAccount()
            {
                Username = "AddProf"
            };
            Students.Add(AddStudentAccount);
            foreach (var account in suggestions)
            {
                Students.Add(account);
            }

            return suggestions;
        }

        private async void FilteredListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var FirstItem = (StudentAccount)e.ClickedItem;
            long max = 0;
            await viewModelGroup.ExecuteGetGroupCommand(GroupDetail.myData.ProfName, GroupDetail.myData.GroupName, GroupDetail.myData.SchoolYear);
            foreach(var student in viewModelGroup.GroupStudents)
            {
                if (max < student.student_id) max = student.student_id;
            }
            await viewModelGroup.ExecuteAddItemCommand(GroupDetail.myData.GroupName,FirstItem.Username,GroupDetail.myData.ProfName, max+1 );
            await profGroupViewModel.ExecuteUpdateNumberOfStudents(GroupDetail.myData);
            var parText = GroupDetail.myData;
            string par = JsonConvert.SerializeObject(parText);
            Frame.Navigate(typeof(GroupDetail),par);
        }
        private void Name_DragEnter(object sender, DragEventArgs e)
        {
            var t = (AutoSuggestBox)sender;
            var color = new SolidColorBrush();
            color.Color = Windows.UI.Color.FromArgb(0, 50, 50, 50);
            t.Background = color;
            t.BorderBrush = color;
        }
    }
}
