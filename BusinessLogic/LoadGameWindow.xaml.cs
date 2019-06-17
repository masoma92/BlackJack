// <copyright file="LoadGameWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
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
    using Data;

    /// <summary>
    /// Interaction logic for LoadGameWindow.xaml
    /// </summary>
    public partial class LoadGameWindow : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadGameWindow"/> class.
        /// Constructor of loadgamewindow.
        /// </summary>
        public LoadGameWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets selected player from loadgamemodel.
        /// </summary>
        public Player SelectedPlayer
        {
            get { return (this.Resources["VM"] as LoadGameModel).SelectedPlayer; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(sender as DependencyObject).DialogResult = true;
        }
    }
}
