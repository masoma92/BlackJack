// <copyright file="Hand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;

    /// <summary>
    /// Hand class.
    /// </summary>
    public class Hand : ObservableObject
    {
        private int actualPoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hand"/> class.
        /// Constructor of the hand.
        /// </summary>
        public Hand()
        {
            this.Cards = new ObservableCollection<Card>();
            this.PointsA = 0;
            this.PointsB = 0;
        }

        /// <summary>
        /// Gets or sets collection of the cards in the hand.
        /// </summary>
        public ObservableCollection<Card> Cards { get; set; }

        /// <summary>
        /// Gets or sets the actualpoints of the hand.
        /// </summary>
        public int ActualPoints
        {
            get { return this.actualPoints; }
            set { this.Set(ref this.actualPoints, value); }
        }

        /// <summary>
        /// Gets or sets the higher points if there is an ace.
        /// </summary>
        public int PointsA { get; set; }

        /// <summary>
        /// Gets or sets the lower points if there is an ace.
        /// </summary>
        public int PointsB { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hand is active.
        /// </summary>
        public bool IsNotActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hand is lost.
        /// </summary>
        public bool IsLost { get; set; }
    }
}
