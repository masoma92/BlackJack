// <copyright file="ScoreBoardWindow.xaml.cs" company="PlaceholderCompany">
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
    /// Interaction logic for ScoreBoardWindow.xaml
    /// </summary>
    public partial class ScoreBoardWindow : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoardWindow"/> class.
        /// ScoreBoardWindow constructor.
        /// </summary>
        public ScoreBoardWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets CompPoints.
        /// </summary>
        public string CompPoint
        {
            get { return (this.Resources["VM"] as ScoreBoardModel).CompPoint; }
        }

        /// <summary>
        /// Gets PlayerPoints.
        /// </summary>
        public string PlayerPoint
        {
            get { return (this.Resources["VM"] as ScoreBoardModel).CompPoint; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(sender as DependencyObject).DialogResult = false;
        }
    }
}
