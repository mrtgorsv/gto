using GTO.Model.Context;
using GTO.Services.Implementations;

namespace GTO.Presenters.Players
{
    class PlayerListPresenter : ListPresenterBase
    {
        private readonly PlayerService _playerService;

        public override int SelectedEntityId
        {
            get
            {
                return SelectedEntity != null
                    ? ((Player) SelectedEntity).Id
                    : 0;
            }
        }

        public PlayerListPresenter()
        {
            _playerService = new PlayerService();

            EntityCloumns.Add("FullName", "ФИО");
            EntityCloumns.Add("BirthDate", "Дата рождения");
        }

        protected override object GetDataSource()
        {
            return _playerService.GetAll();
        }

        public override void RefreshEntity(int entityId)
        {
            _playerService.Refresh(entityId);
        }
    }
}