// <copyright file="IGameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data;

    /// <summary>
    /// Interface for gamelogic.
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
        /// BetCommand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="bet">Ref of the bet amount</param>
        /// <param name="deck">Ref of the Deck</param>
        void BetCommand(Player player, Player comp, ref int bet, IList<Card> deck);

        /// <summary>
        /// HitCommand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="bet">Ref of the bet amount</param>
        /// <param name="deck">Ref of the Deck</param>
        void HitCommand(Player player, Player comp, ref int bet, IList<Card> deck);

        /// <summary>
        /// StandCommand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="bet">Ref of the bet amount</param>
        /// <param name="deck">Ref of the Deck</param>
        void StandCommand(Player player, Player comp, ref int bet, IList<Card> deck);

        /// <summary>
        /// DoubleCommand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="bet">Ref of the bet amount</param>
        /// <param name="deck">Ref of the Deck</param>
        void DoubleCommand(Player player, Player comp, ref int bet, IList<Card> deck);

        /// <summary>
        /// SurrenderCommand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// <param name="bet">Ref of the bet amount</param>
        /// <param name="deck">Ref of the Deck</param>
        void SurrenderCommand(Player player, Player comp, ref int bet, IList<Card> deck);

        /// <summary>
        /// SplitCommand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="bet">Ref of the bet amount</param>
        void SplitCommand(Player player, ref int bet);

        /// <summary>
        /// HitFirstHand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// /// <param name="deck">Ref of the Deck</param>
        /// <param name="bet">Bet amount</param>
        void HitFirstHand(Player player, Player comp, IList<Card> deck, int bet);

        /// <summary>
        /// HitSecondHand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// /// <param name="deck">Ref of the Deck</param>
        /// <param name="bet">Bet amount</param>
        void HitSecondHand(Player player, Player comp, IList<Card> deck, int bet);

        /// <summary>
        /// StandFirstHand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// /// <param name="deck">Ref of the Deck</param>
        /// <param name="bet">Bet amount</param>
        void StandFirstHand(Player player, Player comp, IList<Card> deck, ref int bet);

        /// <summary>
        /// StandSecondHand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// /// <param name="deck">Ref of the Deck</param>
        /// <param name="bet">Bet amount</param>
        void StandSecondHand(Player player, Player comp, IList<Card> deck, ref int bet);

        /// <summary>
        /// NewGameCommand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// /// <param name="deck">Ref of the Deck</param>
        void NewGameCommand(Player player, Player comp, IList<Card> deck);

        /// <summary>
        /// GetHighscoreCommand method.
        /// </summary>
        void GetHighscoreCommand();

        /// <summary>
        /// LoadCommand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        /// <param name="comp">Ref of the comp</param>
        /// /// <param name="deck">Ref of the Deck</param>
        void LoadCommand(Player player, Player comp, IList<Card> deck);

        /// <summary>
        /// SaveCommand method.
        /// </summary>
        /// <param name="player">Ref of the player</param>
        void SaveCommand(Player player);
    }
}
