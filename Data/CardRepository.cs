// <copyright file="CardRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Implements ICardRepository interface.
    /// </summary>
    public class CardRepository : ICardRepository
    {
        private ObservableCollection<Card> Deck { get; set; }

        /// <summary>
        /// Creates the deck collection.
        /// </summary>
        /// <returns>returns a full deck</returns>
        public ObservableCollection<Card> GetDeck()
        {
            this.Deck = new ObservableCollection<Card>();
            var pictures = Directory.GetFiles(@"../../../Data/images"); // temp

            for (int i = 2; i < pictures.Length; i++)
            {
                string name = Path.GetFileNameWithoutExtension(pictures[i]);
                string image = pictures[i].Replace("../../../Data/images\\", "pack://application:,,,/Data;Component/images/"); // temp
                int value = 0;
                if (name.Any(char.IsDigit))
                {
                    value = int.Parse(name.Substring(1));
                }
                else
                {
                    switch (name[1])
                    {
                        case 'A':
                            value = 11;
                            break;
                        default:
                            value = 10;
                            break;
                    }
                }

                Card c = new Card(name, image, value);
                this.Deck.Add(c);
            }

            return this.Deck;
        }
    }
}
