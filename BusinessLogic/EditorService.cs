// <copyright file="EditorService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Implements IEditorService interface.
    /// </summary>
    internal class EditorService : IEditorService
    {
        /// <summary>
        /// Create a new window of NewGameWindow
        /// </summary>
        /// <returns>Returns name of the player</returns>
        public string NewGame()
        {
            Window window = new Window
            {
                Title = "New Game",
                Height = 250,
                Width = 450,
                Left = 600,
                Top = 450,
                Content = new NewGameWindow()
            };

            if (window.ShowDialog() == true)
            {
                return (window.Content as NewGameWindow).Name;
            }

            return null;
        }
    }
}
