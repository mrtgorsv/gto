using System;
using System.Windows.Forms;
using GTO.Utils;
using GTO.Views;

namespace GTO
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CurrentPrincipal.Instance = new CurrentPrincipal();
            Application.Run(new LoginForm());
        }
    }
}
