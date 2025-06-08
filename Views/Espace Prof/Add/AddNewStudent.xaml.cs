using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using SDKTemplate.Views.Espace_Eléve;
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
    public sealed partial class AddNewStudent : Page
    {
        StudentsAccountViewModel viewModel = new StudentsAccountViewModel();
        GroupStudentViewModel viewModelGroup = new GroupStudentViewModel();
        GroupSessionViewModel viewModelSession = new GroupSessionViewModel();
        ProfGroupViewModel profGroupViewModel = new ProfGroupViewModel();  
        private Lastpage myData { get; set; }


        public AddNewStudent()
        {
            this.InitializeComponent();
            myData = new Lastpage();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<Lastpage>((string)e.Parameter);
            EleveActual.Visibility = Visibility.Collapsed;
        }
        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            string Password = "student";
            string Username = "";
            string Fullname = Name.Text + Surname.Text;
            bool t = true;
            bool b = true;

            if (Name.Text != null && Surname.Text != null && School.Text != null && Number.Text != null)
            {
                for (int i = 0; i < Fullname.Length; i++)
                {
                    if (Fullname[i] != ' ' && Fullname[i] != '/')
                    {
                        Username += Fullname[i];
                    }
                }
                while (t)
                {
                    StudentAccount student = await viewModel.ExecuteGetStudentAccountCommand(Username);
                    if (student.Username == null)
                    {
                        t = false;
                    }
                    else
                    {
                        b = false;
                        EleveActual.Visibility = Visibility.Visible;
                        Continuer.Visibility = Visibility.Collapsed;
                        Username += "1";
                    }
                }
                if (b)
                {
                    if (myData.LastPage == "GroupDetail")
                    {
                        //Add StuedntAccount
                        await viewModel.ExecuteAddItemCommand(Username, Email.Text, Password, School.Text, Name.Text, Surname.Text, null, Avrage.Text, Number.Text, null, Gender.SelectedValue.ToString());
                        //Add Student to the group
                        await viewModelGroup.ExecuteGetGroupCommand(GroupDetail.myData.ProfName, GroupDetail.myData.GroupName, GroupDetail.myData.SchoolYear);
                        await viewModelGroup.ExecuteAddItemCommand(GroupDetail.myData.GroupName, Username, GroupDetail.myData.ProfName, viewModelGroup.GroupStudents.Count);
                        //Update the Number of Student of the group
                        await profGroupViewModel.ExecuteUpdateNumberOfStudents(GroupDetail.myData);

                        var parText = GroupDetail.myData;
                        string par = JsonConvert.SerializeObject(parText);
                        Frame.Navigate(typeof(GroupDetail), par);
                    }
                    else
                    {
                        //Add Student
                        await viewModel.ExecuteAddItemCommand(Username, Email.Text, Password, School.Text, Name.Text, Surname.Text, null, Avrage.Text, Number.Text, null, Gender.SelectedItem.ToString());
                        Frame.Navigate(typeof(StudentSearch));
                    }
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string Password = "student";
            string Username = "";
            string Fullname = Name.Text + Surname.Text;
            bool t = true;

            if(Name.Text!=null && Surname.Text!= null && School.Text != null && Number.Text != null)
            {
                for (int i = 0; i < Fullname.Length; i++)
                {
                    if (Fullname[i] != ' ' && Fullname[i] != '/')
                    {
                        Username += Fullname[i];
                    }
                }
                while (t)
                {
                    StudentAccount student = await viewModel.ExecuteGetStudentAccountCommand(Username);
                    if (student.Username == null)
                    {
                        t = false;
                        EleveActual.Visibility = Visibility.Visible;
                        Continuer.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        Username += "1";
                    }
                }

                if (myData.LastPage == "GroupDetail")
                {
                    //Add StuedntAccount
                    await viewModel.ExecuteAddItemCommand(Username, Email.Text, Password, School.Text, Name.Text, Surname.Text, null, Avrage.Text, Number.Text, null, Gender.SelectedValue.ToString());
                    //Add Student to the group
                    await viewModelGroup.ExecuteGetGroupCommand(GroupDetail.myData.ProfName, GroupDetail.myData.GroupName, GroupDetail.myData.SchoolYear);
                    await viewModelGroup.ExecuteAddItemCommand(GroupDetail.myData.GroupName, Username, GroupDetail.myData.ProfName, viewModelGroup.GroupStudents.Count);
                    //Update the Number of Student of the group
                    await profGroupViewModel.ExecuteUpdateNumberOfStudents(GroupDetail.myData);

                    var parText = GroupDetail.myData;
                    string par = JsonConvert.SerializeObject(parText);
                    Frame.Navigate(typeof(GroupDetail), par);
                }
                else
                {
                    //Add Student
                    await viewModel.ExecuteAddItemCommand(Username, Email.Text, Password, School.Text, Name.Text, Surname.Text, null, Avrage.Text, Number.Text, null, Gender.SelectedItem.ToString());
                    Frame.Navigate(typeof(StudentSearch));
                }

            }
        }
    }
}
