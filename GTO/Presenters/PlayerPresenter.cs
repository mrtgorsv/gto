using System;
using System.Collections.Generic;
using GTO.Model.Context;
using GTO.Models;
using GTO.Services.Implementations;

namespace GTO.Presenters
{
    public class PlayerPresenter : IDisposable
    {
        public Player EditableObject { get; set; }
        protected PlayerService PlayerService;

        public PlayerPresenter()
        {
            PlayerService = new PlayerService();
            EditableObject = PlayerService.Create();
        }

        public void Save()
        {
            PlayerService.Add(EditableObject);
        }
        public List<ComboBoxItem> SexList => new List<ComboBoxItem>
        {
            new ComboBoxItem {Text = "Мужской" , Value = 1},
            new ComboBoxItem {Text = "Женский" , Value = 0}
        };

        public void Dispose()
        {
            PlayerService.Dispose();
        }
    }
}