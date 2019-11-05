// <copyright file="GameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackJackApp
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using BusinessLogic;
    using CommonServiceLocator;
    using Data;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;

    /// <summary>
    /// Implements GameModel class
    /// </summary>
    internal class GameModel : ViewModelBase
    {
        private int bet;
        private Player player;
        private Player comp;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// Implements constructor of GameModel class
        /// </summary>
        public GameModel()
        {
            this.Comp = new Player();

            this.Player = new Player();

            this.Player.PropertyChanged += this.Player_PropertyChanged;

            this.Player.Hand.PropertyChanged += this.Hand_PropertyChanged;

            this.Player.SecondHand.PropertyChanged += this.SecondHand_PropertyChanged;

            this.Comp.Hand.PropertyChanged += this.CompHand_PropertyChanged;

            this.Deck = ServiceLocator.Current.GetInstance<ICardRepository>().GetDeck();

            this.NewGameCommand = new RelayCommand(() => this.Logic.NewGameCommand(this.Player, this.Comp, this.Deck));

            this.BetCommand = new RelayCommand(
                () => this.Logic.BetCommand(this.Player, this.Comp, ref this.bet, this.Deck),
                () => this.player.Hand.Cards.Count == 0 && this.bet != 0);

            this.LoadGameCommand = new RelayCommand(() => this.Logic.LoadCommand(this.Player, this.Comp, this.Deck));

            this.SaveGameCommand = new RelayCommand(
                () => this.Logic.SaveCommand(this.Player),
                () => this.Player.Money != 0);

            this.GetHighScoreCommand = new RelayCommand(() => this.Logic.GetHighscoreCommand());

            this.HitCommand = new RelayCommand(
                () => this.Logic.HitCommand(this.Player, this.Comp, ref this.bet, this.Deck),
                () => this.player.Hand.Cards.Count != 0);

            this.StandCommand = new RelayCommand(
                () => this.Logic.StandCommand(this.Player, this.Comp, ref this.bet, this.Deck),
                () => this.player.Hand.Cards.Count != 0);

            this.DoubleCommand = new RelayCommand(
                () => this.Logic.DoubleCommand(this.Player, this.Comp, ref this.bet, this.Deck),
                () => this.player.Hand.Cards.Count == 2 && this.bet <= this.player.Money);

            this.SplitCommand = new RelayCommand(
                () => this.Logic.SplitCommand(this.Player, ref this.bet),
                () => this.player.Hand.Cards.Count == 2 && this.player.Hand.Cards[0].Value == this.player.Hand.Cards[1].Value && this.bet <= this.player.Money);

            this.SurrenderCommand = new RelayCommand(
                () => this.Logic.SurrenderCommand(this.Player, this.Comp, ref this.bet, this.Deck),
                () => this.player.Hand.Cards.Count == 2);

            this.HitLeftCommand = new RelayCommand(
                () => this.Logic.HitFirstHand(this.Player, this.Comp, this.Deck, this.bet),
                () => !this.player.Hand.IsNotActive);

            this.HitRightCommand = new RelayCommand(
                () => this.Logic.HitSecondHand(this.Player, this.Comp, this.Deck, this.bet),
                () => !this.player.SecondHand.IsNotActive);

            this.StandLeftCommand = new RelayCommand(
                () => this.Logic.StandFirstHand(this.Player, this.Comp, this.Deck, ref this.bet),
                () => !this.player.Hand.IsNotActive);

            this.StandRightCommand = new RelayCommand(
                () => this.Logic.StandSecondHand(this.Player, this.Comp, this.Deck, ref this.bet),
                () => !this.player.SecondHand.IsNotActive);
        }

        /// <summary>
        /// Gets return the image of 1st player card.
        /// </summary>
        public string PlayerCard1
        {
            get { return this.Player.Hand.Cards.Count >= 1 ? this.Player.Hand.Cards[0].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 2nd player card.
        /// </summary>
        public string PlayerCard2
        {
            get { return this.Player.Hand.Cards.Count >= 2 ? this.Player.Hand.Cards[1].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 3rd player card.
        /// </summary>
        public string PlayerCard3
        {
            get { return this.Player.Hand.Cards.Count >= 3 ? this.Player.Hand.Cards[2].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 4th player card.
        /// </summary>
        public string PlayerCard4
        {
            get { return this.Player.Hand.Cards.Count >= 4 ? this.Player.Hand.Cards[3].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 5th player card.
        /// </summary>
        public string PlayerCard5
        {
            get { return this.Player.Hand.Cards.Count >= 5 ? this.Player.Hand.Cards[4].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 6th player card.
        /// </summary>
        public string PlayerCard6
        {
            get { return this.Player.Hand.Cards.Count >= 6 ? this.Player.Hand.Cards[5].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 1st card of player right hand.
        /// </summary>
        public string PlayerRightCard1
        {
            get { return this.Player.SecondHand.Cards.Count >= 1 ? this.Player.SecondHand.Cards[0].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 2nd card of player right hand.
        /// </summary>
        public string PlayerRightCard2
        {
            get { return this.Player.SecondHand.Cards.Count >= 2 ? this.Player.SecondHand.Cards[1].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 3rd card of player right hand.
        /// </summary>
        public string PlayerRightCard3
        {
            get { return this.Player.SecondHand.Cards.Count >= 3 ? this.Player.SecondHand.Cards[2].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 4th card of player right hand.
        /// </summary>
        public string PlayerRightCard4
        {
            get { return this.Player.SecondHand.Cards.Count >= 4 ? this.Player.SecondHand.Cards[3].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 5th card of player right hand.
        /// </summary>
        public string PlayerRightCard5
        {
            get { return this.Player.SecondHand.Cards.Count >= 5 ? this.Player.SecondHand.Cards[4].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 6th card of player right hand.
        /// </summary>
        public string PlayerRightCard6
        {
            get { return this.Player.SecondHand.Cards.Count >= 6 ? this.Player.SecondHand.Cards[5].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 1st comp card.
        /// </summary>
        public string CompCard1
        {
            get { return this.Comp.Hand.Cards.Count >= 1 ? this.Comp.Hand.Cards[0].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 2nd comp card.
        /// </summary>
        public string CompCard2
        {
            get { return this.Comp.Hand.Cards.Count >= 2 ? this.Comp.Hand.Cards[1].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 3rd comp card.
        /// </summary>
        public string CompCard3
        {
            get { return this.Comp.Hand.Cards.Count >= 3 ? this.Comp.Hand.Cards[2].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 4th comp card.
        /// </summary>
        public string CompCard4
        {
            get { return this.Comp.Hand.Cards.Count >= 4 ? this.Comp.Hand.Cards[3].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 5th comp card.
        /// </summary>
        public string CompCard5
        {
            get { return this.Comp.Hand.Cards.Count >= 5 ? this.Comp.Hand.Cards[4].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the image of 6th comp card.
        /// </summary>
        public string CompCard6
        {
            get { return this.Comp.Hand.Cards.Count >= 6 ? this.Comp.Hand.Cards[5].Image : string.Empty; }
        }

        /// <summary>
        /// Gets return the bettingtext label for view layer.
        /// </summary>
        public string BettingText
        {
            get { return this.Player.Hand.ActualPoints == 0 ? string.Empty : $"Player bets {this.bet}$"; }
        }

        /// <summary>
        /// Gets return the PlayerActualPoints label for view layer.
        /// </summary>
        public string PlayerActualPoints
        {
            get { return this.Player.Hand.ActualPoints == 0 ? string.Empty : this.Player.Hand.ActualPoints.ToString(); }
        }

        /// <summary>
        /// Gets return the PlayerLeftHandActualPoints label for view layer.
        /// </summary>
        public string PlayerLeftHandActualPoints
        {
            get { return this.Player.Hand.ActualPoints == 0 ? string.Empty : this.Player.Hand.ActualPoints.ToString(); }
        }

        /// <summary>
        /// Gets return the PlayerRightHandActualPoints label for view layer.
        /// </summary>
        public string PlayerRightHandActualPoints
        {
            get { return this.Player.SecondHand.ActualPoints == 0 ? string.Empty : this.Player.SecondHand.ActualPoints.ToString(); }
        }

        /// <summary>
        /// Gets return the CompActualPoints label for view layer.
        /// </summary>
        public string CompActualPoints
        {
            get { return this.Comp.Hand.ActualPoints == 0 ? string.Empty : this.Comp.Hand.ActualPoints.ToString(); }
        }

        /// <summary>
        /// Gets return the PlayerLabel label for view layer.
        /// </summary>
        public string PlayerLabel
        {
            get { return this.Player.IsLost == true ? string.Empty : $"Name: {this.Player.Name} || Money: {this.Player.Money}";  }
        }

        /// <summary>
        /// Gets or sets the bet amount.
        /// </summary>
        public int Bet
        {
            get { return this.bet; }
            set { this.Set(ref this.bet, value); }
        }

        /// <summary>
        /// Gets or sets collection of all the cards.
        /// </summary>
        public ObservableCollection<Card> Deck { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public Player Player
        {
            get { return this.player; }
            set { this.Set(ref this.player, value); }
        }

        /// <summary>
        /// Gets or sets the comp.
        /// </summary>
        public Player Comp
        {
            get { return this.comp; }
            set { this.Set(ref this.comp, value); }
        }

        /// <summary>
        /// Gets or sets NewGameCommand ICommand interface.
        /// </summary>
        public ICommand NewGameCommand { get; set; }

        /// <summary>
        /// Gets or sets LoadGameCommand ICommand interface.
        /// </summary>
        public ICommand LoadGameCommand { get; set; }

        /// <summary>
        /// Gets or sets SaveGameCommand ICommand interface.
        /// </summary>
        public ICommand SaveGameCommand { get; set; }

        /// <summary>
        /// Gets or sets GetHighScoreCommand ICommand interface.
        /// </summary>
        public ICommand GetHighScoreCommand { get; set; }

        /// <summary>
        /// Gets or sets BetCommand ICommand interface.
        /// </summary>
        public ICommand BetCommand { get; set; }

        /// <summary>
        /// Gets or sets HitCommand ICommand interface.
        /// </summary>
        public ICommand HitCommand { get; set; }

        /// <summary>
        /// Gets or sets StandCommand ICommand interface.
        /// </summary>
        public ICommand StandCommand { get; set; }

        /// <summary>
        /// Gets or sets DoubleCommand ICommand interface.
        /// </summary>
        public ICommand DoubleCommand { get; set; }

        /// <summary>
        /// Gets or sets SplitCommand ICommand interface.
        /// </summary>
        public ICommand SplitCommand { get; set; }

        /// <summary>
        /// Gets or sets SurrenderCommand ICommand interface.
        /// </summary>
        public ICommand SurrenderCommand { get; set; }

        /// <summary>
        /// Gets or sets HitLeftCommand ICommand interface.
        /// </summary>
        public ICommand HitLeftCommand { get; set; }

        /// <summary>
        /// Gets or sets StandLeftCommand ICommand interface.
        /// </summary>
        public ICommand StandLeftCommand { get; set; }

        /// <summary>
        /// Gets or sets HitRightCommand ICommand interface.
        /// </summary>
        public ICommand HitRightCommand { get; set; }

        /// <summary>
        /// Gets or sets StandRightCommand ICommand interface.
        /// </summary>
        public ICommand StandRightCommand { get; set; }

        private IGameLogic Logic
        {
            get { return ServiceLocator.Current.GetInstance<IGameLogic>(); }
        }

        private void SecondHand_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged("PlayerRightHandActualPoints");
            this.RaisePropertyChanged("PlayerRightCard1");
            this.RaisePropertyChanged("PlayerRightCard2");
            this.RaisePropertyChanged("PlayerRightCard3");
            this.RaisePropertyChanged("PlayerRightCard4");
            this.RaisePropertyChanged("PlayerRightCard5");
            this.RaisePropertyChanged("PlayerRightCard6");
        }

        private void CompHand_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged("CompActualPoints");
            this.RaisePropertyChanged("CompCard1");
            this.RaisePropertyChanged("CompCard2");
            this.RaisePropertyChanged("CompCard3");
            this.RaisePropertyChanged("CompCard4");
            this.RaisePropertyChanged("CompCard5");
            this.RaisePropertyChanged("CompCard6");
        }

        private void Hand_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged("BettingText");
            this.RaisePropertyChanged("PlayerActualPoints");
            this.RaisePropertyChanged("PlayerLeftHandActualPoints");
            this.RaisePropertyChanged("Bet");
            this.RaisePropertyChanged("PlayerCard1");
            this.RaisePropertyChanged("PlayerCard2");
            this.RaisePropertyChanged("PlayerCard3");
            this.RaisePropertyChanged("PlayerCard4");
            this.RaisePropertyChanged("PlayerCard5");
            this.RaisePropertyChanged("PlayerCard6");
        }

        private void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged("PlayerLabel");
        }
    }
}
