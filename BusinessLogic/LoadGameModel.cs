// <copyright file="LoadGameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data;

    /// <summary>
    /// LoagGameModel implementation.
    /// </summary>
    public class LoadGameModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadGameModel"/> class.
        /// Constructor of LoadGameModel.
        /// </summary>
        public LoadGameModel()
        {
            this.Players = new List<Player>();
            if (File.Exists("savedgames.txt"))
            {
                string[] file = File.ReadAllLines("savedgames.txt");

                foreach (var line in file)
                {
                    this.Players.Add(new Player() { Name = line.Split('\t')[0], Money = int.Parse(line.Split('\t')[1]) });
                }
            }
        }

        /// <summary>
        /// Gets or sets list of the saved players.
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        /// Gets or sets the selected player.
        /// </summary>
        public Player SelectedPlayer { get; set; }
    }
}
