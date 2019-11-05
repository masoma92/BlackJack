// <copyright file="NewGameWindow.xaml.cs" company="PlaceholderCompany">
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

    /// <summary>
    /// Interaction logic for NewGameWindow.xaml
    /// </summary>
    public partial class NewGameWindow : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewGameWindow"/> class.
        /// Constructor of newGameWindow.
        /// </summary>
        public NewGameWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the name prop from newgamemodel.
        /// </summary>
        public new string Name
        {
            get { return (this.Resources["VM"] as NewGameModel).Name; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(sender as DependencyObject).DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(sender as DependencyObject).DialogResult = false;
        }
    }
}
