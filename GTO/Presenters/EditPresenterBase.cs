using System;
using GTO.Presenters.Interfaces;

namespace GTO.Presenters
{
    public abstract class EditPresenterBase : IDisposable , IEditPresenter
    {
        public abstract void Save();

        public abstract void Dispose();
    }
}
