using Newtonsoft.Json;
using SDKTemplate.Models;
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
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Views.Espace_SNAP
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Expenses : Page
    {
        public ObservableCollection<string> employers = new ObservableCollection<string>();
        EmployerViewModel viewModel = new EmployerViewModel();
        public Expenses()
        {
            this.InitializeComponent();
            Add.Visibility = Visibility.Collapsed;
        }

        private void Continuer_Click(object sender, RoutedEventArgs e)
        {
            Expense password = new Expense
            {
                AdminName = "Administration",
                Username = Utilisateur.SelectedValue.ToString(),
                Amount = (float) Amount.Value,
                Cause = Note.Text
            };
            string par = JsonConvert.SerializeObject(password);
            Add.Navigate(typeof(PasswordView), par);
            Add.Visibility = Visibility.Visible;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteLoadItemsCommand();
            employers.Clear();
            foreach(var employer in viewModel.employers)
            {
                employers.Add(employer.Name + " " + employer.Surname);
            }
            employers.Add("Add employer");
        }

        private void Utilisateur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Utilisateur.SelectedValue.ToString() == "Add employer")
            {
                Add.Navigate(typeof(AddEmployer));
                Add.Visibility = Visibility.Visible;
            }
        }
    }
}
