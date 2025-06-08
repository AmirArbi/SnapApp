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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Views.Espace_SNAP
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class SNAPmenu : Page
    {
        ReceiptViewModel viewModel = new ReceiptViewModel();
        ExpensesViewModel viewModelExpense = new ExpensesViewModel();
        public SNAPmenu()
        {
            this.InitializeComponent();
        }


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.ExecuteLoadItemsCommand();
            await viewModelExpense.ExecuteLoadItemsCommand();
            var pointsCourbe = new PointCollection();
            var pointsCourbeExpenses = new PointCollection();
            var pointsAnnexe = new PointCollection();
            //design
            DayRecette.Visibility = Visibility.Collapsed;
            Jour.Visibility = Visibility.Visible;
            MoisRecette.Visibility = Visibility.Collapsed;
            Mois.Visibility = Visibility.Visible;
            YearRecette.Visibility = Visibility.Collapsed;
            Year.Visibility = Visibility.Visible;
            //daySchema
            MonthGrid.Visibility = Visibility.Collapsed;
            YearGrid.Visibility = Visibility.Collapsed;
            float Allsom = 0;
            float AllExpenses = 0;
            int k = 1;
            for (int i = 0; i <= 50; i++)
            {
                int mon = 1;
                int amir = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - mon);
                while (DateTime.Now.Day + i - 50 + amir < 0)
                {
                    mon++;
                    amir += DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - mon);
                }
                foreach (var expense in viewModelExpense.sessions)
                {
                    if (DateTime.Now.Day + i - 50 + amir == expense.DateTime.Day && DateTime.Now.Month - mon == expense.DateTime.Month)
                        AllExpenses += expense.Amount;
                }
                foreach (var receipt in viewModel.receipts)
                {
                    if (DateTime.Now.Day + i - 50 + amir == receipt.DateTime.Day && DateTime.Now.Month - mon == receipt.DateTime.Month)
                        Allsom += receipt.PaidMoney;
                }
            }
            float max = Math.Max(Allsom, AllExpenses);
            while (max / 350 > 1)
            {
                max /= 2;
                k++;
            }
            float som = 0;
            float expenses = 0;
            for (int i = 0; i <= 50; i++)
            {
                var pointsGraduationX = new PointCollection();
                TextBlock graduation = new TextBlock();
                int mon = 1;
                int amir = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - mon);
                while (DateTime.Now.Day + i - 50 + amir < 0)
                {
                    mon++;
                    amir += DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - mon);
                }
                if (DateTime.Now.Day + i - 50 == 0)
                {
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 10, 450));
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 10, 530));
                    graduation.Text = DateTime.Now.ToString("MMMM");
                    graduation.FontSize = 14;
                    graduation.Margin = new Thickness(100 + i * 10 + 5, 510, 0, 0);
                }
                else if (i % 5 == 0 && i!=0)
                {
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 10, 460));
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 10, 480));
                    if (DateTime.Now.Day + i - 50 > 0)
                    {
                        graduation.Text = (DateTime.Now.Day + i - 50).ToString();
                    }     
                    else
                    {
                        graduation.Text = (DateTime.Now.Day + i - 50 + amir).ToString();
                    }
                    graduation.Margin = new Thickness(100 + i * 10 - 2, 485,0,0);
                } else if (i != 0)
                {
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 10, 475));
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 10, 465));
                }
                float AffSom = 0;
                float AffExpense = 0;

                foreach (var receipt in viewModel.receipts)
                {
                    if (DateTime.Now.Day + i - 50 + amir == receipt.DateTime.Day && DateTime.Now.Month - mon == receipt.DateTime.Month)
                        som += receipt.PaidMoney;
                    AffSom = som;
                    for (int j = 1; j < k; j++)
                    {
                        AffSom /= 2;
                    }
                }
                foreach (var expense in viewModelExpense.sessions)
                {
                    if (DateTime.Now.Day + i - 50 + amir == expense.DateTime.Day && DateTime.Now.Month - mon == expense.DateTime.Month)
                        expenses += expense.Amount;
                    AffExpense = expenses;
                    for (int j = 1; j < k; j++)
                    {
                        AffExpense /= 2;
                    }
                }
                pointsCourbe.Add(new Windows.Foundation.Point(100 + i*10 , 470 - AffSom));
                pointsCourbeExpenses.Add(new Windows.Foundation.Point(100 + i*10 , 470 - AffExpense));
                var polyline1 = new Polyline();
                polyline1.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
                polyline1.StrokeThickness = 1;
                polyline1.Points = pointsGraduationX;
                Figure.Children.Add(polyline1);
                Figure.Children.Add(graduation);
            }
            for (int i = 150; i <= 500; i+=10)
            {
                TextBlock text = new TextBlock();
                var pointsGraduationY = new PointCollection();
                if (i % 50 == 0)
                {
                    if(i != 500)
                    {
                        pointsGraduationY.Add(new Windows.Foundation.Point(110, i - 30));
                        pointsGraduationY.Add(new Windows.Foundation.Point(90, i - 30));
                    }
                    
                    if(i != 150)
                    {
                        int cnst = i - 150;
                        for (int j = 1; j < k; j++)
                        {
                            cnst *= 2;
                        }
                        text.Text = cnst.ToString();
                        text.Margin = new Thickness(60, 600 - i + 10, 0, 0);
                        Figure.Children.Add(text);
                    }    
                } else
                {
                    pointsGraduationY.Add(new Windows.Foundation.Point(105, i-30));
                    pointsGraduationY.Add(new Windows.Foundation.Point(95, i-30));
                }
                var polyline2 = new Polyline();
                polyline2.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
                polyline2.StrokeThickness = 1;
                polyline2.Points = pointsGraduationY;
                Figure.Children.Add(polyline2);
            }
            pointsAnnexe.Add(new Windows.Foundation.Point(700 , 470));
            pointsAnnexe.Add(new Windows.Foundation.Point(100 , 470));
            pointsAnnexe.Add(new Windows.Foundation.Point(100 , 20));
            Courbe.Points = pointsCourbe;
            DayCourbeDepense.Points = pointsCourbeExpenses;
            Annexe.Points = pointsAnnexe;
            RotateTransform m_transform = new RotateTransform();
            m_transform.Angle = -90;
            MonantText.RenderTransform = m_transform;
            //dayContent
            float dayGain = 0;
            float dayExpense = 0;
            foreach (var receipt in viewModel.receipts)
            {
                if (DateTime.Now.Day == receipt.DateTime.Day)
                    dayGain += receipt.PaidMoney;
            }
            foreach (var expense in viewModelExpense.sessions)
            {
                if (DateTime.Now.Day == expense.DateTime.Day)
                    dayExpense += expense.Amount;
            }
            DayGain.Text = dayGain.ToString();
            DayDépenses.Text = dayExpense.ToString();
            if (dayGain - dayExpense > 0)
            {
                SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
                DayRecette.Foreground = greenBrush;
            }
            else if (dayGain - dayExpense == 0)
            {
                SolidColorBrush orangeBrush = new SolidColorBrush(Colors.Orange);
                DayRecette.Foreground = orangeBrush;
            }
            else
            {
                SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
                DayRecette.Foreground = redBrush;
            }
            DayRecetteMontant.Text = (dayGain - dayExpense).ToString();
            //monthSchema
            var pointsCourbeMonth = new PointCollection();
            var pointsCourbeMonthExpenses = new PointCollection();
            var pointsAnnexeMonth = new PointCollection();
            Allsom = 0;
            AllExpenses = 0;
            k = 1;
            for (int i = 0; i <= 12; i++)
            {
                foreach (var expense in viewModelExpense.sessions)
                {
                    if (DateTime.Now.Month + i - 12 == expense.DateTime.Month)
                        AllExpenses += expense.Amount;
                }
                foreach (var receipt in viewModel.receipts)
                {
                    if (DateTime.Now.Month + i - 12 == receipt.DateTime.Month)
                        Allsom += receipt.PaidMoney;
                }
            }
            max = Math.Max(Allsom, AllExpenses);
            while (max / 350 > 1)
            {
                max /= 2;
                k++;
            }
            som = 0;
            expenses = 0;
            for (int i = 0; i <= 12; i++)
            {
                var pointsGraduationX = new PointCollection();
                TextBlock graduation = new TextBlock();
                if (DateTime.Now.Month + i - 12 == 0)
                {
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 40, 450));
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 40, 530));
                    graduation.Text = DateTime.Now.Year.ToString();
                    graduation.FontSize = 14;
                    graduation.Margin = new Thickness(100 + i * 40 + 5, 510, 0, 0);
                }
                else if (i != 0)
                {
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 40, 460));
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 40, 480));
                    if (DateTime.Now.Month + i - 12 > 0)
                    {
                        graduation.Text = (DateTime.Now.Month + i - 12).ToString();
                    }
                    else
                    {
                        graduation.Text = (DateTime.Now.Month + i).ToString();
                    }
                    graduation.Margin = new Thickness(100 + i * 40 - 2, 485, 0, 0);
                }
                float AffSom = 0;
                float AffExpense = 0;
                foreach (var receipt in viewModel.receipts)
                {
                    if (DateTime.Now.Month + i - 12 == receipt.DateTime.Month)
                        som += receipt.PaidMoney;
                    AffSom = som;
                    for (int j = 1; j < k; j++)
                    {
                        AffSom /= 2;
                    }
                }
                foreach (var expense in viewModelExpense.sessions)
                {
                    if (DateTime.Now.Month + i - 12 == expense.DateTime.Month)
                        expenses += expense.Amount;
                    AffExpense = expenses;
                    for (int j = 1; j < k; j++)
                    {
                        AffExpense /= 2;
                    }
                }
                pointsCourbeMonth.Add(new Windows.Foundation.Point(100 + i * 40, 470 - AffSom));
                pointsCourbeMonthExpenses.Add(new Windows.Foundation.Point(100 + i * 40, 470 - AffExpense));
                var polyline1 = new Polyline();
                polyline1.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
                polyline1.StrokeThickness = 1;
                polyline1.Points = pointsGraduationX;
                MonthFigure.Children.Add(polyline1);
                MonthFigure.Children.Add(graduation);
            }
            for (int i = 150; i <= 500; i += 10)
            {
                TextBlock text = new TextBlock();
                var pointsGraduationY = new PointCollection();
                if (i % 50 == 0)
                {
                    if (i != 500)
                    {
                        pointsGraduationY.Add(new Windows.Foundation.Point(110, i - 30));
                        pointsGraduationY.Add(new Windows.Foundation.Point(90, i - 30));
                    }

                    if (i != 150)
                    {
                        int cnst = i - 150;
                        for (int j = 1; j < k; j++)
                        {
                            cnst *= 2;
                        }
                        text.Text = cnst.ToString();
                        text.Margin = new Thickness(60, 600 - i + 10, 0, 0);
                        MonthFigure.Children.Add(text);
                    }
                }
                else
                {
                    pointsGraduationY.Add(new Windows.Foundation.Point(105, i - 30));
                    pointsGraduationY.Add(new Windows.Foundation.Point(95, i - 30));
                }
                var polyline2 = new Polyline();
                polyline2.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
                polyline2.StrokeThickness = 1;
                polyline2.Points = pointsGraduationY;
                MonthFigure.Children.Add(polyline2);
            }
            pointsAnnexeMonth.Add(new Windows.Foundation.Point(700, 470));
            pointsAnnexeMonth.Add(new Windows.Foundation.Point(100, 470));
            pointsAnnexeMonth.Add(new Windows.Foundation.Point(100, 20));
            MonthCourbe.Points = pointsCourbeMonth;
            MonthCourbeDepense.Points = pointsCourbeMonthExpenses;
            MonthAnnexe.Points = pointsAnnexeMonth;
            RotateTransform m_transformMonth = new RotateTransform();
            m_transformMonth.Angle = -90;
            MonthMonantText.RenderTransform = m_transformMonth;
            //MonthContent
            float monthGain = 0;
            float monthExpense = 0;
            foreach (var receipt in viewModel.receipts)
            {
                if (DateTime.Now.Month == receipt.DateTime.Month)
                    monthGain += receipt.PaidMoney;
            }
            foreach (var expense in viewModelExpense.sessions)
            {
                if (DateTime.Now.Month == expense.DateTime.Month)
                    monthExpense += expense.Amount;
            }
            MonthGain.Text = monthGain.ToString();
            MonthDépenses.Text = monthExpense.ToString();
            if (monthGain - monthExpense > 0)
            {
                SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
                MoisRecette.Foreground = greenBrush;
            } else if (monthGain - monthExpense == 0)
            {
                SolidColorBrush orangeBrush = new SolidColorBrush(Colors.Orange);
                MoisRecette.Foreground = orangeBrush;
            } else
            {
                SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
                MoisRecette.Foreground = redBrush;
            }
            MoisRecetteMontant.Text = (monthGain - monthExpense).ToString();
            //YearSchema
            var pointsCourbeYear = new PointCollection();
            var pointsCourbeYearExpenses = new PointCollection();
            var pointsAnnexeYear = new PointCollection();
            Allsom = 0;
            AllExpenses = 0;
            k = 1;
            for (int i = 0; i <= 12; i++)
            {
                foreach (var expense in viewModelExpense.sessions)
                {
                    if (DateTime.Now.Year + i - 12 == expense.DateTime.Year)
                        AllExpenses += expense.Amount;
                }
                foreach (var receipt in viewModel.receipts)
                {
                    if (DateTime.Now.Year + i - 12 == receipt.DateTime.Year)
                        Allsom += receipt.PaidMoney;
                }
            }
            max = Math.Max(Allsom, AllExpenses);
            while (max / 350 > 1)
            {
                max /= 2;
                k++;
            }
            som = 0;
            expenses = 0;
            for (int i = 0; i <= 12; i++)
            {
                var pointsGraduationX = new PointCollection();
                TextBlock graduation = new TextBlock();
                if (i != 0)
                {
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 40, 460));
                    pointsGraduationX.Add(new Windows.Foundation.Point(100 + i * 40, 480));
                    graduation.Text = (DateTime.Now.Year + i - 12).ToString();
                    graduation.Margin = new Thickness(100 + i * 40 - 15, 485, 0, 0);
                }
                float AffSom = 0;
                float AffExpense = 0;
                foreach (var receipt in viewModel.receipts)
                {
                    if (DateTime.Now.Year + i - 12 == receipt.DateTime.Year)
                        som += receipt.PaidMoney;
                    AffSom = som;
                    for (int j = 1; j < k; j++)
                    {
                        AffSom /= 2;
                    }
                }
                foreach (var expense in viewModelExpense.sessions)
                {
                    if (DateTime.Now.Year + i - 12 == expense.DateTime.Year)
                        expenses += expense.Amount;
                    AffExpense = expenses;
                    for (int j = 1; j < k; j++)
                    {
                        AffExpense /= 2;
                    }
                }
                pointsCourbeYear.Add(new Windows.Foundation.Point(100 + i * 40, 470 - AffSom));
                pointsCourbeYearExpenses.Add(new Windows.Foundation.Point(100 + i * 40, 470 - AffExpense));
                var polyline1 = new Polyline();
                polyline1.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
                polyline1.StrokeThickness = 1;
                polyline1.Points = pointsGraduationX;
                YearFigure.Children.Add(polyline1);
                YearFigure.Children.Add(graduation);
            }
            for (int i = 150; i <= 500; i += 10)
            {
                TextBlock text = new TextBlock();
                var pointsGraduationY = new PointCollection();
                if (i % 50 == 0)
                {
                    if (i != 500)
                    {
                        pointsGraduationY.Add(new Windows.Foundation.Point(110, i - 30));
                        pointsGraduationY.Add(new Windows.Foundation.Point(90, i - 30));
                    }

                    if (i != 150)
                    {
                        int cnst = i - 150;
                        for (int j = 1; j < k; j++)
                        {
                            cnst *= 2;
                        }
                        text.Text = cnst.ToString();
                        text.Margin = new Thickness(60, 600 - i + 10, 0, 0);
                        YearFigure.Children.Add(text);
                    }
                }
                else
                {
                    pointsGraduationY.Add(new Windows.Foundation.Point(105, i - 30));
                    pointsGraduationY.Add(new Windows.Foundation.Point(95, i - 30));
                }
                var polyline2 = new Polyline();
                polyline2.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
                polyline2.StrokeThickness = 1;
                polyline2.Points = pointsGraduationY;
                YearFigure.Children.Add(polyline2);
            }
            pointsAnnexeYear.Add(new Windows.Foundation.Point(700, 470));
            pointsAnnexeYear.Add(new Windows.Foundation.Point(100, 470));
            pointsAnnexeYear.Add(new Windows.Foundation.Point(100, 20));
            YearCourbe.Points = pointsCourbeYear;
            YearCourbeDepense.Points = pointsCourbeYearExpenses;
            YearAnnexe.Points = pointsAnnexeYear;
            RotateTransform m_transformYear = new RotateTransform();
            m_transformYear.Angle = -90;
            YearMonantText.RenderTransform = m_transformYear;
            //YearContent
            float yearGain = 0;
            float yearExpense = 0;
            foreach (var receipt in viewModel.receipts)
            {
                if (DateTime.Now.Year == receipt.DateTime.Year)
                    yearGain += receipt.PaidMoney;
            }
            foreach (var expense in viewModelExpense.sessions)
            {
                if (DateTime.Now.Year == expense.DateTime.Year)
                    yearExpense += expense.Amount;
            }
            YearGain.Text = yearGain.ToString();
            YearDépenses.Text = yearExpense.ToString();
            if (yearGain - yearExpense > 0)
            {
                SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
                YearRecette.Foreground = greenBrush;
            }
            else if (yearGain - yearExpense == 0)
            {
                SolidColorBrush orangeBrush = new SolidColorBrush(Colors.Orange);
                YearRecette.Foreground = orangeBrush;
            }
            else
            {
                SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
                YearRecette.Foreground = redBrush;
            }
            YearRecetteMontant.Text = (yearGain - yearExpense).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Les nombres
            DayRecette.Visibility = Visibility.Visible;
            Jour.Visibility = Visibility.Collapsed;
            MoisRecette.Visibility = Visibility.Collapsed;
            Mois.Visibility = Visibility.Visible;
            YearRecette.Visibility = Visibility.Collapsed;
            Year.Visibility = Visibility.Visible;
            //les shemas
            DayGrid.Visibility = Visibility.Visible;
            MonthGrid.Visibility = Visibility.Collapsed;
            YearGrid.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //les nombres
            DayRecette.Visibility = Visibility.Collapsed;
            Jour.Visibility = Visibility.Visible;
            MoisRecette.Visibility = Visibility.Visible;
            Mois.Visibility = Visibility.Collapsed;
            YearRecette.Visibility = Visibility.Collapsed;
            Year.Visibility = Visibility.Visible;
            //les shemas
            DayGrid.Visibility = Visibility.Collapsed;
            MonthGrid.Visibility = Visibility.Visible;
            YearGrid.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //les nombres
            DayRecette.Visibility = Visibility.Collapsed;
            Jour.Visibility = Visibility.Visible;
            MoisRecette.Visibility = Visibility.Collapsed;
            Mois.Visibility = Visibility.Visible;
            YearRecette.Visibility = Visibility.Visible;
            Year.Visibility = Visibility.Collapsed;
            //les shemas
            DayGrid.Visibility = Visibility.Collapsed;
            MonthGrid.Visibility = Visibility.Collapsed;
            YearGrid.Visibility = Visibility.Visible;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Inscription));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Detail));
        }
    }
}
