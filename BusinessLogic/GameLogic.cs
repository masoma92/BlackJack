// <copyright file="GameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using Data;
    using GalaSoft.MvvmLight.Messaging;

    /// <summary>
    /// Implements IGameLogic interface.
    /// </summary>
    public class GameLogic : IGameLogic
    {
        private static Random r = new Random();

        /// <summary>
        /// Implements bet command.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="bet">Ref of the bet amount</param>
        /// <param name="deck">Ref of the Deck</param>
        public void BetCommand(Player player, Player comp, ref int bet, IList<Card> deck)
        {
            player.Money -= bet;
            this.SetHandsAndPointsToNull(player, comp, deck);
            this.HitCommand(player, comp, ref bet, deck);
            this.HitCommand(player, comp, ref bet, deck);
            this.HitForComp(comp, deck);
        }

        /// <summary>
        /// Implements HitCommand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="bet">Ref of the bet amount</param>
        /// <param name="deck">Ref of the Deck</param>
        public void HitCommand(Player player, Player comp, ref int bet, IList<Card> deck)
        {
            Card card = deck[r.Next(deck.Count)];
            player.Hand.Cards.Add(card);
            if (card.Value == 11)
            {
                player.Hand.PointsA += card.Value;
                player.Hand.PointsB += 1;
            }
            else
            {
                player.Hand.PointsA += card.Value;
                player.Hand.PointsB += card.Value;
            }

            if (player.Hand.PointsA > 21 && player.Hand.PointsB <= 21)
            {
                player.Hand.ActualPoints = player.Hand.PointsB;
            }
            else
            {
                player.Hand.ActualPoints = player.Hand.PointsA;
            }

            deck.Remove(card);

            if (player.Hand.ActualPoints > 21)
            {
                Messenger.Default.Send($"You lost: {bet}$", "LogicResult");
                this.WeAreLost(player);
                bet = 0;
                player.Hand.IsLost = true;
                this.SetHandsAndPointsToNull(player, comp, deck);
            }
        }

        /// <summary>
        /// Implements StandCommand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="bet">Ref of the bet amount</param>
        /// <param name="deck">Ref of the Deck</param>
        public void StandCommand(Player player, Player comp, ref int bet, IList<Card> deck)
        {
            player.Hand.IsNotActive = true; // inaktívvá teszi a gombokat
            while (player.Hand.ActualPoints >= comp.Hand.ActualPoints && comp.Hand.ActualPoints < 17)
            {
                this.HitForComp(comp, deck);
            }

            if (comp.Hand.ActualPoints > 21 || comp.Hand.ActualPoints < player.Hand.ActualPoints)
            {
                this.WinWithOneHand(ref bet, player);
                this.WeAreWin(player);
            }
            else if (comp.Hand.ActualPoints > player.Hand.ActualPoints)
            {
                Messenger.Default.Send($"You lost: {bet}$", "LogicResult");
                this.WeAreLost(player);
                bet = 0;
            }
            else
            {
                Messenger.Default.Send($"Draw, you won: {bet}$", "LogicResult");
                this.WeAreWin(player);
                player.Money += bet;
                bet = 0;
            }

            this.SetHandsAndPointsToNull(player, comp, deck);
        }

        /// <summary>
        /// Implements StandCommand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="bet">Ref of the bet amount</param>
        /// <param name="deck">Ref of the Deck</param>
        public void DoubleCommand(Player player, Player comp, ref int bet, IList<Card> deck)
        {
            player.Money -= bet;
            bet *= 2;
            this.HitCommand(player, comp, ref bet, deck);
            if (player.Hand.Cards.Count != 0)
            {
                this.StandCommand(player, comp, ref bet, deck);
            }
        }

        /// <summary>
        /// Implements SurrenderCommand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="bet">Ref of the bet amount</param>
        /// <param name="deck">Ref of the Deck</param>
        public void SurrenderCommand(Player player, Player comp, ref int bet, IList<Card> deck)
        {
            player.Money += bet / 2;
            Messenger.Default.Send($"You won: {bet / 2}$", "LogicResult");
            bet = 0;
            this.SetHandsAndPointsToNull(player, comp, deck);
        }

        /// <summary>
        /// Implements SplitCommand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="bet">Ref of the bet amount</param>
        public void SplitCommand(Player player, ref int bet)
        {
            /*player.Hand.Cards.Count == 2 && player.Hand.Cards[0].Value == player.Hand.Cards[1].Value*/

            Card c = player.Hand.Cards[1];
            player.SecondHand.Cards.Add(c);
            player.Hand.Cards.Remove(c);
            player.Money -= bet;
            bet *= 2;
            player.SecondHand.PointsA = player.Hand.PointsA / 2;
            player.SecondHand.PointsB = player.Hand.PointsB / 2;
            player.Hand.PointsA /= 2;
            player.Hand.PointsB /= 2;

            player.SecondHand.ActualPoints = player.SecondHand.PointsA;
            player.Hand.ActualPoints = player.Hand.PointsA;
            player.Hand.IsNotActive = false;
            player.SecondHand.IsNotActive = false;
        }

        /// <summary>
        /// Implements HitFirstHand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="deck">Ref of the Deck</param>
        /// <param name="bet">Bet amount</param>
        public void HitFirstHand(Player player, Player comp, IList<Card> deck, int bet)
        {
            Card card = deck[r.Next(deck.Count)];
            player.Hand.Cards.Add(card);
            if (card.Value == 11)
            {
                player.Hand.PointsA += card.Value;
                player.Hand.PointsB += 1;
            }
            else
            {
                player.Hand.PointsA += card.Value;
                player.Hand.PointsB += card.Value;
            }

            if (player.Hand.PointsA > 21 && player.Hand.PointsB <= 21)
            {
                player.Hand.ActualPoints = player.Hand.PointsB;
            }
            else
            {
                player.Hand.ActualPoints = player.Hand.PointsA;
            }

            if (player.Hand.ActualPoints > 21)
            {
                if (player.SecondHand.IsLost)
                {
                    Messenger.Default.Send($"You lost: {bet}$", "LogicResult");
                    this.WeAreLost(player);
                    bet = player.Money;
                    this.SetHandsAndPointsToNull(player, comp, deck);
                }
                else if (player.SecondHand.IsNotActive)
                {
                    player.Hand.IsLost = true;
                    player.Hand.IsNotActive = true;
                    this.SplittedHandGameProcess(player, comp, deck, ref bet);
                    this.SetHandsAndPointsToNull(player, comp, deck);
                }
                else
                {
                    player.Hand.IsLost = true;
                    player.Hand.IsNotActive = true;
                }
            }
        }

        /// <summary>
        /// Implements HitSecondHand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="deck">Ref of the Deck</param>
        /// <param name="bet">Bet amount</param>
        public void HitSecondHand(Player player, Player comp, IList<Card> deck, int bet)
        {
            Card card = deck[r.Next(deck.Count)];
            player.SecondHand.Cards.Add(card);
            if (card.Value == 11)
            {
                player.SecondHand.PointsA += card.Value;
                player.SecondHand.PointsB += 1;
            }
            else
            {
                player.SecondHand.PointsA += card.Value;
                player.SecondHand.PointsB += card.Value;
            }

            if (player.SecondHand.PointsA > 21 && player.SecondHand.PointsB <= 21)
            {
                player.SecondHand.ActualPoints = player.SecondHand.PointsB;
            }
            else
            {
                player.SecondHand.ActualPoints = player.SecondHand.PointsA;
            }

            if (player.SecondHand.ActualPoints > 21)
            {
                if (player.Hand.IsLost)
                {
                    Messenger.Default.Send($"You lost: {bet}$", "LogicResult");
                    this.WeAreLost(player);
                    bet = player.Money;
                    this.SetHandsAndPointsToNull(player, comp, deck);
                }
                else if (player.Hand.IsNotActive)
                {
                    player.SecondHand.IsLost = true;
                    player.SecondHand.IsNotActive = true;
                    this.SplittedHandGameProcess(player, comp, deck, ref bet);
                    this.SetHandsAndPointsToNull(player, comp, deck);
                }
                else
                {
                    player.SecondHand.IsLost = true;
                    player.SecondHand.IsNotActive = true;
                }
            }
        }

        /// <summary>
        /// Implements HitSecStandFirstHandondHand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="deck">Ref of the Deck</param>
        /// <param name="bet">Ref bet amount</param>
        public void StandFirstHand(Player player, Player comp, IList<Card> deck, ref int bet)
        {
            player.Hand.IsNotActive = true; // inaktívvá teszi a hitfirsthand gombokat

            if (player.SecondHand.IsNotActive)
            {
                this.SplittedHandGameProcess(player, comp, deck, ref bet);
                this.SetHandsAndPointsToNull(player, comp, deck);
            }
        }

        /// <summary>
        /// Implements StandSecondHand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="deck">Ref of the Deck</param>
        /// <param name="bet">Ref bet amount</param>
        public void StandSecondHand(Player player, Player comp, IList<Card> deck, ref int bet)
        {
            player.SecondHand.IsNotActive = true;
            if (player.Hand.IsNotActive)
            {
                this.SplittedHandGameProcess(player, comp, deck, ref bet);
                this.SetHandsAndPointsToNull(player, comp, deck);
            }
        }

        /// <summary>
        /// Implements NewGameCommand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="deck">Ref of the Deck</param>
        public void NewGameCommand(Player player, Player comp, IList<Card> deck)
        {
            IEditorService editorService = new EditorService();
            string name = editorService.NewGame();
            if (name != null)
            {
                player.Name = name;
                player.Money = 1000;
                player.IsLost = false;
                foreach (var card in player.Hand.Cards)
                {
                    deck.Add(card);
                }

                player.Hand.Cards.Clear();
                player.Hand.IsLost = false;
                foreach (var card in player.SecondHand.Cards)
                {
                    deck.Add(card);
                }

                player.SecondHand.Cards.Clear();
                player.SecondHand.IsLost = false;
                comp.Hand.IsLost = false;
                foreach (var card in comp.Hand.Cards)
                {
                    deck.Add(card);
                }

                comp.Hand.Cards.Clear();
            }
        }

        /// <summary>
        /// Implements GetHighScoreCommand
        /// </summary>
        public void GetHighscoreCommand()
        {
            if (File.Exists("highscore.txt"))
            {
                Window window = new Window
                {
                    Title = "Highscores",
                    Height = 400,
                    Width = 450,
                    Left = 600,
                    Top = 450,
                    Content = new ScoreBoardWindow()
                };
                window.ShowDialog();
            }
            else
            {
                Messenger.Default.Send($"You haven't played yet!", "LogicResult");
            }
        }

        /// <summary>
        /// Implements LoadCommand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="deck">Ref of the Deck</param>
        public void LoadCommand(Player player, Player comp, IList<Card> deck)
        {
            Window window = new Window
            {
                Title = "Load Game",
                Height = 400,
                Width = 450,
                Left = 600,
                Top = 450,
                Content = new LoadGameWindow()
            };

            if (window.ShowDialog() == true && (window.Content as LoadGameWindow).SelectedPlayer != null)
            {
                player.Name = (window.Content as LoadGameWindow).SelectedPlayer.Name;
                player.Money = (window.Content as LoadGameWindow).SelectedPlayer.Money;
                player.IsLost = false;
                this.SetHandsAndPointsToNull(player, comp, deck);
            }
        }

        /// <summary>
        /// Implements SaveCommand
        /// </summary>
        /// <param name="player">Ref of the player</param>
        public void SaveCommand(Player player)
        {
            if (!File.Exists("savedgames.txt"))
            {
                File.WriteAllText("savedgames.txt", string.Empty);
            }

            File.AppendAllText("savedgames.txt", $"{player.Name}\t{player.Money}\n");
            Messenger.Default.Send($"\tGame saved!\n Name: {player.Name}\t Money: {player.Money}", "LogicResult");
        }

        // innentől a private metódusok!
        private void HitForComp(Player comp, IList<Card> deck)
        {
            Card card = deck[r.Next(deck.Count)];
            comp.Hand.Cards.Add(card);
            if (card.Value == 11)
            {
                comp.Hand.PointsA += card.Value;
                comp.Hand.PointsB += 1;
            }
            else
            {
                comp.Hand.PointsA += card.Value;
                comp.Hand.PointsB += card.Value;
            }

            if (comp.Hand.PointsA > 21 && comp.Hand.PointsB <= 21)
            {
                comp.Hand.ActualPoints = comp.Hand.PointsB;
            }
            else
            {
                comp.Hand.ActualPoints = comp.Hand.PointsA;
            }

            deck.Remove(card);
        }

        private void WinWithOneHand(ref int bet, Player player)
        {
            Messenger.Default.Send($"You won: {bet * 2}$", "LogicResult");
            player.Money += bet * 2;
            bet = 0;
        }

        private void SplittedHandGameProcess(Player player, Player comp, IList<Card> deck, ref int bet)
        {
            int opportunities = 0;
            if (!player.Hand.IsLost && !player.SecondHand.IsLost)
            {
                while ((player.Hand.ActualPoints > comp.Hand.ActualPoints || player.SecondHand.ActualPoints > comp.SecondHand.ActualPoints) && comp.Hand.ActualPoints < 17)
                {
                    this.HitForComp(comp, deck);
                    opportunities = 1;
                }
            }
            else if (!player.Hand.IsLost)
            {
                while (player.Hand.ActualPoints > comp.Hand.ActualPoints && comp.Hand.ActualPoints < 17)
                {
                    this.HitForComp(comp, deck);
                    opportunities = 2;
                }
            }
            else if (!player.SecondHand.IsLost)
            {
                while (player.SecondHand.ActualPoints > comp.SecondHand.ActualPoints && comp.Hand.ActualPoints < 17)
                {
                    this.HitForComp(comp, deck);
                    opportunities = 3;
                }
            }

            switch (opportunities)
            {
                case 1:
                    if (comp.Hand.ActualPoints > 21)
                    {
                        Messenger.Default.Send($"You win : {bet * 2}$", "LogicResult");
                        this.WeAreWin(player);
                        player.Money += bet;
                        bet = 0;
                    }
                    else if (comp.Hand.ActualPoints < player.Hand.ActualPoints && comp.Hand.ActualPoints < player.SecondHand.ActualPoints)
                    {
                        Messenger.Default.Send($"You win : {bet * 2}$", "LogicResult");
                        this.WeAreWin(player);
                        player.Money += bet;
                        bet = 0;
                    }
                    else if (comp.Hand.ActualPoints < player.Hand.ActualPoints || comp.Hand.ActualPoints < player.SecondHand.ActualPoints)
                    {
                        Messenger.Default.Send($"You win (with one hand): {bet / 2}$", "LogicResult");
                        this.WeAreWin(player);
                        player.Money += bet / 2;
                        bet = 0;
                    }
                    else
                    {
                        Messenger.Default.Send($"You lost: {bet}$", "LogicResult");
                        this.WeAreLost(player);
                        bet = 0;
                    }

                    break;
                case 2:
                    if (comp.Hand.ActualPoints > 21)
                    {
                        player.Money += bet;
                        Messenger.Default.Send($"You win : {bet * 2}$", "LogicResult");
                        this.WeAreWin(player);
                        player.Money += bet;
                        bet = 0;
                    }
                    else if (comp.Hand.ActualPoints < player.Hand.ActualPoints)
                    {
                        Messenger.Default.Send($"You win (with one hand): {bet / 2}$", "LogicResult");
                        this.WeAreWin(player);
                        player.Money += bet / 2;
                        bet = 0;
                    }
                    else
                    {
                        Messenger.Default.Send($"You lost: {bet}$", "LogicResult");
                        this.WeAreLost(player);
                        bet = 0;
                    }

                    break;
                case 3:
                    if (comp.Hand.ActualPoints > 21)
                    {
                        player.Money += bet;
                        Messenger.Default.Send($"You win : {bet * 2}$", "LogicResult");
                        this.WeAreWin(player);
                        player.Money += bet;
                        bet = 0;
                    }
                    else if (comp.Hand.ActualPoints < player.SecondHand.ActualPoints)
                    {
                        Messenger.Default.Send($"You win (with one hand): {bet / 2}$", "LogicResult");
                        this.WeAreWin(player);
                        player.Money += bet / 2;
                        bet = 0;
                    }
                    else
                    {
                        Messenger.Default.Send($"You lost: {bet}$", "LogicResult");
                        this.WeAreLost(player);
                        bet = 0;
                    }

                    break;
                default:
                    break;
            }
        }

        private void SetHighScoreCommand(bool isLost)
        {
            if (!File.Exists("highscore.txt"))
            {
                File.WriteAllText("highscore.txt", "0\t0");
            }

            string[] results = File.ReadAllText("highscore.txt").Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
            if (isLost)
            {
                int compWon = int.Parse(results[1]);
                compWon += 1;
                File.WriteAllText("highscore.txt", results[0] + "\t" + compWon);
            }
            else
            {
                int playerWon = int.Parse(results[0]);
                playerWon += 1;
                File.WriteAllText("highscore.txt", playerWon + "\t" + results[1]);
            }
        }

        private void WeAreLost(Player player)
        {
            if (player.Money == 0)
            {
                player.IsLost = true;
                this.SetHighScoreCommand(player.IsLost);
                Messenger.Default.Send("Game Over", "LogicResult");
            }
        }

        private void WeAreWin(Player player)
        {
            // ha 10000 felé kerül a megnyert összeg akkor nyerünk és vége a játéknak!
            if (player.Money >= 10000)
            {
                player.IsWin = true;
                this.SetHighScoreCommand(!player.IsWin);
                Messenger.Default.Send("Congratulations, you won!", "LogicResult");
            }
        }

        private void SetHandsAndPointsToNull(Player player, Player comp, IList<Card> deck)
        {
            player.Hand.ActualPoints = 0;
            comp.Hand.ActualPoints = 0;
            player.Hand.PointsA = 0;
            player.Hand.PointsB = 0;
            comp.Hand.PointsA = 0;
            comp.Hand.PointsB = 0;

            foreach (var card in player.Hand.Cards)
            {
                deck.Add(card);
            }

            player.Hand.Cards.Clear();
            player.Hand.IsLost = false;
            foreach (var card in player.SecondHand.Cards)
            {
                deck.Add(card);
            }

            player.SecondHand.Cards.Clear();
            player.SecondHand.IsLost = false;
            comp.Hand.IsLost = false;
            foreach (var card in comp.Hand.Cards)
            {
                deck.Add(card);
            }

            comp.Hand.Cards.Clear();
        }
    }
}
