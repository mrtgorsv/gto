namespace GTO.Model.Context
{
    public partial class Player
    {
        public string FullName
        {
            get { return string.Format("{0} {1} {2}({3}-{4})", LastName, MiddleName, Name , PassSerial , PassNumber); }
        }
    }
}
