using Newtonsoft.Json;
using PrintSample;
using SDKTemplate.Code;
using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Printing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Views.Espace_Eléve
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class PaymentPage : Page
    {
        ReceiptViewModel viewModel = new ReceiptViewModel();
        MonthViewModel viewModelMonth = new MonthViewModel();
        private PrintHelper printHelper;
        public Month myData { get; set; }
        public PaymentPage()
        {
            this.InitializeComponent();
            myData = new Month();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myData = JsonConvert.DeserializeObject<Month>((string)e.Parameter);

        }
        private async void Continuer_Click(object sender, RoutedEventArgs e)
        {
            if(myData.Slug == "inscription")
            {
                await viewModelMonth.ExecuteMonthCommand("CentreSnap", myData.GroupName, myData.Slug, myData.SchoolYear);
                await viewModel.ExecuteGetStudentPaymentCommand("CentreSnap", StudentProfs.myData.Username, myData.GroupName, myData.SchoolYear, myData.Slug);
            }
            else
            {
                await viewModelMonth.ExecuteMonthCommand(StudentMonths.myData.Username, myData.GroupName, myData.Slug, myData.SchoolYear);
                await viewModel.ExecuteGetStudentPaymentCommand(StudentMonths.myData.Username, StudentProfs.myData.Username, myData.GroupName, myData.SchoolYear, myData.Slug);
            }
            int i = 0;
            if(viewModel.receipts.Count == 0)
            {
                if (float.Parse(Money.Text) == viewModelMonth.Months[0].WantedMoney)
                {
                    if (myData.Slug == "inscription")
                        await viewModel.ExecuteAddItemCommand(StudentProfs.myData.Username, "CentreSnap", myData.GroupName, float.Parse(Money.Text), Payement.SelectedValue.ToString(), null, myData.Slug, myData.SchoolYear, Visibility.Collapsed);
                    else
                        await viewModel.ExecuteAddItemCommand(StudentProfs.myData.Username, StudentMonths.myData.Username, myData.GroupName, float.Parse(Money.Text), Payement.SelectedValue.ToString(), null, myData.Slug, myData.SchoolYear, Visibility.Collapsed);
                }
                else
                {
                    if (myData.Slug == "inscription")
                        await viewModel.ExecuteAddItemCommand(StudentProfs.myData.Username, "CentreSnap", myData.GroupName, float.Parse(Money.Text), Payement.SelectedValue.ToString(), null, myData.Slug, myData.SchoolYear, Visibility.Visible);
                    else
                        await viewModel.ExecuteAddItemCommand(StudentProfs.myData.Username, StudentMonths.myData.Username, myData.GroupName, float.Parse(Money.Text), Payement.SelectedValue.ToString(), null, myData.Slug, myData.SchoolYear, Visibility.Visible);
                }
            } else
            {
                float somme = 0;
                foreach (var receipt in viewModel.receipts)
                {
                    i++;
                    somme += receipt.PaidMoney;
                }
                if (somme + float.Parse(Money.Text) == viewModelMonth.Months[0].WantedMoney)
                {
                    if (myData.Slug == "inscription")
                        await viewModel.ExecuteAddItemCommand(StudentProfs.myData.Username, "CentreSnap", myData.GroupName, float.Parse(Money.Text), Payement.SelectedValue.ToString(), null, myData.Slug, myData.SchoolYear, Visibility.Collapsed);
                    else    
                        await viewModel.ExecuteAddItemCommand(StudentProfs.myData.Username, StudentMonths.myData.Username, myData.GroupName, float.Parse(Money.Text), Payement.SelectedValue.ToString(), null, myData.Slug, myData.SchoolYear, Visibility.Collapsed);
                }
                else
                {
                    if (myData.Slug == "inscription")
                        await viewModel.ExecuteAddItemCommand(StudentProfs.myData.Username, "CentreSnap", myData.GroupName, float.Parse(Money.Text), Payement.SelectedValue.ToString(), null, myData.Slug, myData.SchoolYear, Visibility.Visible);
                    else
                        await viewModel.ExecuteAddItemCommand(StudentProfs.myData.Username, StudentMonths.myData.Username, myData.GroupName, float.Parse(Money.Text), Payement.SelectedValue.ToString(), null, myData.Slug, myData.SchoolYear, Visibility.Visible);
                }

            }

            printHelper = new PrintHelper(this);
            printHelper.RegisterForPrinting();

            // Initialize print content for this scenario
            if (myData.Slug == "inscription")
                await viewModel.ExecuteGetStudentPaymentCommand("CentreSnap", StudentProfs.myData.Username, myData.GroupName, myData.SchoolYear, myData.Slug);
             else
                await viewModel.ExecuteGetStudentPaymentCommand(StudentMonths.myData.Username, StudentProfs.myData.Username, myData.GroupName, myData.SchoolYear, myData.Slug);
            printHelper.PreparePrintContent(new ReçuView(viewModel.receipts[i]));
            await printHelper.ShowPrintUIAsync();
            
        }

        private void Exist_Click(object sender, RoutedEventArgs e)
        {
            if (printHelper != null)
            {
                printHelper.UnregisterForPrinting();
            }
            Frame.Visibility = Visibility.Collapsed;
        }
    }
}
