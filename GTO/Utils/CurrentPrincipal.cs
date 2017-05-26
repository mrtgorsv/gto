namespace GTO.Utils
{
    public sealed class CurrentPrincipal
    {
        private static CurrentPrincipal _instance;

        public int Id { get; set; }

        public string Login { get; set; }
        public bool IsAdmin { get; set; }

        public CurrentPrincipal() { }

        public static CurrentPrincipal Instance
        {
            get { return _instance ; }
            set { _instance = value; }
        }
    }
}
