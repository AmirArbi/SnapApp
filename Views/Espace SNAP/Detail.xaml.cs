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
using Windows.UI;
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
    public sealed partial class Detail : Page
    {
        ObservableCollection<InscriptionModel> inscriptions = new ObservableCollection<InscriptionModel>();
        ReceiptViewModel viewModel = new ReceiptViewModel();
        ExpensesViewModel viewModelExpense = new ExpensesViewModel();
        public Detail()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteLoadItemsCommand();
            await viewModelExpense.ExecuteLoadItemsCommand();
            inscriptions.Clear();
            int CurrentMonth = DateTime.Now.Month;
            int CurrentDay = DateTime.Now.Day;
            for (int i = DateTime.Now.Year; i >= 2018; i--)
            {
                for (int j = CurrentMonth; j > 0; j--)
                {

                    for (int k = CurrentDay; k > 0; k--)
                    {

                        float recette = 0;
                        float depense = 0;
                        foreach (var inscription in viewModel.receipts)
                        {
                            if (inscription.DateTime.Day == k && inscription.DateTime.Month == j && inscription.DateTime.Year == i )
                            {
                                recette += inscription.PaidMoney;
                            }
                        }
                        foreach (var expense in viewModelExpense.sessions)
                        {
                            if (expense.DateTime.Day == k && expense.DateTime.Month == j && expense.DateTime.Year == i)
                            {
                                depense += expense.Amount;
                            }
                        }

                        if (recette != 0 || depense != 0)
                        {
                            DateTime HollDate = new DateTime(i, j, k);
                            InscriptionModel inscription = new InscriptionModel
                            {
                                Date = HollDate.Date,
                                AmountRecette = recette,
                                AmountExpense = depense,
                                AmountGain = recette - depense,
                            };
                            if (inscription.AmountGain < 0)
                            {
                                SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
                                inscription.ColorBrush = redBrush;
                            } else if(inscription.AmountGain > 0)
                            {
                                SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
                                inscription.ColorBrush = greenBrush;
                            } else
                            {
                                SolidColorBrush orangeBrush = new SolidColorBrush(Colors.Orange);
                                inscription.ColorBrush = orangeBrush;
                            }
                            inscriptions.Add(inscription);
                        }
                        CurrentDay = DateTime.DaysInMonth(DateTime.Now.Year, j);
                    }
                    CurrentMonth = 12;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SNAPmenu));
        }

        private void Date_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            float recette = 0;
            float depense = 0;
            inscriptions.Clear();
            foreach (var inscription in viewModel.receipts)
            {
                if (inscription.DateTime.Day == Date.Date.Day && inscription.DateTime.Month == Date.Date.Month && inscription.DateTime.Year == Date.Date.Year)
                {
                    recette += inscription.PaidMoney;
                }
            }
            foreach (var expense in viewModelExpense.sessions)
            {
                if (expense.DateTime.Day == Date.Date.Day && expense.DateTime.Month == Date.Date.Month && expense.DateTime.Year == Date.Date.Year)
                {
                    depense += expense.Amount;
                }
            }
            if (recette != 0 || depense != 0)
            {
                DateTime HollDate = new DateTime(Date.Date.Year, Date.Date.Month, Date.Date.Day);
                InscriptionModel inscription = new InscriptionModel
                {
                    Date = HollDate.Date,
                    AmountRecette = recette,
                    AmountExpense = depense,
                    AmountGain = recette - depense,
                };
                if (inscription.AmountGain < 0)
                {
                    SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
                    inscription.ColorBrush = redBrush;
                }
                else if (inscription.AmountGain > 0)
                {
                    SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
                    inscription.ColorBrush = greenBrush;
                }
                else
                {
                    SolidColorBrush orangeBrush = new SolidColorBrush(Colors.Orange);
                    inscription.ColorBrush = orangeBrush;
                }
                inscriptions.Add(inscription);
            }
        }
    }
}
