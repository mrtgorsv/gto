using GTO.Model.Context;
using GTO.Services.Implementations;

namespace GTO.Presenters.Judges
{
    class JudgeListPresenter : ListPresenterBase
    {
        private readonly JudgeService _judgeService;

        public override int SelectedEntityId
        {
            get
            {
                return SelectedEntity != null
                    ? ((Judge) SelectedEntity).Id
                    : 0;
            }
        }

        public JudgeListPresenter()
        {
            _judgeService = new JudgeService();

            EntityCloumns.Add("FullName", "ФИО");
            EntityCloumns.Add("BirthDate", "Дата рождения");
        }

        protected override object GetDataSource()
        {
            return _judgeService.GetAll();
        }
        public override void RefreshEntity(int entityId)
        {
            _judgeService.Refresh(entityId);
        }
    }
}