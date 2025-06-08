using SDKTemplate.ViewModels;
using System;
using System.Collections.Generic;
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

    public sealed partial class AddPeriod : Page
    {
        GroupSessionViewModel viewModel = new GroupSessionViewModel();
        GroupStudentViewModel viewModelGroup = new GroupStudentViewModel();
        MonthViewModel viewModelMonth = new MonthViewModel();
        public AddPeriod()
        {
            this.InitializeComponent();
        }

        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            DateTime HollDate = new DateTime(Date.Date.Year, Date.Date.Month, Date.Date.Day, Time.Time.Hours, Time.Time.Minutes, Time.Time.Seconds);
            await viewModelGroup.ExecuteGetGroupCommand(GroupDetail.myData.ProfName, GroupDetail.myData.GroupName, GroupDetail.myData.SchoolYear);
            foreach(var student in viewModelGroup.GroupStudents)
            {
                if(student.IsStudying)
                    await viewModel.ExecuteAddItemCommand(GroupDetail.myData.GroupName, GroupDetail.myData.ProfName, student.StudentName, HollDate, long.Parse(SessionId.Text), MonthDetail.myData.Slug, MonthDetail.myData.SchoolYear);
            }
            await viewModelMonth.ExecuteUpdateNumberOfStudents(MonthDetail.myData);
            Frame.Visibility = Visibility.Collapsed;
        }
    }
}
