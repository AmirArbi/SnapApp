using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
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
    public sealed partial class NotePage : Page
    {
        private Color currentColor = Colors.White;
        GroupStudentViewModel viewModel = new GroupStudentViewModel();
        public static GroupStudents myData { get; set; }

        public NotePage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<GroupStudents>((string)e.Parameter);
        }
        private async void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteGetStudentsGroupCommand(myData.ProfName, myData.StudentName, myData.SchoolYear);
            await viewModel.ExecuteUpdateNoteCommand(viewModel.GroupStudents[0], editor.Text);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteGetStudentsGroupCommand(myData.ProfName, myData.StudentName, myData.SchoolYear);
            if(viewModel.GroupStudents.Count != 0)
            {
                if(viewModel.GroupStudents[0].Note != null)
                    editor.Text = viewModel.GroupStudents[0].Note;
            }
        }

        private async void Back_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteGetStudentsGroupCommand(myData.ProfName, myData.StudentName, myData.SchoolYear);
            await viewModel.ExecuteUpdateNoteCommand(viewModel.GroupStudents[0], editor.Text);
            Frame.Visibility = Visibility.Collapsed;
        }
    }
}
