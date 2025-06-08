using Newtonsoft.Json;
using SDKTemplate.Models;
using SDKTemplate.Services;
using SDKTemplate.ViewModels;
using SDKTemplate.Views.Espace_Eléve;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace SDKTemplate.Views.Espace_Prof.Update
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class UpdateStudent : Page
    {
        StudentsAccountViewModel viewModel = new StudentsAccountViewModel();
        public static StudentAccount myData { get; set; }
        public UpdateStudent()
        {
            this.InitializeComponent();
            myData = new StudentAccount();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<StudentAccount>((string)e.Parameter);
            Name.Text = myData.Name;
            Surname.Text = myData.Surname;
            if (myData.Email != null)
            {
                Email.Text  = myData.Email;
            }
            School.Text = myData.School;
            Number.Text = myData.PhoneNumber;
            if (myData.Avrage != null)
            {
                Avrage.Text = myData.Avrage;
            }
            if (myData.Gender == "Homme")
                Gender.SelectedIndex = 0;
            else
                Gender.SelectedIndex = 1;
        }
        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            myData.Name = Name.Text ;
            myData.Surname = Surname.Text ;
            if(myData.Email != null)
            {
                myData.Email = Email.Text;
            }
            myData.School= School.Text ;
            myData.PhoneNumber = Number.Text ;
            myData.Avrage = Avrage.Text ;
            if (Gender.SelectedIndex == 0)
                myData.Gender = "Homme";
            else
                myData.Gender = "Femme";
            await viewModel.ExecuteUpadteAccountCommand(myData);
            if(myData.Page == "StudentProfs")
            {
                myData.Image = null;
                string par = JsonConvert.SerializeObject(myData);
                Frame.Navigate(typeof(StudentProfs), par);
            } else
            {
                myData.Image = null;
                string par = JsonConvert.SerializeObject(myData);
                Frame.Navigate(typeof(StudentDetail), par);
            }
            
        }
    }
}
