using System.Collections.Generic;

namespace GTO.Presenters
{
    public abstract class ListPresenterBase
    {
        private object _dataSource;
        public virtual int SelectedEntityId { get; }

        public Dictionary<string, string> EntityCloumns = new Dictionary<string, string>();

        public virtual object DataSource
        {
            get
            {
              return   _dataSource ?? (_dataSource = GetDataSource());
            }
        }

        protected virtual object GetDataSource()
        {
            return null;
        }

        public void RefreshDataSource()
        {
            _dataSource = GetDataSource();
        }

        public virtual object SelectedEntity { get; set; }

        public virtual void RefreshEntity(int entityId)
        {
        }
    }
}
