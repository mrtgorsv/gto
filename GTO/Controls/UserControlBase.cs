using System;
using System.Windows.Forms;

namespace GTO.Controls
{
    public delegate void NeedCloseEventHandler(object sender, EventArgs args);

    public partial class UserControlBase : UserControl
    {
        public event NeedCloseEventHandler NeedClose;

        public UserControlBase()
        {
            InitializeComponent();
            Load += OnFormLoad;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            InitializePresenter();
        }

        protected virtual void InitializePresenter()
        {
            
        }

        protected virtual void OnNeedClose()
        {
            NeedClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
