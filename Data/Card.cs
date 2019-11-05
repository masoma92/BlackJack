// <copyright file="Card.cs" company="PlaceholderCompany">
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
    /// Card data class.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// Card constructor.
        /// </summary>
        /// <param name="name">card name</param>
        /// <param name="image">card image</param>
        /// <param name="value">card value</param>
        public Card(string name, string image, int value)
        {
            this.Name = name;
            this.Image = image;
            this.Value = value;
        }

        /// <summary>
        /// Gets the card name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the card's image.
        /// </summary>
        public string Image { get; private set; }

        /// <summary>
        /// Gets or sets the card's value.
        /// </summary>
        public int Value { get; set; }
    }
}
