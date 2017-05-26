using System.Collections.Generic;
using GTO.Models;
using GTO.Services.Implementations;

namespace GTO.Presenters.Players
{
    public class PlayerPresenter : EditPresenterBase
    {
        public Model.Context.Player EditableObject { get; set; }
        protected PlayerService PlayerService;

        public PlayerPresenter(int playerId = 0)
        {
            PlayerService = new PlayerService();
            EditableObject = PlayerService.GetOrCreate(playerId);
        }

        public List<ComboBoxItem> SexList => new List<ComboBoxItem>
        {
            new ComboBoxItem {Text = "Мужской", Value = 1},
            new ComboBoxItem {Text = "Женский", Value = 0}
        };

        public override void Dispose()
        {
            PlayerService.Dispose();
        }

        public override void Save()
        {
            PlayerService.AddOrUpdate(EditableObject);
        }
    }
}