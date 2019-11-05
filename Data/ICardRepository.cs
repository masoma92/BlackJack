// <copyright file="ICardRepository.cs" company="PlaceholderCompany">
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

    /// <summary>
    /// Interface for the repo.
    /// </summary>
    public interface ICardRepository
    {
        /// <summary>
        /// Gets all the card.
        /// </summary>
        /// <returns>Deck</returns>
        ObservableCollection<Card> GetDeck();
    }
}
