using SDKTemplate.ViewModels;
using SDKTemplate.Views.Espace_Prof;
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

namespace SDKTemplate.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ReductionPage : Page
    {
        GroupStudentViewModel viewModel = new GroupStudentViewModel();
        public ReductionPage()
        {
            this.InitializeComponent();
        }

        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteGetStudentsGroupCommand(GroupDetail.myData.ProfName, StudentDetail.myData.Username, GroupDetail.myData.SchoolYear);
            await viewModel.ExecuteUpdateReductionCommand(viewModel.GroupStudents[0], (float) Reduction.Value);
            Frame.Visibility = Visibility.Collapsed;
        }
    }
}
