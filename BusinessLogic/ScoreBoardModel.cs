// <copyright file="ScoreBoardModel.cs" company="PlaceholderCompany">
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

    /// <summary>
    /// ScoreBoardModel for scoreboard view.
    /// </summary>
    public class ScoreBoardModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoardModel"/> class.
        /// Constructor of ScoreBoardModel, reads file.
        /// </summary>
        public ScoreBoardModel()
        {
            string[] temp = File.ReadAllLines("highscore.txt");
            this.PlayerPoint = temp[0].Split('\t')[0];
            this.CompPoint = temp[0].Split('\t')[1];
        }

        /// <summary>
        /// Gets or sets computer points.
        /// </summary>
        public string CompPoint { get; set; }

        /// <summary>
        /// Gets or sets PlayerPoints.
        /// </summary>
        public string PlayerPoint { get; set; }
    }
}
