using SDKTemplate.Models;
using SDKTemplate.ViewModels;
using System;
using System.Collections.Generic;
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

namespace SDKTemplate.Views.Reglage
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ReglageView : Page
    {
        PasswordViewModel viewModel = new PasswordViewModel();
        public ReglageView()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush red = new SolidColorBrush(Colors.Red);
            Button clicked = (Button)sender;
            if (clicked.Name == "ConfirmAdm")
            {
                Password password = new Password
                {
                    AdminPassword = OldPassword.Password,
                    AdminName = "Administration"
                };
                if(OldPassword.Password.Length != 0 )
                {
                    if(Password.Password != "")
                    {
                        await viewModel.ExecuteCheckPasswordCommand(password);
                        if (viewModel.res == "1")
                        {
                            ContinuAdmButton.Visibility = Visibility.Visible;
                            ConfirmPassword.Visibility = Visibility.Visible;
                            DirectorConfirmPassword.Visibility = Visibility.Collapsed;
                            ContinueDirButton.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            AdministrationPassowrdStatue.Text = "Votre ancien mot de passe est incorrect";
                            AdministrationPassowrdStatue.Foreground = red;
                            AdministrationPassowrdStatue.Visibility = Visibility.Visible;
                        }
                    } else
                    {
                        AdministrationPassowrdStatue.Text = "Votre nouveau mot de passe est vide";
                        AdministrationPassowrdStatue.Foreground = red;
                        AdministrationPassowrdStatue.Visibility = Visibility.Visible;
                    }
                } else
                {
                    AdministrationPassowrdStatue.Text = "Votre ancien mot de passe est vide";
                    AdministrationPassowrdStatue.Foreground = red;
                    AdministrationPassowrdStatue.Visibility = Visibility.Visible;
                }
                
                
            } else if (clicked.Name == "ConfirmDir")
            {
                Password password = new Password
                {
                    AdminPassword = OldPasswordDir.Password,
                    AdminName = "Directeur"
                };
                if (OldPasswordDir.Password.Length != 0)
                {
                    if(DirectorPassword.Password != "")
                    {
                        await viewModel.ExecuteCheckPasswordCommand(password);
                        if (viewModel.res == "1")
                        {
                            ContinuAdmButton.Visibility = Visibility.Collapsed;
                            ConfirmPassword.Visibility = Visibility.Collapsed;
                            DirectorConfirmPassword.Visibility = Visibility.Visible;
                            ContinueDirButton.Visibility = Visibility.Visible;
                        } else
                        {
                            DirectorPassowrdStatue.Text = "Votre ancien mot de passe est incorrect";
                            DirectorPassowrdStatue.Foreground = red;
                            DirectorPassowrdStatue.Visibility = Visibility.Visible;
                        }
                    } else
                    {
                        DirectorPassowrdStatue.Text = "Votre nouveau mot de passe est vide";
                        DirectorPassowrdStatue.Foreground = red;
                        DirectorPassowrdStatue.Visibility = Visibility.Visible;
                    }     
                } else
                {
                    DirectorPassowrdStatue.Text = "Votre ancien mot de passe est vide";
                    DirectorPassowrdStatue.Foreground = red;
                    DirectorPassowrdStatue.Visibility = Visibility.Visible;
                }
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;
            SolidColorBrush red = new SolidColorBrush(Colors.Red);
            SolidColorBrush green = new SolidColorBrush(Colors.Green);
            if (clicked.Name == "ContinuAdmButton")
            {
                if(ConfirmPassword.Password == Password.Password)
                {
                    Password password = new Password
                    {
                        AdminPassword = Password.Password,
                        AdminName = "Administration"
                    };
                    await viewModel.ExecuteUpdatePresenceCommand(password);
                    AdministrationPassowrdStatue.Text = "La mot de passe est bien changé";
                    AdministrationPassowrdStatue.Foreground = green;
                    AdministrationPassowrdStatue.Visibility = Visibility.Visible;
                } else
                {
                    AdministrationPassowrdStatue.Text = "Les mots de passe ne correspondent pas";
                    AdministrationPassowrdStatue.Foreground = red;
                    AdministrationPassowrdStatue.Visibility = Visibility.Visible;
                }
            }
            else if (clicked.Name == "ContinueDirButton")
            {
                if (DirectorConfirmPassword.Password == DirectorPassword.Password)
                {
                    Password password = new Password
                    {
                        AdminPassword = DirectorPassword.Password,
                        AdminName = "Directeur"
                    };
                    await viewModel.ExecuteUpdatePresenceCommand(password);
                    DirectorPassowrdStatue.Text = "La mot de passe est bien changé";
                    DirectorPassowrdStatue.Foreground = green;
                    DirectorPassowrdStatue.Visibility = Visibility.Visible;
                }
                else
                {
                    DirectorPassowrdStatue.Text = "Les mots de passe ne correspondent pas";
                    DirectorPassowrdStatue.Foreground = red;
                    DirectorPassowrdStatue.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
