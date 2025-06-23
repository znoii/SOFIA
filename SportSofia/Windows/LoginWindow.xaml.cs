using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace SportSofia.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            Closing += OnLoginWindowClosing;
            this.Content = new LoginControl();
        }

        private void OnLoginWindowClosing(object? sender, CancelEventArgs e)
        {
            if (DialogResult != true)
            {
                if (MessageBox.Show("Do you want to exit?", "Exit",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
        }
    }
}