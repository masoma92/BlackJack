// <copyright file="ISavedGamesRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for SavedGamesRepo.
    /// </summary>
    internal interface ISavedGamesRepository
    {
        /// <summary>
        /// Saves game
        /// </summary>
        /// <param name="name">name of the user</param>
        /// <param name="money">amount of earned money</param>
        void Save(string name, int money);

        /// <summary>
        /// Loads game
        /// </summary>
        /// <param name="name">name of the saved game</param>
        /// <returns>name and money</returns>
        string[] Load(string name);

        /// <summary>
        /// Displays highscore table
        /// </summary>
        /// <returns>the users' scores and the comp score</returns>
        int[] GetHighscores();

        /// <summary>
        /// increments the user/comp value
        /// </summary>
        /// <param name="who">who wins (comp or user)</param>
        void SetHighscores(bool who);
    }
}
