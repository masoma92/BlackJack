// <copyright file="Player.cs" company="PlaceholderCompany">
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
    /// Class of the Player.
    /// </summary>
    public class Player : ObservableObject
    {
        private string name;
        private int money;
        private Hand hand;
        private Hand secondhand;
        private bool isLost;
        private bool isWin;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// Creates two empty hands.
        /// </summary>
        public Player()
        {
            this.Hand = new Hand();
            this.SecondHand = new Hand();
        }

        /// <summary>
        /// Gets or sets player's name.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.Set(ref this.name, value); }
        }

        /// <summary>
        /// Gets or sets amount of the player's money.
        /// </summary>
        public int Money
        {
            get { return this.money; }
            set { this.Set(ref this.money, value); }
        }

        /// <summary>
        /// Gets or sets player first hand.
        /// </summary>
        public Hand Hand
        {
            get { return this.hand; }
            set { this.Set(ref this.hand, value); }
        }

        /// <summary>
        /// Gets or sets player second hand.
        /// </summary>
        public Hand SecondHand
        {
            get { return this.secondhand; }
            set { this.Set(ref this.secondhand, value); }
        }

        /// <summary>
        /// Gets a value indicating whetherthe hand is splitted. On the fly prop.
        /// </summary>
        public bool IsSplitted
        {
            get { return this.SecondHand.Cards.Count > 0; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether player isLost.
        /// </summary>
        public bool IsLost
        {
            get { return this.isLost; }
            set { this.Set(ref this.isLost, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether player is win.
        /// </summary>
        public bool IsWin
        {
            get { return this.isWin; }
            set { this.Set(ref this.isWin, value); }
        }
    }
}
