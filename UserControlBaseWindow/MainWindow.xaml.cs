﻿using System.Windows;

namespace UserControlBaseWindow
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Views.ViewLocator.Sample1.Show();
        }
    }
}
