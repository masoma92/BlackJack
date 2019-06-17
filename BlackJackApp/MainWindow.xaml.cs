// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackJackApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using GalaSoft.MvvmLight.Messaging;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// Constructor of mainwindow.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "LogicResult", msg =>
            {
                MessageBox.Show(msg);
                this.SetToDeafault();
            });
            this.SetToDeafault();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NormalHandPoints.Visibility = Visibility.Hidden;
            this.NormalHandCards.Visibility = Visibility.Hidden;
            this.NormalButtons.Visibility = Visibility.Hidden;

            this.Button1.Visibility = Visibility.Hidden;
            this.Button2.Visibility = Visibility.Hidden;
            this.Button3.Visibility = Visibility.Hidden;
            this.Button4.Visibility = Visibility.Hidden;

            this.LeftHandPoints.Visibility = Visibility.Visible;
            this.LeftHandCards.Visibility = Visibility.Visible;
            this.RightHandPoints.Visibility = Visibility.Visible;
            this.RightHandCards.Visibility = Visibility.Visible;
            this.SplittedButtons.Visibility = Visibility.Visible;
        }

        private void SetToDeafault()
        {
            this.NormalHandPoints.Visibility = Visibility.Visible;
            this.NormalHandCards.Visibility = Visibility.Visible;
            this.NormalButtons.Visibility = Visibility.Visible;

            this.Button1.Visibility = Visibility.Visible;
            this.Button2.Visibility = Visibility.Visible;
            this.Button3.Visibility = Visibility.Visible;
            this.Button4.Visibility = Visibility.Visible;

            this.LeftHandPoints.Visibility = Visibility.Hidden;
            this.LeftHandCards.Visibility = Visibility.Hidden;
            this.RightHandPoints.Visibility = Visibility.Hidden;
            this.RightHandCards.Visibility = Visibility.Hidden;
            this.SplittedButtons.Visibility = Visibility.Hidden;
        }
    }
}
