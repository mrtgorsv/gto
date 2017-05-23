namespace GTO.Model.Context
{
    public partial class Judge
    {
        public string FullName
        {
            get { return string.Format("{0} {1} {2}", LastName, MiddleName, Name); }
        }
    }
}
